using Stitcher.App;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration
    .AddJsonFile("appsettings.json")
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", true)
    .AddEnvironmentVariables();

var sources = builder.Configuration.GetSection("GraphQL:Sources").Get<List<RemoteSchemaSource>>()
    ?? new List<RemoteSchemaSource>();

var gqlServer = builder.Services
    .AddGraphQLServer()
    .ModifyRequestOptions(opt => opt.IncludeExceptionDetails = true);

foreach (var source in sources)
{
    builder.Services.AddHttpClient(source.Name, c => c.BaseAddress = new Uri(source.Url));
    gqlServer.AddRemoteSchema(source.Name);
}

gqlServer.AddTypeExtensionsFromFile("./Stitching.graphql");

var app = builder.Build();

app.MapGraphQL("");
app.Run();