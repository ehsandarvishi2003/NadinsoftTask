using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using NadinsoftTask.Models.Entity;

namespace NadinsoftTask.Models.DataBase
{
    public class DatabaseContext:DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options)
            : base(options)
        {

        }

        #region DbSet
        public DbSet<Product> Products { get; set; }

        #endregion

    }
}
