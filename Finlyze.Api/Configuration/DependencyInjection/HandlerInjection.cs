using Finlyze.Application.Abstracts.Interfaces.Handlers;
using Finlyze.Infrastructure.Implementations.Interfaces.Handlers;

namespace Finlyze.Api.Configurations.DependencyInjection;

public static class HandlerInjection
{
    public static IServiceCollection RegisterHandlers(this IServiceCollection service)
    {
        service.AddScoped<ICreateUserAccountHandler, CreateUserAccountHandler>();
        service.AddScoped<IDeleteAccountHandler, DeleteUserAccountHandler>();
        service.AddScoped<ILoginUserAccountHandler, LoginUserAccountHandler>();
        service.AddScoped<IUpdateUserAccountHandler, UpdateUserAccountHandler>();
        service.AddScoped<ICreateAdminUserAccountHandler, CreateAdminUserAccountHandler>();
        service.AddScoped<ICreateFinancialTransactionHandler, CreateFinancialTransactionHandler>();
        service.AddScoped<IDeleteFinancialTransactionHandler, DeleteFinancialTransactionHandler>();

        return service;
    }
}