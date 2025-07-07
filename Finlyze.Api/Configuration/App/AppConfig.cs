namespace Finlyze.Api.Configuration.App;

public static class AppConfig
{
    public static WebApplication RegisterConfig(this WebApplication app)
    {
        app.UseHttpsRedirection();
        app.UseRouting();
        app.UseAuthentication();
        app.UseAuthorization();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.MapControllers();

        return app;
    }
}