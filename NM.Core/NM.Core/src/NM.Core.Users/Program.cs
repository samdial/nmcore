using NM.Core.NM.Core.Users;

internal class Program
{
    private static void Main(string[] args)
    {
        CreateHostBuilder(args).Build().Run();
    }

    private static IHostBuilder CreateHostBuilder(string[] args)
    {
        return Host.CreateDefaultBuilder(args)
            .ConfigureAppConfiguration(x =>
            {
                x.SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile($"appsettings.keycloak.json", optional: true)
                    .AddEnvironmentVariables()
                    .AddCommandLine(args)
                    .Build();
                x.Build();
            })
            .ConfigureWebHostDefaults(builder => builder.UseStartup<Startup>());
    }
}