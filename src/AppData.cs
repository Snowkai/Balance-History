using Newtonsoft.Json;
using System.Collections.ObjectModel;

namespace Balance_History.src
{
    [JsonObject(MemberSerialization.OptIn)]
    internal static class AppData
    {
        [JsonProperty]
        public static ObservableCollection<string> Categories = new ObservableCollection<string>();
        private static List<string> ListOfDefaultCategories = new List<string>() { "Food", "Transport", "Mobile", "Internet", "Entertaiment" };

        public static void ConvertToObserver(List<string> list)
        {
            foreach (string category in list)
            {
                Categories.Add(category);
            }
        }
        public static List<string> ConvertToList(ObservableCollection<string> collection)
        {
            var list = new List<string>();
            foreach (string category in collection)
            {
                list.Add(category);
            }
            return list;
        }
        public static void SetDefault()
        {
            ConvertToObserver(ListOfDefaultCategories);
        }
    }
}
