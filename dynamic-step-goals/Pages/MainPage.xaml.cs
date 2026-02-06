using dynamic_step_goals.Models;
using dynamic_step_goals.PageModels;

namespace dynamic_step_goals.Pages
{
    public partial class MainPage : ContentPage
    {
        public MainPage(MainPageModel model)
        {
            InitializeComponent();
            BindingContext = model;
        }
    }
}