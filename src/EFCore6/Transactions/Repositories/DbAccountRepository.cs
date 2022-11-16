using Transactions.Domain;
using Transactions.Models;

namespace Transactions.Repositories
{
    internal class DbAccountRepository : IAccountRepository
    {
        private readonly ApplicationDbContext context;

        public DbAccountRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public void Deposit(decimal amount, string owner)
        {
            var recipient = context.Accounts.Single(a => a.Owner == owner);
            recipient.Balance += amount;

            if (recipient.Balance > Account.BalanceLimit)
                throw new ApplicationException($"Balance over limit {Account.BalanceLimit}");

            context.SaveChanges();
        }

        public void Withdraw(decimal amount, string owner)
        {
            var sender = context.Accounts.Single(a => a.Owner == owner);
            sender.Balance -= amount;
            context.SaveChanges();
        }
    }
}
