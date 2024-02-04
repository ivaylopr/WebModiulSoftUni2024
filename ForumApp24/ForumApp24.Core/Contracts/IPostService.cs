using ForumApp24.Core.Models;

namespace ForumApp24.Core.Contracts
{
    public interface IPostService
    {
        Task AddAssync(PostModel model);
		Task EditAsync(PostModel model);
		Task<IEnumerable<PostModel>> GetAllPostsAsync();
		Task<PostModel?> GetByIdAsync(int id);
	}
}
