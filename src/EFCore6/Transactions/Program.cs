using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Net.WebSockets;
using System.Transactions;
using Transactions;
using Transactions.Domain;
using Transactions.Models;
using Transactions.Repositories;

Console.WriteLine("Hello, Transactions!");

var contextFactory = new ContextFactory();
using var context = contextFactory.CreateDbContext(args);

if (context.Database.EnsureCreated())
{
    context.Accounts.AddRange(GenerateAccounts());
    context.SaveChanges();
}

// TODO: safe transfer money 100 PLN

// SingleTransaction(context);

// DistributedTransaction(context);

string connectionString = @"Server=(localdb)\mssqllocaldb;Database=TransactionsDb";
var options = new DbContextOptionsBuilder<ApplicationDbContext>()
    .UseSqlServer(connectionString).Options;

// MultiNativeTransaction(options);

SqlConnection connection;

SqlTransaction transaction;

ExternalTransaction(connectionString, options, out connection, out transaction);

static IEnumerable<Account> GenerateAccounts() => new List<Account>
{
    new Account { Owner = "John", Balance = 1000 },
    new Account { Owner = "Mark", Balance = 1000 }
};

static void SingleNativeTransaction(ApplicationDbContext context)
{
    decimal amount = 100;
    var transaction = context.Database.BeginTransaction(); // SQL: BEGIN TRANSACTION
    Console.WriteLine("Begin transaction");

    try
    {
        var sender = context.Accounts.Single(a => a.Owner == "John");
        sender.Balance -= amount;
        context.SaveChanges();

        var recipient = context.Accounts.Single(a => a.Owner == "Mark");
        recipient.Balance += amount;

        if (recipient.Balance > Account.BalanceLimit)
            throw new ApplicationException($"Balance over limit {Account.BalanceLimit}");

        context.SaveChanges();

        transaction.Commit(); // SQL: COMMIT
        Console.WriteLine("Commited transaction.");
    }
    catch (Exception e)
    {
        transaction.Rollback(); // SQL: ROLLBACK
        Console.WriteLine("Rollbacked transaction.");
    }


}


static void MultiNativeTransaction(DbContextOptions options)
{
    decimal amount = 100;

    using var senderContext = new ApplicationDbContext(options);


    var transaction = senderContext.Database.BeginTransaction(); // SQL: BEGIN TRANSACTION
    Console.WriteLine("Begin transaction");

    SqlConnection senderConnection = senderContext.Database.GetDbConnection() as SqlConnection;
    Console.WriteLine($"Sender Client Connection Id {senderConnection.ClientConnectionId}");



    try
    {
        var sender = senderContext.Accounts.Single(a => a.Owner == "John");
        sender.Balance -= amount;
        senderContext.SaveChanges();

        using var recipientContext = new ApplicationDbContext(options);

        // Przekazanie połączenia
        recipientContext.Database.SetDbConnection(senderConnection);

        SqlConnection recipientConnection = recipientContext.Database.GetDbConnection() as SqlConnection;
        Console.WriteLine($"Recipient Client Connection Id {recipientConnection.ClientConnectionId}");

        // Przekazanie transakcji
        recipientContext.Database.UseTransaction(transaction.GetDbTransaction());


        var recipient = recipientContext.Accounts.Single(a => a.Owner == "Mark");
        recipient.Balance += amount;

        if (recipient.Balance > Account.BalanceLimit)
            throw new ApplicationException($"Balance over limit {Account.BalanceLimit}");

        recipientContext.SaveChanges();

        transaction.Commit(); // SQL: COMMIT
        Console.WriteLine("Commited transaction.");
    }
    catch (Exception e)
    {
        transaction.Rollback(); // SQL: ROLLBACK
        Console.WriteLine("Rollbacked transaction.");
    }


}

static void DistributedTransaction(ApplicationDbContext context)
{
    decimal amount = 100;

    IAccountRepository accountRepository = new DbAccountRepository(context);

    // Transakcja rozproszona (Distributed Transaction)
    using (TransactionScope transaction = new TransactionScope())
    {
        try
        {
            accountRepository.Withdraw(amount, "John");
            accountRepository.Deposit(amount, "Mark");

            transaction.Complete();
        }
        catch (ApplicationException e)
        {
            Console.WriteLine("Rollbacked transaction.");
        }
    } // Commit lub Rollback zależnie od tego czy była wywołana metoda Complete()
}

static void ExternalTransaction(string connectionString, DbContextOptions<ApplicationDbContext> options, out SqlConnection connection, out SqlTransaction transaction)
{
    decimal amount = 100;
    connection = new SqlConnection(connectionString);
    connection.Open();
    transaction = connection.BeginTransaction();
    try
    {
        var command = connection.CreateCommand();
        command.CommandText = $"UPDATE Accounts SET Balance = Balance - {amount} WHERE Owner = 'John'";
        command.Transaction = transaction;
        command.ExecuteNonQuery();

        using var recipientContext = new ApplicationDbContext(options);
        recipientContext.Database.SetDbConnection(connection);
        recipientContext.Database.UseTransaction(transaction);

        var recipient = recipientContext.Accounts.Single(a => a.Owner == "Mark");
        recipient.Balance += amount;

        if (recipient.Balance > Account.BalanceLimit)
            throw new ApplicationException($"Balance over limit {Account.BalanceLimit}");

        recipientContext.SaveChanges();

        transaction.Commit();

    }
    catch (Exception e)
    {
        transaction.Rollback();
    }
}