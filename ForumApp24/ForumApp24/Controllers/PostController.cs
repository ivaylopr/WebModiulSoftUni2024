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
        [HttpGet]
        public  IActionResult Add()
        {
            var model = new PostModel();
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Add(PostModel model)
        {
            if (ModelState.IsValid==false)
            {
                return View(model);
            }
            await postService.AddAssync(model);
            return RedirectToAction(nameof(Index));   
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            PostModel? post = await postService.GetByIdAsync(id);
            if (post == null)
            {
                ModelState.AddModelError("All", "Invalid post");
            }
            return View(post);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(PostModel model)
        {
            if (ModelState.IsValid==false)
            {
                return View(model);
            }
            await postService.EditAsync(model);
            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await postService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
