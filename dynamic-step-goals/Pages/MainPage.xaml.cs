namespace dynamic_step_goals.Pages
{
    public class MainPage : ContentPage
    {
        public MainPage()
        {
            Content = new VerticalStackLayout
            {
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.Center,
                Children =
                {
                    new Label { Text = "Hello World", FontSize = 32 }
                }
            };
        }
    }
}