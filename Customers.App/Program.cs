using Customers.App.Customers;
using HotChocolate.AspNetCore;
using HotChocolate.Execution.Batching;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration
    .AddJsonFile("appsettings.json")
    .AddJsonFile($"appsetting.{builder.Environment.EnvironmentName}.json", true)
    .AddEnvironmentVariables();

builder.Services
    .AddSingleton<CustomersRepository>()
    .AddGraphQLServer()
    .AddQueryType(q => q.Name("Query"))
    .AddType<CustomerQueries>()
    .AddMutationType(m => m.Name("Mutation"))
    .AddType<CustomerMutations>();

var app = builder.Build();

app.MapGraphQL("")
    .WithOptions(new GraphQLServerOptions
    {
        EnableBatching = true
    });
app.Run();