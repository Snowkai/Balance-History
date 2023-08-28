using Balance_History.src;

namespace Balance_History;

public partial class StartPage : ContentPage
{
	public StartPage()
	{
		InitializeComponent();
	}

	List<Profile> profiles = new List<Profile>();
	Dictionary<string, Button> buttons = new Dictionary<string, Button>();
    async void CreateForm(object sender, System.EventArgs e)
	{
		var name = await DisplayPromptAsync("Create profile", "Enter profile name:");
		if (name != null) {
			Profile profile = new Profile(name);
			profiles.Add(profile);
			Button button = new Button() {				
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
			buttons.Add(name, button);
		}
		else
		{
			await DisplayAlert("Alert", "You didn't enter profile name.", "Cancel");
		}
    }
	async private void NextPage(object sender, System.EventArgs e)
	{
		await Navigation.PushAsync(new MainPage());
	}
	async private void DeleteProfile(object sender, System.EventArgs e)
	{
		string[] names = new string[profiles.Count];
		for(int i = 0; i < names.Length; i++)
		{
			names[i] = profiles[i].Name;
		}
        var name = await DisplayActionSheet("Delete Profile", "Cancel", "Delete", names);
		profiles.Remove(profiles.Find(item => item.Name == name));
		buttons.Remove(name);
	}
}