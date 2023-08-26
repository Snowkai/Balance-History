namespace Balance_History
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        async private void BackToProfiles(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}