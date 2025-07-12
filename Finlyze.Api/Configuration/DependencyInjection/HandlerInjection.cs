using Finlyze.Api.Controller.Users;
using Finlyze.Application.Abstract.Interface;
using Finlyze.Application.Abstract.Interface.Handler;
using Finlyze.Infrastructure.Implementation.Interfaces.Handler;

namespace Finlyze.Api.Configuration.DependencyInjection;

public static class HandlerInjection
{
    public static IServiceCollection RegisterHandlers(this IServiceCollection service)
    {
        service.AddScoped<ICreateUserAccountHandler, CreateUserAccountHandler>();
        service.AddScoped<IDeleteAccountHandler, DeleteUserAccountHandler>();
        service.AddScoped<ILoginUserAccountHandler, LoginUserAccountHandler>();
        service.AddScoped<IUpdateUserAccountHandler, UpdateUserAccountHandler>();
        service.AddScoped<ICreateAdminUserAccountHandler, CreateAdminUserAccountHandler>();
        service.AddScoped<ICreateTransactionHandler, CreateTransactionHandler>();

        return service;
    }
}