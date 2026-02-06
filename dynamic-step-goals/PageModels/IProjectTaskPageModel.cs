using CommunityToolkit.Mvvm.Input;
using dynamic_step_goals.Models;

namespace dynamic_step_goals.PageModels
{
    public interface IProjectTaskPageModel
    {
        IAsyncRelayCommand<ProjectTask> NavigateToTaskCommand { get; }
        bool IsBusy { get; }
    }
}