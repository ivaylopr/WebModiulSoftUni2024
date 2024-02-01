using ForumApp24.Core.Models;

namespace ForumApp24.Core.Contracts
{
    public interface IPostService
    {
        Task<IEnumerable<PostModel>> GetAllPostsAsync();
    }
}
