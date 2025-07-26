using System.Security.Claims;
using System.Text;
using Finlyze.Api.Services.Jwt;
using Finlyze.Domain.ValueObjects.Enums;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace Finlyze.Api.Configurations.Builder;

public static class BuilderConfig
{
    public static WebApplicationBuilder RegisterConfig(this WebApplicationBuilder builder)
    {
        builder.Services.AddAuthentication(x =>
        {
            x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(x =>
        {
            x.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(JwtSettings.JwtKey)),
                ValidateIssuer = false,
                ValidateAudience = false
            };
        });

        builder.Services.AddAuthorization(options =>
        {
            options.AddPolicy("AdminOnly", policy =>
            policy.RequireAssertion(context =>
            {
                var roleClaim = context.User.FindFirst(ClaimTypes.Role);

                if (roleClaim is null || !int.TryParse(roleClaim.Value, out var rolesAsInt))
                    return false;

                var roles = (ERole)rolesAsInt;

                return roles.HasFlag(ERole.User) && roles.HasFlag(ERole.Admin);
            }));
        });


        builder.Services.Configure<ApiBehaviorOptions>(options => options.SuppressModelStateInvalidFilter = true);

        if (builder.Environment.IsDevelopment())
        {
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
        }

        builder.Services.AddHealthChecks();

        builder.Services.AddControllers();

        return builder;
    }
}