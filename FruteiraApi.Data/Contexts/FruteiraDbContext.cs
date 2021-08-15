using FruteiraApi.Data.Repositories;
using Microsoft.EntityFrameworkCore;

namespace FruteiraApi.Data.Contexts
{
    public class FruteiraDbContext : DbContext
    {
        #region FruteiraDbContext
        public FruteiraDbContext(DbContextOptions<FruteiraDbContext> options) : base(options)
        {

        }
        #endregion

        public DbSet<Frutas> Frutas { get; set; }
    }
}
