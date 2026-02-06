using dynamic_step_goals.Models;
using dynamic_step_goals.Services;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace dynamic_step_goals.PageModels;

public class MainPageViewModel : INotifyPropertyChanged
{
    #region Properties

    private int _todayGoal = 500;
    private int _tomorrowGoal = 0;
    private string _todayStepsInput = string.Empty;
    private StepGoalService _stepGoalService = new StepGoalService();
    private DailyStepStorageService _storageService = new DailyStepStorageService();

    public int TodayGoal
    {
        get => _todayGoal;
        set => SetProperty(ref _todayGoal, value);
    }

    public int TomorrowGoal
    {
        get => _tomorrowGoal;
        set => SetProperty(ref _tomorrowGoal, value);
    }

    public string TodayStepsInput
    {
        get => _todayStepsInput;
        set => SetProperty(ref _todayStepsInput, value);
    }

    public ICommand SaveStepsCommand { get; }

    #endregion

    public MainPageViewModel()
    {
        SaveStepsCommand = new Command(async () => await SaveStepsAsync());
        _ = LoadInitialStateAsync();
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    protected void OnPropertyChanged([CallerMemberName] string? name = null)
        => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

    protected bool SetProperty<T>(ref T backingStore, T value, [CallerMemberName] string? name = null)
    {
        if (EqualityComparer<T>.Default.Equals(backingStore, value))
            return false;

        backingStore = value;
        OnPropertyChanged(name);
        return true;
    }

    private async Task LoadInitialStateAsync()
    {
        var today = DateOnly.FromDateTime(DateTime.Today);
        var tomorrow = today.AddDays(1);

        var todaysEntry = await _storageService.GetByDateAsync(today);
        if (todaysEntry != null)
            TodayGoal = todaysEntry.Goal;

        var tomorrowsEntry = await _storageService.GetByDateAsync(tomorrow);
        if (tomorrowsEntry != null)
            TomorrowGoal = tomorrowsEntry.Goal;
        else if (todaysEntry != null)
        {
            TomorrowGoal = _stepGoalService.CalculateTomorrowGoal(
                todaysEntry,
                await _storageService.GetAllAsync());
        }
    }


    private async Task SaveStepsAsync()
    {
        if (!int.TryParse(TodayStepsInput, out var todaySteps))
            return;

        var today = DateOnly.FromDateTime(DateTime.Today);
        var todayEntry = new DailyStepEntry
        {
            Date = today,
            CurrentSteps = todaySteps,
            Goal = TodayGoal
        };
        await _storageService.AddOrUpdateAsync(todayEntry);

        TomorrowGoal = _stepGoalService.CalculateTomorrowGoal(todayEntry, await _storageService.GetAllAsync());

        var tomorrow = today.AddDays(1);
        var tomorrowEntry = new DailyStepEntry
        {
            Date = tomorrow,
            CurrentSteps = 0,
            Goal = TomorrowGoal
        };
        await _storageService.AddOrUpdateAsync(tomorrowEntry);

        TodayStepsInput = string.Empty;
    }


}
