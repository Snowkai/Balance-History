namespace Balance_History.src
{
    internal class Profile
    {
        public string Name { get; set; }
        public string DBName { get; set; }

        public Profile(string name)
        {
            Name = name;
            DBName = name + ".db";
        }
        public string DatabasePath =>
            Path.Combine(FileSystem.AppDataDirectory, DBName);
    }
}
