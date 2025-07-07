using System.Data;
using Finlyze.Api.Services.Jwt;
using Finlyze.Application.Abstract.Interface;
using Finlyze.Infrastructure.Implementation.Interfaces.Handler;
using Finlyze.Migrations.Data.Context;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Finlyze.Api.Configuration.DependencyInjection;

public static class ServiceInjection
{
    public static IServiceCollection RegisterServices(this IServiceCollection service, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");

        service.AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionString));
        service.AddScoped<IDbConnection>(sp => new SqlConnection(connectionString));
        JwtSettings.JwtKey = configuration["JwtSettings:JwtKey"] ?? throw new Exception("JwtKey não encontrada");

        service.AddScoped<ICreateUserAccountHandler, CreateUserAccountHandler>();
        service.AddScoped<ILoginUserAccountHandler, LoginUserAccountHandler>();

        return service;
    }
}