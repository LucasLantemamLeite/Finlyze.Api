using Finlyze.Application.Abstract.Interface;
using Finlyze.Infrastructure.Implementation.Interfaces.Query;

namespace Finlyze.Api.Configuration.DependencyInjection;

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