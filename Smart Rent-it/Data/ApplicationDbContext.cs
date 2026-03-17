
    using global::Smart_Rent_it.Models;
    using Microsoft.EntityFrameworkCore;
    using Smart_Rent_it.Models; 

    namespace Smart_Rent_it.Data
    {
        public class ApplicationDbContext : DbContext
        {
            public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
                : base(options)
            {
            }

            public DbSet<Product> Products { get; set; }
            public DbSet<User> Users { get; set; }

            protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
                base.OnModelCreating(modelBuilder);

               
            }
        }
    }

