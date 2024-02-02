using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NM.Core.src.NM.Core.Users.Services;
using NM.Core.src.NM.Core.Users.Services.Interfaces;

namespace NM.Core.NM.Core.Users{
public class Startup
{
    public IConfiguration Configuration { get; }

    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddScoped<IUserService, UserService>();
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        var postgresConnectionString = "Host=localhost;Port=5432;Database=postgres;User ID=postgres;Password=3452;";

            if (postgresConnectionString is null) throw new ApplicationException();
        DbContextOptionsBuilder<ApplicationContext> dbBuilder = new DbContextOptionsBuilder<ApplicationContext>().UseNpgsql(postgresConnectionString);

        services.AddSingleton(x => dbBuilder);
        services.AddDbContext<ApplicationContext>(x => x.UseNpgsql(postgresConnectionString));
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
            var context = serviceScope.ServiceProvider.GetRequiredService<ApplicationContext>();
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
}}