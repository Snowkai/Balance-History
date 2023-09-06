using System.Collections.ObjectModel;

namespace Balance_History.src
{
    internal static class AppData
    {

        public static ObservableCollection<string> Categories = new ObservableCollection<string>();
        public static ObservableCollection<string> ListOfDefaultCategories = new ObservableCollection<string>() { "Food", "Transport", "Mobile", "Internet", "Entertaiment" };
    }
}
