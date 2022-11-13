

using Bogus;
using Microsoft.EntityFrameworkCore;
using UserDefinedFunctionMapping;
using UserDefinedFunctionMapping.Models;

Console.WriteLine("Hello, UserDefinedFunctionMapping!");

var contextFactory = new ContextFactory();
using var context = contextFactory.CreateDbContext(args);

if (context.Database.EnsureCreated())
{

    context.Database.ExecuteSqlRaw(@"
           CREATE FUNCTION dbo.CommentedPostCountForBlog(@id int)
            RETURNS int
            AS
            BEGIN
                RETURN (SELECT COUNT(*)
                    FROM [Posts] AS [p]
                    WHERE ([p].[BlogId] = @id) AND ((
                        SELECT COUNT(*)
                        FROM [Comments] AS [c]
                        WHERE [p].[PostId] = [c].[PostId]) > 0));
            END
    ");

    var commentFaker = new Faker<Comment>()
        .RuleFor(p => p.Content, f => f.Lorem.Sentences(3))
        .RuleFor(p => p.Author, f => f.Person.FullName);

    var postFaker = new Faker<Post>()
        .RuleFor(p => p.Content, f => f.Lorem.Text())
        .RuleFor(p => p.Comments, f => commentFaker.GenerateBetween(0, 10));


    var blogFaker = new Faker<Blog>()
        .RuleFor(p => p.Title, f => f.Lorem.Sentence())
        .RuleFor(p => p.Posts, f => postFaker.Generate(20));


    var blogs = blogFaker.Generate(50);

    context.Set<Blog>().AddRange(blogs);
    context.SaveChanges();
}



