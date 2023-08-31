using Balance_History.src;

namespace Balance_History;

public partial class StartPage : ContentPage
{
    public StartPage()
    {
        InitializeComponent();
    }

    Dictionary<string, Button> buttons = new Dictionary<string, Button>();
    [Obsolete]
    async void CreateForm(object sender, System.EventArgs e)
    {
        var name = await DisplayPromptAsync("Create profile", "Enter profile name:");
        if (name != null)
        {
            Profile profile = new Profile(name);
            AppData.ProfileList.Add(profile);
            Button button = new Button()
            {
                Text = name,
                CornerRadius = 10,
                VerticalOptions = LayoutOptions.Start,
                TextColor = Colors.WhiteSmoke,
                FontSize = 22,
                FontAttributes = FontAttributes.Bold,
                HeightRequest = 100,
                BackgroundColor = Color.FromHex("6C6C6C"),
            };
            button_set.Children.Add(button);
            button.Clicked += NextPage;
            buttons[name] = button;
        }
        else
        {
            await DisplayAlert("Alert", "You didn't enter profile name.", "Cancel");
        }
    }
    async private void NextPage(object sender, System.EventArgs e)
    {
        Button btn = sender as Button;
        AppData.CurrentProfileName = btn.Text;
        await Navigation.PushAsync(new MainPage());
    }
    async private void DeleteProfile(object sender, System.EventArgs e)
    {
        string[] names = new string[AppData.ProfileList.Count];
        for (int i = 0; i < names.Length; i++)
        {
            names[i] = AppData.ProfileList[i].Name;
        }
        var name = await DisplayActionSheet("Delete Profile", "Cancel", null, names);
        AppData.ProfileList.Remove(AppData.ProfileList.Find(item => item.Name == name));
        button_set.Children.Remove(buttons[name]);
    }
}