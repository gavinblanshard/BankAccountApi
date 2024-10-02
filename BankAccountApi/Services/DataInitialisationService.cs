using BankAccountApi.Data;
using Microsoft.EntityFrameworkCore;

namespace BankAccountApi.Services
{
    public class DataInitialisationService(AppDbContext context) : IDataInitialisationService
    {
        public async Task InitialiseDataAsync()
        {
            // required to ensure the in-memory database has been created and seed data added
            await context.Database.EnsureCreatedAsync();

            // add initial data
            if (!await context.AccountTypes.AnyAsync())
            {
                context.AccountTypes.AddRange(
                     new AccountType { Id = Guid.NewGuid(), Name = "Current" },
                     new AccountType { Id = Guid.NewGuid(), Name = "Savings" }
                );
                await context.SaveChangesAsync();
            }
        }
    }
}
