using Microsoft.EntityFrameworkCore;
using HWLinq.Models;

namespace HWLinq.Data
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Product> Products { get; set; }

        private readonly string connectionString;
        public ApplicationContext(string connectionString)
        {
            this.connectionString = connectionString;

            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseNpgsql("Server=localhost; Database = scaffolding; User ID = postgres; Password = postgres;");
            optionsBuilder.UseNpgsql(connectionString);
        }
    }
}