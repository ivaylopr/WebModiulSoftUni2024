using ForumApp24.Core.Contracts;
using ForumApp24.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace ForumApp24.Controllers
{
    public class PostController : Controller
    {
        private readonly IPostService postService;
        public PostController(IPostService _postService)
        {
            postService = _postService;
        }
        public async Task<IActionResult> Index()
        {
            IEnumerable<PostModel> posts = await postService.GetAllPostsAsync();
            return View(posts);
        }
    }
}
