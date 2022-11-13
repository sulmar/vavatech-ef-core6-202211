Console.WriteLine("Hello, Shared Types!");

// TODO: save PropertyBag to database

// TODO: get PropertyBag


static Dictionary<string, object> GeneratePropertyBag()
{
    return new Dictionary<string, object>
    {
        { "Name", "Atari" },
        { "Price", 1000m },
        { "Color", "Brown" },
    };
}
