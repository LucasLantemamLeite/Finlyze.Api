using Finlyze.Application.Abstract.Interface;
using Finlyze.Infrastructure.Implementation.Interfaces;
using Finlyze.Infrastructure.Implementation.Interfaces.Repository;

namespace Finlyze.Api.Configuration.DependencyInjection;

public static class RepositoryInjection
{
    public static IServiceCollection RegisterRepositories(this IServiceCollection service)
    {
        service.AddScoped<IUserAccountRepository, UserAccountRepository>();
        service.AddScoped<IFinancialTransactionRepository, FinancialTransactionRepository>();
        service.AddScoped<ISystemLogRepository, SystemLogRepository>();

        return service;
    }
}