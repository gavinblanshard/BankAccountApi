using System.ComponentModel.DataAnnotations;
using BankAccountApi.Enums;

namespace BankAccountApi.Data
{
    public class AccountDetails
    {
        [Required]
        [Range(0, 99999999)]
        public uint AccountNumber { get; set; }

        [Required]
        [EnumDataType(typeof(AccountTypeText))]
        public AccountTypeText AccountTypeText { get; set; }

        [Required]
        public bool IsActive { get; set; }
    }
}
