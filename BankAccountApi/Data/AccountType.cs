using System.ComponentModel.DataAnnotations;

namespace BankAccountApi.Data
{
    public class AccountType
    {
        [Key]
        [Required]
        public Guid Id { get; set; }

        [Required]
        public required string Name { get; set; }
    }
}
