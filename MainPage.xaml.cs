using Balance_History.src;
using Balance_History.ViewModels;
using System.Collections.ObjectModel;

namespace Balance_History
{
    public partial class MainPage : ContentPage
    {
        private readonly RecordViewModel _viewModel;
        public MainPage(RecordViewModel viewModel)
        {
            InitializeComponent();
            _viewModel = viewModel;
            BindingContext = _viewModel;
            categoryList.ItemsSource = AppData.Categories;
            pickCategory.ItemsSource = AppData.Categories;
        }

        public MainPage()
        {
        }

        async private void BackToProfiles(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

        private void AddRecordMenu(object sender, EventArgs e)
        {
            if (recordView.IsVisible == false)
            {
                recordView.IsVisible = true;
            }
            else
            {
                recordView.IsVisible = false;
            }
        }

        private void EditCategory(object sender, EventArgs e)
        {
            if (category.IsVisible == false)
            {
                category.IsVisible = true;
            }
            else
            {
                category.IsVisible = false;
            }
        }

        private async void AddCategory(object sender, EventArgs e)
        {
            var cat = await DisplayPromptAsync("Add Category", null);
            AppData.Categories.Add(cat);
        }

        private async void RemoveCategory(object sender, EventArgs e)
        {
            string[] cat = new string[AppData.Categories.Count];
            for(int i = 0; i < cat.Length; i++)
            {
                cat[i] = AppData.Categories[i];
            }
            var catName = await DisplayActionSheet("Remove Category", "Cancel", null, cat);
            AppData.Categories.Remove(catName);
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            await _viewModel.LoadRecordsAsync();
        }

        private void AddRecord(object sender, EventArgs e)
        {
            _viewModel.SaveRecordCommand.Execute(null);
        }
    }
}

