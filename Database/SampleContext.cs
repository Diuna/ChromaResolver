using ChromaResolver.Models.ECM;
using Microsoft.EntityFrameworkCore;

namespace ChromaResolver.Database
{
    public class SampleContext : DbContext
    {

        public DbSet<BaseSample> Samples { get; set; }

        private string _path = @"C:\Temp\test.db";

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite($"Data Source={_path}");
        }
    }
}
