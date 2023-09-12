using SQLite;
using System.Linq.Expressions;
using System.Reflection;

namespace Balance_History.src
{
    public class DatabaseContex : IAsyncDisposable
    {
        private const string DbName = "BHDatabase.db3";
        private static string DbPath => Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"\" + DbName;
        //private static string DbPath => Path.Combine(FileSystem.AppDataDirectory, DbName);
        private SQLiteAsyncConnection _connection;

        /* Unmerged change from project 'Balance History (net7.0-windows10.0.19041.0)'
        Before:
                private SQLiteAsyncConnection Database => (_connection ??= new SQLiteAsyncConnection(DbPath, SQLiteOpenFlags.Create | SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.SharedCache));

                private async Task CreateTableIfNotExist<TTable>() where TTable : class, new()
        After:
                private SQLiteAsyncConnection Database => (_connection ??= new SQLiteAsyncConnection(DbPath, SQLiteOpenFlags.Create | SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.SharedCache));

                private async Task CreateTableIfNotExist<TTable>() where TTable : class, new()
        */

        /* Unmerged change from project 'Balance History (net7.0-maccatalyst)'
        Before:
                private SQLiteAsyncConnection Database => (_connection ??= new SQLiteAsyncConnection(DbPath, SQLiteOpenFlags.Create | SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.SharedCache));

                private async Task CreateTableIfNotExist<TTable>() where TTable : class, new()
        After:
                private SQLiteAsyncConnection Database => (_connection ??= new SQLiteAsyncConnection(DbPath, SQLiteOpenFlags.Create | SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.SharedCache));

                private async Task CreateTableIfNotExist<TTable>() where TTable : class, new()
        */

        /* Unmerged change from project 'Balance History (net7.0-ios)'
        Before:
                private SQLiteAsyncConnection Database => (_connection ??= new SQLiteAsyncConnection(DbPath, SQLiteOpenFlags.Create | SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.SharedCache));

                private async Task CreateTableIfNotExist<TTable>() where TTable : class, new()
        After:
                private SQLiteAsyncConnection Database => (_connection ??= new SQLiteAsyncConnection(DbPath, SQLiteOpenFlags.Create | SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.SharedCache));

                private async Task CreateTableIfNotExist<TTable>() where TTable : class, new()
        */
        private SQLiteAsyncConnection Database => (_connection ??= new SQLiteAsyncConnection(DbPath, SQLiteOpenFlags.Create | SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.SharedCache));

        private async Task CreateTableIfNotExist<TTable>() where TTable : class, new()
        {
            await Database.CreateTableAsync<TTable>();
        }
        private async Task<AsyncTableQuery<TTable>> GetTableAsync<TTable>() where TTable : class, new()
        {
            await CreateTableIfNotExist<TTable>();
            return Database.Table<TTable>();
        }
        public async Task<IEnumerable<TTable>> GetAllAsync<TTable>() where TTable : class, new()
        {
            var table = await GetTableAsync<TTable>();
            return await table.ToListAsync();
        }
        public async Task<IEnumerable<TTable>> GetFilteringAsync<TTable>(Expression<Func<TTable, bool>> predicate) where TTable : class, new()
        {
            var table = await GetTableAsync<TTable>();
            return await table.Where(predicate).ToListAsync();
        }
        public async Task<bool> AddItemAsync<TTable>(TTable item) where TTable : class, new()
        {
            await CreateTableIfNotExist<TTable>();
            return await Database.InsertAsync(item) > 0;
        }
        public async Task<bool> UpdateItemAsync<TTable>(TTable item) where TTable : class, new()
        {
            await CreateTableIfNotExist<TTable>();
            return await Database.UpdateAsync(item) > 0;
        }
        public async Task<bool> DeleteItemAsync<TTable>(TTable item) where TTable : class, new()
        {
            await CreateTableIfNotExist<TTable>();
            return await Database.DeleteAsync(item) > 0;
        }
        public async Task<bool> DeleteItemByKeyAsync<TTable>(object primaryKey) where TTable : class, new()
        {
            await CreateTableIfNotExist<TTable>();
            return await Database.DeleteAsync<TTable>(primaryKey) > 0;
        }
        public async Task<TTable> GetItemByKeyAsync<TTable>(object primaryKey) where TTable : class, new()
        {
            await CreateTableIfNotExist<TTable>();
            return await Database.GetAsync<TTable>(primaryKey);
        }

        public async ValueTask DisposeAsync()
        {
            await _connection?.CloseAsync();
        }
    }
}
