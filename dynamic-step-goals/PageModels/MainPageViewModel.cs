using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace dynamic_step_goals.PageModels;

public class MainPageViewModel : INotifyPropertyChanged
{
    private int _todayGoal = 8000;
    private int _tomorrowGoal = 8300;
    private string _todayStepsInput = string.Empty;

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

    public MainPageViewModel()
    {
        SaveStepsCommand = new Command(() =>
        {
            // Dummy-Verhalten für jetzt:
            // Beim Klick verändern wir einfach die Ziele,
            // damit du siehst, dass Binding funktioniert.
            TodayGoal += 100;
            TomorrowGoal += 150;
            TodayStepsInput = string.Empty;
        });
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
}
