using Microsoft.EntityFrameworkCore;

namespace BankAccountApi.Data
{
    public class AccountsRepository(AppDbContext context) : IAccountsRepository
    {
        public async Task<IEnumerable<Account>> GetAccounts()
        {
            return await context.Accounts
                .Join(
                    context.AccountTypes,
                    a => a.AccountTypeId,
                    at => at.Id,
                    (a, at) => AddAccountType(a, at.Name))
                .ToListAsync();
        }

        public async Task<Account?> GetAccount(uint accountNumber)
        {
            return await context.Accounts
                .Where(a => a.AccountNumber == accountNumber)
                .Join(
                    context.AccountTypes,
                    a => a.AccountTypeId,
                    at => at.Id,
                    (a, at) => AddAccountType(a, at.Name))
                .FirstOrDefaultAsync();
        }

        public async Task<Account> AddAccount(AccountDetails accountDetails)
        {
            var dt = DateTime.UtcNow;
            var accountType = context.AccountTypes.First(at => at.Name == accountDetails.AccountTypeText.ToString());
            Account account = new()
            {
                AccountNumber = accountDetails.AccountNumber,
                AccountTypeId = accountType.Id,
                AccountTypeText = accountType.Name,
                IsActive = accountDetails.IsActive,
                Created = dt,
                LastUpdated = dt
            };

            context.Accounts.Add(account);
            await context.SaveChangesAsync();

            return account;
        }

        public async Task<Account?> UpdateAccount(uint accountNumber, bool isActive)
        {
            var account = await context.Accounts.FirstOrDefaultAsync(a => a.AccountNumber == accountNumber);

            if (account is null)
            {
                return null;
            }

            account.IsActive = isActive;
            account.LastUpdated = DateTime.UtcNow;
            await context.SaveChangesAsync();

            return account;
        }

        public async Task<bool> DeleteAccount(uint accountNumber)
        {
            var account = await context.Accounts.FirstOrDefaultAsync(a => a.AccountNumber == accountNumber);

            if (account is null)
            {
                return false;
            }

            context.Accounts.Remove(account);
            await context.SaveChangesAsync();

            return true;
        }

        public async Task<IEnumerable<Transaction>> GetTransactionsForAccount(uint accountNumber)
        {
            return await context.Transactions
                .Where(t => t.AccountNumber == accountNumber)
                .OrderBy(t => t.Created)
                .ToListAsync();
        }

        public async Task<Transaction> AddTransaction(uint accountNumber, decimal amount)
        {
            var transactionId = Guid.NewGuid();
            Guid? relatedTransactionId = null;

            return await ApplyTransaction(accountNumber, transactionId, relatedTransactionId, amount);
        }

        public async Task<(Transaction, Transaction)> TransferFunds(uint fromAccountNumber, uint toAccountNumber, decimal amount)
        {
            var fromTransactionId = Guid.NewGuid();
            var toTransactionId = Guid.NewGuid();
            Transaction transaction1;
            Transaction transaction2;

            // Would normally use the following, but transactions are not supported by the EF Core in-memory database
            //using var dbTransaction = context.Database.BeginTransaction();

            try
            {
                transaction1 = await ApplyTransaction(fromAccountNumber, fromTransactionId, toTransactionId, -amount);
                transaction2 = await ApplyTransaction(toAccountNumber, toTransactionId, fromTransactionId, amount);

                // Would normally use the following, but transactions are not supported by the EF Core in-memory database
                //dbTransaction.Commit();
            }
            catch
            {
                // Would normally use the following, but transactions are not supported by the EF Core in-memory database
                //dbTransaction.Rollback();
                throw;
            }

            return (transaction1, transaction2);
        }

        private async Task<Transaction> ApplyTransaction(
            uint accountNumber,
            Guid transactionId,
            Guid? relatedTransactionId,
            decimal amount)
        {
            var account = await context.Accounts
                .Where(a => a.AccountNumber == accountNumber)
                .FirstOrDefaultAsync() ?? throw new KeyNotFoundException($"Account {accountNumber} not found");

            if (!account.IsActive)
            {
                throw new InvalidOperationException($"Account {accountNumber} is disabled");
            }

            account.Balance += amount;

            var transaction = new Transaction()
            {
                Id = transactionId,
                AccountNumber = accountNumber,
                Amount = amount,
                RelatedTransactionId = relatedTransactionId,
                Created = DateTime.UtcNow
            };

            context.Transactions.Add(transaction);
            await context.SaveChangesAsync();

            return transaction;
        }

        private static Account AddAccountType(Account account, string accountTypeText)
        {
            account.AccountTypeText = accountTypeText;
            return account;
        }
    }
}
