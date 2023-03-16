using Microsoft.EntityFrameworkCore;
using testAPI.Datatables;

namespace testAPI.Context
{
    public class DemoContext : DbContext
    {
        public DemoContext() 
        {
            
        }
        public DbSet<SysDemoUser> SysDemoUsers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
                                                                         .AddJsonFile("appsettings.json", optional: true, reloadOnChange: false)
                                                                         .Build();

            var connectionString = configuration["ConnectionStrings:Core.Framework"];
            optionsBuilder.UseSqlServer(connectionString);
        }
    }
}
