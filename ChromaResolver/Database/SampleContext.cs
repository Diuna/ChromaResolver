using ChromaResolver.Models.ECM;
using Microsoft.EntityFrameworkCore;

namespace ChromaResolver.Database
{
    public class SampleContext : DbContext
    {
        public DbSet<Sample> Samples { get; set; }

        public DbSet<BaseElement> BaseElements { get; set; }

        private string _path = @"C:\Temp\test.db";

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite($"Data Source={_path}");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BaseElement>()
                .HasKey(e => e.Guid);
        }
    }
}
