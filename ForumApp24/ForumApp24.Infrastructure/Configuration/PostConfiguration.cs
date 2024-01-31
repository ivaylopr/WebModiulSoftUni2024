using ForumApp24.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ForumApp24.Infrastructure.Configuration
{
    public class PostConfiguration : IEntityTypeConfiguration<Post>
    {
        private Post[] initialPosts = new Post[]
        {
            new Post { Id = 1, Title = "First Title",Content="First post content",},
            new Post { Id = 2, Title = "Second Title",Content="Second post content",},
            new Post { Id = 3, Title = "Third Title",Content="Third post content",}
        };
        public void Configure(EntityTypeBuilder<Post> builder)
        {
            builder.HasData(initialPosts);
        }
    }
}
