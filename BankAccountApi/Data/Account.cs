using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace BankAccountApi.Data
{
    public class Account
    {
        [Key]
        [JsonIgnore]
        public uint AccountNumber { get; set; }

        public string AccountNumberFormatted => AccountNumber.ToString("D8");

        [JsonIgnore]
        public Guid AccountTypeId { get; set; }

        [NotMapped]
        public string? AccountTypeText { get; set; }

        public bool IsActive { get; set; }

        public decimal Balance { get; set; }
        
        public DateTime Created { get; set; }

        public DateTime LastUpdated { get; set; }
    }
}
