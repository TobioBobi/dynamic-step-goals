using dynamic_step_goals.Models;
using dynamic_step_goals.Services;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace dynamic_step_goals.PageModels;

public class MainPageViewModel : INotifyPropertyChanged
{
    #region Properties

    private int _todayGoal = 8000;
    private int _tomorrowGoal = 8300;
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

        var todayEntry = await _storageService.GetByDateAsync(today);

        if (todayEntry != null)
            TodayGoal = todayEntry.Goal;
        else
            TodayGoal = 1;

        TomorrowGoal = TodayGoal + 300;
    }

    private async Task SaveStepsAsync()
    {
        if (!int.TryParse(TodayStepsInput, out var todaySteps))
            return;

        var today = DateOnly.FromDateTime(DateTime.Today);

        var entry = new DailyStepEntry
        {
            Date = today,
            ActualSteps = todaySteps,
            Goal = TodayGoal
        };

        await _storageService.AddOrUpdateAsync(entry);

        var result = _stepGoalService.CalculateGoals(todaySteps, TodayGoal);

        TodayGoal = result.todayGoal;
        TomorrowGoal = result.tomorrowGoal;

        TodayStepsInput = string.Empty;
    }


}
