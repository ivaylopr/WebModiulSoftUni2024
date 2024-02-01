using ForumApp24.Infrastructure.Data.Models;
using ForumApp24.Infrastructure.Data.Models.Configuration;
using Microsoft.EntityFrameworkCore;

namespace ForumApp24.Infrastructure.Data
{
    public class ForumDbContext : DbContext
    {
        public ForumDbContext(DbContextOptions<ForumDbContext> options)
            :base(options)
        {
            
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration<Post>(new PostConfiguration());
            base.OnModelCreating(modelBuilder);
        }
        public DbSet<Post> Posts { get; set; }
    }
}
