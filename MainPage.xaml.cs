using Balance_History.src;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace Balance_History
{
    public partial class MainPage : ContentPage
    {
        DBActions db = new DBActions();
        List<TableModel> tableModels = new List<TableModel>();
        public MainPage()
        {
            InitializeComponent();
            GetItemsFromDB();
            categoryList.ItemsSource = AppData.Categories;
            pickCategory.ItemsSource = AppData.Categories;
        }

        async private void GetItemsFromDB()
        {
            tableModels = await db.GetItemsAsync();
        }
        async private void BackToProfiles(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

        private void AddRecord(object sender, EventArgs e)
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

        private void RecordDone(object sender, EventArgs e)
        {
            TableModel record = new TableModel();
            record.dateTime = System.Data.DataSetDateTime.Local;
            record.Category = pickCategory.SelectedItem.ToString();
            record.Comment = entComment.Text;
            record.Price = Decimal.Parse(entPrice.Text);
            recordView.IsVisible = false;
            db.SaveItemAsync(record);
            viewTable.ItemsSource = tableModels;
        }
    };
}

