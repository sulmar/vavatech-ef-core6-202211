using TableSplitting;
using TableSplitting.Models;

Console.WriteLine("Hello, Table Splitting!");

var contextFactory = new ContextFactory();
using var context = contextFactory.CreateDbContext(args);

if (context.Database.EnsureCreated())
{    
    context.Attachments.AddRange(GenerateAttachments());
    context.SaveChanges();
}

// TODO: get attachments with content


// TODO: get attachments with without content



static void Display(IEnumerable<Attachment> attachments)
{
    foreach (var attachment in attachments)
    {
        Console.WriteLine(attachment);
    }
}


static IEnumerable<Attachment> GenerateAttachments() => new List<Attachment>
{
    new Attachment { ContentType = "application/pdf",
        FileName = Path.GetFileName(@"Assets\hello-world.pdf"),
        Content = File.ReadAllBytes(@"Assets\hello-world.pdf"),
        Description = "Lorem ipsum"
    },

    new Attachment { ContentType = "image/png",
        FileName = Path.GetFileName(@"Assets\efcore.png"),
        Content = File.ReadAllBytes(@"Assets\efcore.png"),
        Description = "Lorem ipsum"
    },
};