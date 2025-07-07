using Microsoft.AspNetCore.Mvc;

namespace Finlyze.Api.Configuration.Builder;

public static class BuilderConfig
{
    public static WebApplicationBuilder RegisterConfig(this WebApplicationBuilder builder)
    {
        // builder.Services.AddAuthentication();
        // builder.Services.AddAuthorization(); 

        builder.Services.Configure<ApiBehaviorOptions>(options => options.SuppressModelStateInvalidFilter = true);

        if (builder.Environment.IsDevelopment())
        {
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
        }

        builder.Services.AddControllers();

        return builder;
    }
}