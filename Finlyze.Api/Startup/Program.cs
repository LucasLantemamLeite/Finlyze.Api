using Finlyze.Api.Configuration.App;
using Finlyze.Api.Configuration.Builder;
using Finlyze.Api.Configuration.DependencyInjection;

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
