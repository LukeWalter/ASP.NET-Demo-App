using Microsoft.EntityFrameworkCore;

namespace FirstApp.Data
{
    public class ExampleDbContext(DbContextOptions<ExampleDbContext> options) : DbContext(options)
    {
        public DbSet<Example> Examples => Set<Example>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Example>().HasData(
                new Example
                {
                    Id = 1,
                    Label = "Example 1"
                },
                new Example
                {
                    Id = 2,
                    Label = "Example 2"
                },
                new Example
                {
                    Id = 3,
                    Label = "Example 3"
                },
                new Example
                {
                    Id = 4,
                    Label = "Example 4"
                }
            );
        }
    }
}
