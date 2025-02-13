using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;


namespace ClientService.Infrastructure.Data
{
    public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            var config = new Microsoft.Extensions.Configuration.ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory()) 
                .AddJsonFile("ClientService.WebAPI/appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            var connectionString = config.GetConnectionString("DefaultConnection");

            optionsBuilder.UseOracle(connectionString);

            return new AppDbContext(optionsBuilder.Options, null, config);
        }
    }
}
