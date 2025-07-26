using Finlyze.Application.Abstracts.Interfaces.Queries;
using Finlyze.Infrastructure.Implementations.Interfaces.Queries;

namespace Finlyze.Api.Configurations.DependencyInjection;

public static class QueryInjection
{
    public static IServiceCollection RegisterQueries(this IServiceCollection service)
    {
        service.AddScoped<IUserAccountQuery, UserAccountQuery>();
        service.AddScoped<IFinancialTransactionQuery, FinancialTransactionQuery>();
        service.AddScoped<ISystemLogQuery, SystemLogQuery>();

        return service;
    }
}