using SQLite;

namespace Balance_History.src
{
    internal class DBActions
    {
        SQLiteAsyncConnection Database;
        public const SQLiteOpenFlags Flags =
        // open the database in read/write mode
        SQLite.SQLiteOpenFlags.ReadWrite |
        // create the database if it doesn't exist
        SQLite.SQLiteOpenFlags.Create |
        // enable multi-threaded database access
        SQLite.SQLiteOpenFlags.SharedCache;

        public DBActions() { }

        async Task Init()
        {
            if (Database is not null)
                return;

            Database = new SQLiteAsyncConnection(AppData.ProfileList[AppData.CurrentProfileIndex()].DatabasePath, Flags);
            await Database.CreateTableAsync<TableModel>();
        }

        public async Task<List<TableModel>> GetItemsAsync()
        {
            await Init();
            return await Database.Table<TableModel>().ToListAsync();
        }

        public async Task<TableModel> GetItemAsync(int id)
        {
            await Init();
            return await Database.Table<TableModel>().Where(i => i.Id == id).FirstOrDefaultAsync();
        }

        public async Task<int> SaveItemAsync(TableModel item)
        {
            await Init();
            if (item.Id != 0)
                return await Database.UpdateAsync(item);
            else
                return await Database.InsertAsync(item);
        }

        public async Task<int> DeleteItemAsync(TableModel item)
        {
            await Init();
            return await Database.DeleteAsync(item);
        }
    }
}
