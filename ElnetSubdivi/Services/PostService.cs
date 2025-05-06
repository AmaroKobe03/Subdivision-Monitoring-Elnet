using ElnetSubdivi.data;
using ElnetSubdivi.Models;
using Microsoft.EntityFrameworkCore;

namespace ElnetSubdivi.Services
{
    public class PostService : IPostService
    {
        private readonly ApplicationDbContext _context;

        public PostService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<List<Post>> GetAllPostsAsync()
        {
            var posts = await _context.Posts.ToListAsync();
            return posts ?? new List<Post>(); // Ensure it never returns null
        }


        public async Task<List<Post>> GetPostsByUserAsync(string userId)
        {
            return await _context.Posts
                                 .Where(p => p.user_id == userId)
                                 .OrderByDescending(p => p.post_date)
                                 .ToListAsync();
        }

        public async Task<List<Post>> GetAllPost()
        {
            try
            {
                var posts = await _context.Posts
                    .Select(u => new Post
                    {
                        post_id = u.post_id ?? "",
                        user_id = u.user_id ?? "",
                        post_date = u.post_date,
                        post_media = u.post_media ?? "",
                        post_text = u.post_text ?? "",
                        likes = u.likes,
                        comments = u.comments,

                    })
                    .ToListAsync();

                return posts;
            }
            catch (Exception ex)
            {
                // Log errors and return an empty list
                Console.WriteLine($"Error fetching users: {ex.Message}");
                return new List<Post>();
            }
        }
    }
}