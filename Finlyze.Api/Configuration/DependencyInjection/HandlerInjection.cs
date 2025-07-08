using Finlyze.Application.Abstract.Interface;
using Finlyze.Infrastructure.Implementation.Interfaces.Handler;

namespace Finlyze.Api.Configuration.DependencyInjection;

public static class HandlerInjection
{
    public static IServiceCollection RegisterHandlers(this IServiceCollection service)
    {
        service.AddScoped<ICreateUserAccountHandler, CreateUserAccountHandler>();
        service.AddScoped<IDeleteAccountHandler, DeleteUserAccountHandler>();
        service.AddScoped<ILoginUserAccountHandler, LoginUserAccountHandler>();

        return service;
    }
}