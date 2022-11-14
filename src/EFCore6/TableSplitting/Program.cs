using Microsoft.EntityFrameworkCore;
using TableSplitting;
using TableSplitting.Models;

Console.WriteLine("Hello, Table Splitting!");

var contextFactory = new ContextFactory();
using var context = contextFactory.CreateDbContext(args);

 context.Database.EnsureDeleted();

if (context.Database.EnsureCreated())
{
    // context.Attachments.AddRange(GenerateAttachments());
    context.AttachmentHeaders.AddRange(GenerateAttachmentHeaders());
    context.SaveChanges();
}


// TODO: get attachments with without content
var query = context.AttachmentHeaders.TagWith("get attachments with without content").ToList();

// var query2 = context.AttachmentHeaders.Include(p => p.Content).ToList();

// TODO: get attachments with content

var attachmentHeader = context.AttachmentHeaders.Find(1);
attachmentHeader.FileName = "File1.pdf";
context.SaveChanges();

var attachmentContent = context.AttachmentContents.Find(1);

Console.WriteLine(attachmentContent);

attachmentContent.FileName = "Plik.pdf";

context.SaveChanges();


Console.WriteLine("Press any key to exit.");
Console.ReadKey();




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
        Title = "Lorem",
        FileName = Path.GetFileName(@"Assets\hello-world.pdf"),
        Content = File.ReadAllBytes(@"Assets\hello-world.pdf"),
        Description = "Lorem ipsum"
    },

    new Attachment { ContentType = "image/png",
        Title = "Ipsum",
        FileName = Path.GetFileName(@"Assets\efcore.png"),
        Content = File.ReadAllBytes(@"Assets\efcore.png"),
        Description = "Lorem ipsum"
    },
};

static IEnumerable<AttachmentHeader> GenerateAttachmentHeaders() => new List<AttachmentHeader>
{
    new AttachmentHeader { 
        Title = "Lorem",
        FileName = Path.GetFileName(@"Assets\hello-world.pdf"),        
        Description = "Lorem ipsum",
        Content = new AttachmentContent
        {
            ContentType = "application/pdf",
            Content = File.ReadAllBytes(@"Assets\hello-world.pdf"),
            FileName = Path.GetFileName(@"Assets\hello-world.pdf"),
        }
    },

    new AttachmentHeader { 
        Title = "Ipsum",
        FileName = Path.GetFileName(@"Assets\efcore.png"),        
        Description = "Lorem ipsum",
        Content = new AttachmentContent
        {
             ContentType = "image/png",
             Content = File.ReadAllBytes(@"Assets\efcore.png"),
             FileName = Path.GetFileName(@"Assets\efcore.png"),
        }
    },
};