namespace BankAccountApi.Data
{
    public class TransactionResponse
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
        /// The transactions that were created
        /// </summary>
        public List<Transaction> Transactions { get; }

        public TransactionResponse(
            uint accountNumber,
            string errorMessage)
        {
            AccountNumber = accountNumber;
            ErrorMessage = errorMessage;
            Transactions = [];
        }

        public TransactionResponse(
            uint accountNumber,
            Transaction? transaction = null)
        {
            AccountNumber = accountNumber;
            ErrorMessage = null;
            Transactions = [];
            if (transaction is not null)
            {
                Transactions.Add(transaction);
            }
        }

        public TransactionResponse(
            uint accountNumber,
            (Transaction, Transaction) transactions)
        {
            AccountNumber = accountNumber;
            ErrorMessage = null;
            Transactions = [];
            Transactions.Add(transactions.Item1);
            Transactions.Add(transactions.Item2);
        }
    }
}
