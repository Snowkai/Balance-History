using System.Collections.ObjectModel;

namespace Balance_History.src
{
    internal static class AppData
    {
        public static List<Profile> ProfileList = new List<Profile>();
        public static ObservableCollection<string> Categories = new ObservableCollection<string>() { "Food", "Transport", "Mobile", "Internet", "Entertaiment" };
        public static string CurrentProfileName = null;

        public static int CurrentProfileIndex()
        {
            return ProfileList.FindIndex(p => p.Name == CurrentProfileName);
        }
    }
}
