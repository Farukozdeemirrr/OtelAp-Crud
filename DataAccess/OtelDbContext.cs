using Microsoft.EntityFrameworkCore;
using Entities;
using Microsoft.Extensions.Configuration;

namespace DataAccess
{
    public class OtelDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            IConfigurationBuilder configBuilder = new ConfigurationBuilder().AddJsonFile("appSettings.json");
            IConfigurationRoot configuration = configBuilder.Build();
            optionsBuilder.UseNpgsql(configuration.GetConnectionString("DefaultConnection"));
        }

        public DbSet<Otel> Otels { get; set; }
        public DbSet<Garage> Garages { get; set; }

        public DbSet<Person> Persons { get; set; }


    }
}
