using Aptude.Entities;
using Microsoft.EntityFrameworkCore;

namespace Aptude.Data
{
    public partial class AptudeDbContext : DbContext
    {
        public AptudeDbContext()
        { }

        public AptudeDbContext(DbContextOptions<AptudeDbContext> options)
            : base(options)
        { }

        #region Tables
        public virtual DbSet<Employee> Employees { get; set; }
        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
