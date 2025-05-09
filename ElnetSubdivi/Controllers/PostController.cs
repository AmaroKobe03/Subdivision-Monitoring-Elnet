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
        [RequestSizeLimit(10_000_000)] // optional: limit upload size (10MB here)
        public async Task<IActionResult> CreatePost([FromForm] string postText, [FromForm] IFormFile? mediaFile)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return Unauthorized();
            }

            var userId = _userService.GetCurrentUserId(User);
            string mediaPath = "";

            if (mediaFile != null && mediaFile.Length > 0)
            {
                var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");
                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(mediaFile.FileName);
                var filePath = Path.Combine(uploadsFolder, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await mediaFile.CopyToAsync(stream);
                }

                mediaPath = $"/uploads/{fileName}"; // relative path to serve in <img> or <video>
            }

            var newPost = new Post
            {
                post_id = Guid.NewGuid().ToString(),
                user_id = userId,
                post_date = DateTime.Now,
                post_text = postText,
                post_media = mediaPath,
                likes = 0,
                comments = 0
            };

            _context.Posts.Add(newPost);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Post created successfully.", media = mediaPath });
        }


        // In PostController.cs (or relevant controller)
        public async Task<IActionResult> UserDash()
        {
                var posts = await _postService.GetAllPost();
                return View(posts);
        }
    }
}
