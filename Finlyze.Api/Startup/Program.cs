using Finlyze.Api.Configurations.App;
using Finlyze.Api.Configurations.Builder;
using Finlyze.Api.Configurations.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

builder.RegisterConfig();

builder.Services
    .RegisterRepositories()
    .RegisterQueries()
    .RegisterHandlers()
    .RegisterServices(builder.Configuration);

var app = builder.Build();

app.RegisterConfig();

app.Run();
