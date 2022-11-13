

using OwnedEntityTypes;
using OwnedEntityTypes.Models;

Console.WriteLine("Hello, Owned Entity Types!");

var contextFactory = new ContextFactory();
using var context = contextFactory.CreateDbContext(args);


if (context.Database.EnsureCreated())
{
    context.Customers.AddRange(GenerateCustomers());
    context.SaveChanges();
}





static IEnumerable<Customer> GenerateCustomers()
{

    var customers = new List<Customer>
    {
        new Customer
        {
            FirstName = "John", LastName = "Smith",
            InvoiceAddress = new Address
            {
                City = "Warsaw",
                Country = "Poland",
                Street = "Puławska",
                ZipCode = "01-001"
            },

            Location = new Coordinate(52.05, 25.04),

            ShipAddress = new Address
            {
                City = "Warsaw",
                Country = "Poland",
                Street = "Puławska",
                ZipCode = "01-001"
            },
        },

        new Customer
        {
            FirstName = "Jack",
            LastName = "London",

            Location = new Coordinate(51.95, 24.14),

            InvoiceAddress = new Address
            {
                City = "Bydgoszcz",
                Country = "Poland",
                Street = "Dworcowa",
                ZipCode = "85-138"
            },

            ShipAddress = new Address
            {
                City = "Poznań",
                Country = "Poland",
                Street = "Grunwaldzka",
                ZipCode = "60-166"
            }
        },


    };

    return customers;

}