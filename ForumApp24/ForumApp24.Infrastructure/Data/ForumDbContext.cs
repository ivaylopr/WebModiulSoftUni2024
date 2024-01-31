using ForumApp24.Infrastructure.Configuration;
using ForumApp24.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace ForumApp24.Infrastructure.Data
{
    public class ForumDbContext : DbContext 
    {
        public ForumDbContext(DbContextOptions<ForumDbContext> options)
            : base(options) 
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
