using SQLite;
using System.Data;

namespace Balance_History.src
{
    internal class TableModel
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public DataSetDateTime dateTime { get; set; }
        public string Category { get; set; }
        public decimal Price { get; set; }
        public string Comment { get; set; }
    }
}
