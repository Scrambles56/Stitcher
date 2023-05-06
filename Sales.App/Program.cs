using HotChocolate.Execution.Batching;
using Sales.App.Sales;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration
    .AddJsonFile("appsettings.json")
    .AddJsonFile($"appsetting.{builder.Environment.EnvironmentName}.json", true)
    .AddEnvironmentVariables();

builder.Services
    .AddSingleton<SalesRepository>()
    .AddGraphQLServer()
    .AddType<ExportDirectiveType>()
    .AddQueryType(q => q.Name("Query"))
        .AddType<SalesQueries>()
    .AddMutationType(m => m.Name("Mutation"))
        .AddType<SalesMutations>();

var app = builder.Build();

app.MapGraphQL("");
app.Run();