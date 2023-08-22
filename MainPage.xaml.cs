namespace Balance_History
{
    public partial class MainPage : ContentPage
    {


        public MainPage()
        {
            InitializeComponent();
        }

        private List<string> Categories = new List<string>() { "Food", "Transport", "Mobile", "Internet", "Entertainment" };

        public void AddCategory(string category)
        {
            Categories.Add(category);
        }
        public void RemoveCategory(string category)
        {
            Categories.Remove(category);
        }
        public void SendRecord() { }
        public void ReceiveRecord() { }
        public void DeleteRecord() { }
        public void GetReport() { }
    }
}