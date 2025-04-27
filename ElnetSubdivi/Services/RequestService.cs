// Rename the class to avoid the conflict with the existing 'PostService' class in the same namespace.  
using ElnetSubdivi.data;
using ElnetSubdivi.Models;
using Microsoft.EntityFrameworkCore;

namespace ElnetSubdivi.Services
{
    public class RequestService : IRequestService // Renamed from 'PostService' to 'RequestService'  
    {
        private readonly ApplicationDbContext _context;

        public RequestService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<ServiceRequest>> GetAllRequest()
        {
            var requests = await _context.Service_Request.ToListAsync();
            return requests ?? new List<ServiceRequest>(); // Ensure it never returns null  
        }

        public async Task<List<Post>> GetPostsByUserAsync(string userId)
        {
            return await _context.Posts
                                 .Where(p => p.user_id == userId)
                                 .OrderByDescending(p => p.post_date)
                                 .ToListAsync();
        }

        public async Task<List<ServiceRequest>> GetAllPost()
        {
            try
            {
                var request = await _context.Service_Request
                    .Select(u => new ServiceRequest
                    {
                        Request_Id = u.Request_Id ?? "",
                        User_Id = u.User_Id ?? "",
                        Request_Creation = u.Request_Creation,
                        Request_Type = u.Request_Type ?? "",
                        Request_Subject = u.Request_Subject ?? "",
                        Request_Time = u.Request_Time ?? "",
                        Request_Date = u.Request_Date,
                        Request_Description = u.Request_Description ?? "",
                        Request_Status = u.Request_Status ?? ""
                    })
                    .ToListAsync();

                return request;
            }
            catch (Exception ex)
            {
                // Log errors and return an empty list  
                Console.WriteLine($"Error fetching users: {ex.Message}");
                return new List<ServiceRequest>();
            }
        }
    }
}
