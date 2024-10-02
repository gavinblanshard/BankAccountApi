namespace BankAccountApi.Data
{
    public interface IAccountsRepository
    {
        Task<IEnumerable<Account>> GetAccounts();
        Task<Account?> GetAccount(uint accountNumber);
        Task<Account> AddAccount(AccountDetails accountDetails);
        Task<Account?> UpdateAccount(uint accountNumber, bool isActive);
        Task<bool> DeleteAccount(uint accountNumber);

        Task<IEnumerable<Transaction>> GetTransactionsForAccount(uint accountNumber);
        Task<Transaction> AddTransaction(uint accountNumber, decimal amount);
        Task<(Transaction, Transaction)> TransferFunds(uint fromAccountNumber, uint toAccountNumber, decimal amount);
    }
}
