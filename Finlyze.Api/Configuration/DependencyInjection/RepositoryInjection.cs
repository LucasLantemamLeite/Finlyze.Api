using Finlyze.Application.Abstracts.Interfaces.Repositories;
using Finlyze.Infrastructure.Implementations.Interfaces.Repositories;

namespace Finlyze.Api.Configurations.DependencyInjection;

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