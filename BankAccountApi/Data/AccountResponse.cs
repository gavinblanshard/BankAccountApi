namespace BankAccountApi.Data
{
    public class AccountResponse
    {
        /// <summary>
        /// The Account Number of the account specified in the request
        /// </summary>
        public uint AccountNumber { get; }

        /// <summary>
        /// A message indicating the cause of any error that occurred, null if successful
        /// </summary>
        public string? ErrorMessage { get; }

        /// <summary>
        /// The account that was created or updated
        /// </summary>
        public Account? Account { get; }

        public AccountResponse(
            uint accountNumber,
            string errorMessage)
        {
            AccountNumber = accountNumber;
            ErrorMessage = errorMessage;
            Account = null;
        }

        public AccountResponse(
            uint accountNumber,
            Account? account = null)
        {
            AccountNumber = accountNumber;
            ErrorMessage = null;
            Account = account;
        }
    }
}
