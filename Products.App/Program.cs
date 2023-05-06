using HotChocolate.Execution.Batching;
using Products.App.Products;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration
    .AddJsonFile("appsettings.json")
    .AddJsonFile($"appsetting.{builder.Environment.EnvironmentName}.json", true)
    .AddEnvironmentVariables();

builder.Services
    .AddSingleton<ProductRepository>()
    .AddGraphQLServer()
    .AddQueryType(q => q.Name("Query"))
        .AddType<ProductQueries>()
    .AddMutationType(m => m.Name("Mutation"))
        .AddType<ProductMutations>();

var app = builder.Build();

app.MapGraphQL("");
app.Run();