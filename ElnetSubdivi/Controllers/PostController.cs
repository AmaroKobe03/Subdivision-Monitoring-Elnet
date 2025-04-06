using ElnetSubdivi.data;
using ElnetSubdivi.Models;
using ElnetSubdivi.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ElnetSubdivi.Controllers
{
    public class PostController : Controller
    {
        private readonly IUserService _userService;
        private readonly ApplicationDbContext _context;
        private readonly IPostService _postService;

        public PostController(IUserService userService, ApplicationDbContext context, IPostService postService)
        {
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _postService = postService ?? throw new ArgumentNullException(nameof(postService));
        }

        public async Task<IActionResult> Index()
        {
            var posts = await _postService.GetAllPost() ?? new List<Post>(); // Ensures it's not null
            return View(posts);
        }



        [HttpPost]
        public async Task<IActionResult> CreatePost([FromBody] PostViewModel postData)
        {

            if (!User.Identity.IsAuthenticated)
            {
                return Unauthorized();
            }

            var userId = _userService.GetCurrentUserId(User); // however you fetch current user ID

            var newPost = new Post
            {
                post_id = Guid.NewGuid().ToString(),
                user_id = userId,
                post_date = DateTime.Now,
                post_text = postData.PostText,
                post_media = "", // implement if you support media
                likes = 0,
                comments = 0
            };

            _context.Posts.Add(newPost);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Post created successfully." });
        }

        // In PostController.cs (or relevant controller)
        public async Task<IActionResult> UserDash()
        {
                var posts = await _postService.GetAllPost();
                return View(posts);
        }
    }
}
