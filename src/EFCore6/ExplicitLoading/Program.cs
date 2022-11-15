
using Bogus;
using ExplicitLoading;
using ExplicitLoading.Models;


Console.WriteLine("Hello, Explicit Loading!");

var contextFactory = new ContextFactory();
using var context = contextFactory.CreateDbContext(args);

var customers = GenerateCustomers(10);
// var blogs = GenerateBlogs(5);

if (context.Database.EnsureCreated())
{
    context.Customers.AddRange(customers);


    context.SaveChanges();
}

var query = context.Customers.ToList();


foreach (var customer in query)
{
    Console.WriteLine($"Processing {customer.Name}...");

    context.Entry(customer).Reference(c => c.AccountManager).Load();

    Console.WriteLine($"{customer.AccountManager.FirstName} {customer.AccountManager.LastName}");

    context.Entry(customer).Collection(c=>c.Orders).Load();

    foreach (var order in customer.Orders)
    {
        Console.WriteLine(order.TotalAmount);
    }

    


}



static Faker<Employee> GetEmployeeFaker() => new Faker<Employee>()
    .RuleFor(p => p.FirstName, f => f.Person.FirstName)
    .RuleFor(p => p.LastName, f => f.Person.LastName);

static Faker<Bill> GetBillFaker() => new Faker<Bill>()
    .RuleFor(p => p.DateOfPayment, f => f.Date.Past());

static Faker<Order> GetOrderFaker() => new Faker<Order>()
    .RuleFor(p => p.DueDate, f => f.Date.Soon())
    .RuleFor(p => p.TotalAmount, f => decimal.Parse(f.Commerce.Price()))
    .RuleFor(p => p.Paid, f => f.Random.Bool(0.7f))
    .RuleFor(p => p.Bill, f => GetBillFaker().Generate());

static IEnumerable<Customer> GenerateCustomers(int count) => new Faker<Customer>()
    .RuleFor(p => p.Name, f => f.Company.CompanyName())
    .RuleFor(p => p.AccountManager, f => GetEmployeeFaker().Generate())
    .RuleFor(p => p.Orders, f => GetOrderFaker().GenerateBetween(1, 10))
    .Generate(count);

static Faker<ExplicitLoading.Models.Person> GetPersonFaker() => new Faker<ExplicitLoading.Models.Person>()
    .RuleFor(p => p.FirstName, f => f.Person.FirstName)
    .RuleFor(p => p.LastName, f => f.Person.LastName);

static Faker<Post> GetPostFaker() => new Faker<Post>()
    .RuleFor(p => p.Slug, f => f.Internet.Url())
    .RuleFor(p => p.Content, f => f.Lorem.Paragraphs())
    .RuleFor(p => p.Rating, f => f.Random.Byte(0, 5))
    .RuleFor(p => p.Author, GetPersonFaker().Generate());

static IEnumerable<Blog> GenerateBlogs(int count) => new Faker<Blog>()
    .RuleFor(p => p.Title, f => f.Lorem.Sentence())
    .RuleFor(p => p.Owner, f => GetPersonFaker().Generate())
    .RuleFor(p => p.Posts, f => GetPostFaker().GenerateBetween(1, 20))
    .Generate(count);