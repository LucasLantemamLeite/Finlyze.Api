using Finlyze.Api.Configuration.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .RegisterQueries()
    .RegisterRepositories()
    .RegisterServices(builder.Configuration);

var app = builder.Build();

app.Run();
