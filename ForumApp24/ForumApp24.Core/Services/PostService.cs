using ForumApp24.Core.Contracts;
using ForumApp24.Core.Models;
using ForumApp24.Infrastructure.Data;
using ForumApp24.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ForumApp24.Core.Services
{
	public class PostService : IPostService
    {
        private readonly ForumDbContext context;
        
        public PostService(
            ForumDbContext _context
            )
        {
            context = _context;
            
        }

        public async Task AddAssync(PostModel model)
        {
            var entity = new Post()
            {
                Title = model.Title,
                Content = model.Content,
            };
            
				await context.AddAsync(entity);
				await context.SaveChangesAsync();
			
           
        }

		public async Task DeleteAsync(int id)
		{
			var entity = await context.FindAsync<Post>(id);
			if (entity == null)
			{
				throw new ApplicationException("Invalid post");
			}
            context.Remove(entity);
            await context.SaveChangesAsync();
		}

		

		public async Task EditAsync(PostModel model)
		{
            var entity = await context.FindAsync<Post>(model.Id);
            if (entity == null)
            {
                throw new ApplicationException("Invalid post");
            }
            entity.Title = model.Title;
            entity.Content = model.Content;
            await context.SaveChangesAsync();
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

		public async Task<PostModel?> GetByIdAsync(int id)
		{
            return await context.Posts.Where(p => p.Id == id).Select(p => new PostModel()
            {
                Id = p.Id,
                Title = p.Title,
                Content = p.Content
            }).AsNoTracking().FirstOrDefaultAsync();
		}
	}
}
