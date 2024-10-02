using BankAccountApi.Data;
using BankAccountApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace BankAccountApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IAccountsRepository _repository;

        public AccountsController(IDataInitialisationService dataInitialisationService, IAccountsRepository repository)
        {
            _repository = repository;
            dataInitialisationService.InitialiseDataAsync().Wait();
        }

        // GET: api/Accounts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Account>>> GetAccounts()
        {
            var accounts = await _repository.GetAccounts();
            return accounts.ToList();
        }

        // GET: api/Accounts/12345678
        [HttpGet("{accountNumber}")]
        public async Task<ActionResult<Account>> GetAccount(uint accountNumber)
        {
            var account = await _repository.GetAccount(accountNumber);

            if (account is null)
            {
                return NotFound();
            }

            return account;
        }

        // POST: api/Accounts
        [HttpPost]
        public async Task<ActionResult<AccountResponse>> PostAccount(AccountDetails accountDetails)
        {
            Account account;

            try
            {
                account = await _repository.AddAccount(accountDetails);
            }
            catch (Exception ex)
            {
                return new AccountResponse(accountDetails.AccountNumber, ex.Message);
            }

            return new AccountResponse(accountDetails.AccountNumber, account);
        }

        // PUT: api/Accounts/12345678
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{accountNumber}")]
        public async Task<ActionResult<AccountResponse>> PutAccount(uint accountNumber, bool isActive)
        {
            Account? account;

            try
            {
                account = await _repository.UpdateAccount(accountNumber, isActive);
            }
            catch (Exception ex)
            {
                return new AccountResponse(accountNumber, ex.Message);
            }

            if (account is null)
            {
                return NotFound();
            }

            return new AccountResponse(accountNumber, account);
        }

        // DELETE: api/Accounts/12345678
        [HttpDelete("{accountNumber}")]
        public async Task<ActionResult<AccountResponse>> DeleteAccount(uint accountNumber)
        {
            bool isDeleted;

            try
            {
                isDeleted = await _repository.DeleteAccount(accountNumber);
            }
            catch (Exception ex)
            {
                return new AccountResponse(accountNumber, ex.Message);
            }

            if (!isDeleted)
            {
                return NotFound();
            }

            return new AccountResponse(accountNumber);
        }
    }
}
