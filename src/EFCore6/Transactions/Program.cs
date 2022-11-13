using Transactions;
using Transactions.Models;

Console.WriteLine("Hello, Transactions!");

var contextFactory = new ContextFactory();
using var context = contextFactory.CreateDbContext(args);

if (context.Database.EnsureCreated())
{
    context.Accounts.AddRange(GenerateAccounts());
    context.SaveChanges();
}

// TODO: safe transfer money 100 PLN





static IEnumerable<Account> GenerateAccounts() => new List<Account>
{
    new Account { Owner = "John", Balance = 1000 },
    new Account { Owner = "Mark", Balance = 1000 }
};