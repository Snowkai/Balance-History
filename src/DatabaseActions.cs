using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Balance_History.src
{
    class DatabaseActions
    {
        SQLiteAsyncConnection Database;
        public DatabaseActions()
        {
        }

        async Task Init()
        {
            if (Database is not null)
                return;

            Database = new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
            var result = await Database.CreateTableAsync<Record>();
        }
        public async Task<List<Record>> GetItemsAsync()
        {
            await Init();
            return await Database.Table<Record>().ToListAsync();
        }

        public async Task<List<Record>> GetItemsNotDoneAsync()
        {
            await Init();
            return await Database.Table<Record>().Where(t => t.Done).ToListAsync();

            // SQL queries are also possible
            //return await Database.QueryAsync<TodoItem>("SELECT * FROM [TodoItem] WHERE [Done] = 0");
        }

        public async Task<Record> GetItemAsync(int id)
        {
            await Init();
            return await Database.Table<Record>().Where(i => i.ID == id).FirstOrDefaultAsync();
        }

        public async Task<int> SaveItemAsync(Record item)
        {
            await Init();
            if (item.ID != 0)
                return await Database.UpdateAsync(item);
            else
                return await Database.InsertAsync(item);
        }

        public async Task<int> DeleteItemAsync(Record item)
        {
            await Init();
            return await Database.DeleteAsync(item);
        }
    }
}
