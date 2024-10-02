using BankAccountApi.Data;
using Microsoft.AspNetCore.Mvc;

namespace BankAccountApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TransactionsController : ControllerBase
    {
        private readonly IAccountsRepository _repository;

        private const string InvalidAmountMessage = "The amount must be greater than 0 and no more than 50,000";

        public TransactionsController(IAccountsRepository repository)
        {
            _repository = repository;
        }

        // GET: api/Transactions/GetTransactionsForAccount/12345678
        [HttpGet("{accountNumber}")]
        public async Task<ActionResult<IEnumerable<Transaction>>> GetTransactionsForAccount(uint accountNumber)
        {
            var transactions = await _repository.GetTransactionsForAccount(accountNumber);
            return transactions.ToList();
        }

        // POST: api/Transactions/CreditAccount
        [HttpPost("{accountNumber}")]
        public async Task<ActionResult<TransactionResponse>> CreditAccount(uint accountNumber, decimal amount)
        {
            Transaction transaction;

            if (amount <= 0 || amount > 50000)
            {
                return new TransactionResponse(accountNumber, InvalidAmountMessage);
            }

            try
            {
                transaction = await _repository.AddTransaction(accountNumber, amount);
            }
            catch (Exception ex)
            {
                return new TransactionResponse(accountNumber, ex.Message);
            }

            return new TransactionResponse(accountNumber, transaction);
        }

        // POST: api/Transactions/DebitAccount
        [HttpPost("{accountNumber}")]
        public async Task<ActionResult<TransactionResponse>> DebitAccount(uint accountNumber, decimal amount)
        {
            Transaction transaction;

            if (amount <= 0 || amount > 50000)
            {
                return new TransactionResponse(accountNumber, InvalidAmountMessage);
            }

            try
            {
                transaction = await _repository.AddTransaction(accountNumber, -amount);
            }
            catch (Exception ex)
            {
                return new TransactionResponse(accountNumber, ex.Message);
            }

            return new TransactionResponse(accountNumber, transaction);
        }

        // POST: api/Transactions/TransferFunds
        [HttpPost("{fromAccountNumber}")]
        public async Task<ActionResult<TransactionResponse>> TransferFunds(uint fromAccountNumber, uint toAccountNumber, decimal amount)
        {
            (Transaction, Transaction) transactions;

            if (amount <= 0 || amount > 50000)
            {
                return new TransactionResponse(fromAccountNumber, InvalidAmountMessage);
            }

            try
            {
                transactions = await _repository.TransferFunds(fromAccountNumber, toAccountNumber, amount);
            }
            catch (Exception ex)
            {
                return new TransactionResponse(fromAccountNumber, ex.Message);
            }

            return new TransactionResponse(fromAccountNumber, transactions);
        }
    }
}
