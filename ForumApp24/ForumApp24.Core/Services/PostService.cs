using ForumApp24.Core.Contracts;
using ForumApp24.Core.Models;
using ForumApp24.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics.Internal;

namespace ForumApp24.Core.Services
{
    public class PostService : IPostService
    {
        private readonly ForumDbContext context;
        public PostService(ForumDbContext _context)
        {
            context = _context;
        }

        public async Task<IEnumerable<PostModel>> GetAllPostsAsync()
        {
            return await context.Posts.AsNoTracking()
                .Select(p => new PostModel()
                {
                    Id = p.Id,
                    Title = p.Title,
                    Content = p.Content
                }).ToListAsync();
        }
    }
}
