using SQLite;

namespace Balance_History.Models
{
    public class Record
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public DateTime Data
        {
            get
            {
                return Data = DateTime.Now;
            }
            set { }
        }
        public string Category { get; set; }
        public decimal Price { get; set; }
        public string Comment { get; set; }

        public Record Clone() => MemberwiseClone() as Record;

#pragma warning disable CS8632 // The annotation for nullable reference types should only be used in code within a '#nullable' annotations context.
        public (bool IsValid, string? ErrorMessage) Validate()
#pragma warning restore CS8632 // The annotation for nullable reference types should only be used in code within a '#nullable' annotations context.
        {
            if (string.IsNullOrWhiteSpace(Category))
            {
                return (false, $"{nameof(Category)} is requared");
            }
            else if (Price < 0)
            {
                return (false, $"{nameof(Price)} less then 0");
            }
            return (true, null);
        }
    }
}
