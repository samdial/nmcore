using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

public class Startup
{
    public IConfiguration Configuration { get; }

    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        var postgresConnectionString = Configuration.GetValue<string>(EnvironmentVariables.POSTGRES_CONNECTION_STRING);
        if (postgresConnectionString is null) throw new ApplicationException();
        DbContextOptionsBuilder<CoreContext> dbBuilder = new DbContextOptionsBuilder<CoreContext>().UseNpgsql(postgresConnectionString);

        services.AddSingleton(x => dbBuilder);
        services.AddDbContext<CoreContext>(x => x.UseNpgsql(postgresConnectionString));
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
        {
            var context = serviceScope.ServiceProvider.GetRequiredService<CoreContext>();
            context.Database.Migrate();
        }

        app.UseHttpsRedirection();
        app.UseRouting();
        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}

