using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace NadinsoftTask.Models.DataBase
{
    public class DatabaseContext:DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=EHSAN;Initial Catalog=NadinsoftTask;Integrated Security=True;MultipleActiveResultSets=true;TrustServerCertificate=True");
            base.OnConfiguring(optionsBuilder);
        }
        public DbSet<Product> Products { get; set; }
    }
}
