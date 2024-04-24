using Microsoft.EntityFrameworkCore;

namespace Hasin.Infrastructure.DbContexts
{
    public class HasinDbContext : DbContext
    {
        public HasinDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(HasinDbContext).Assembly);
        }

        public virtual async Task<int> SaveChangesAsync()
        {
            try
            {
                /*do something*/
                var result = await base.SaveChangesAsync();

                return result;
            }
            catch (Exception exp)
            {

                throw exp;
            }
        }

    }
}
