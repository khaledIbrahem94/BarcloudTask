using BarcloudTask.DataBase.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace BarcloudTask.DataBase
{
    public partial class DBContext : DbContext
    {
        public DBContext() { }

        public DBContext(DbContextOptions<DbContext> options) : base(options) { }

        public DbSet<Client> Clients { get; set; }
        public DbSet<StockData> StockData { get; set; }
        public DbSet<ErrorsLog> ErrorsLog { get; set; }
        public DbSet<StockSymbol> StockSymbols { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var config = new ConfigurationBuilder()
               .SetBasePath(Directory.GetCurrentDirectory())
               .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
               .Build();

            optionsBuilder.UseSqlServer(config.GetConnectionString("DefaultConnection"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<StockSymbol>().HasData(
               new StockSymbol { Id = 1, Symbol = "AAPL" },
               new StockSymbol { Id = 2, Symbol = "MSFT" },
               new StockSymbol { Id = 3, Symbol = "GOOGL" },
               new StockSymbol { Id = 4, Symbol = "AMZN" }
           );

        }
    }
}
