using DependencyInjection;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var connectionString = builder.Configuration.GetConnectionString("AppConnectionString");
// builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString));
builder.Services.AddDbContextPool<ApplicationDbContext>(options => options.UseSqlServer(connectionString));

var app = builder.Build();



// Configure the HTTP request pipeline.


app.MapGet("/", () => "Hello World!");

app.MapGet("/api/products", (ApplicationDbContext context) => context.Products.ToList());   

app.Run();

