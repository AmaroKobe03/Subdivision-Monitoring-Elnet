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

        public PostController(IUserService userService, ApplicationDbContext context)
        {
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public IActionResult Index()
        {
            return View();
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

    }
}
