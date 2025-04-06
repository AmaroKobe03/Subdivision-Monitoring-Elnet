using ElnetSubdivi.data;
using ElnetSubdivi.Models;
using ElnetSubdivi.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ElnetSubdivi.Controllers
{
    public class ServiceRequestController : Controller
    {

        private readonly IUserService _userService;
        private readonly ApplicationDbContext _context;
        private readonly IPostService _postService;

        public ServiceRequestController(IUserService userService, ApplicationDbContext context, IPostService postService)
        {
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _postService = postService ?? throw new ArgumentNullException(nameof(postService));
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateRequest([FromBody] ServiceRequestViewModel model)
        {
            if (!User.Identity.IsAuthenticated)
                return Unauthorized();

            var userId = _userService.GetCurrentUserId(User);

            // Get the latest request with the highest numeric RequestId
            var latestRequest = await _context.Set<ServiceRequest>()
                .OrderByDescending(r => r.Request_Id)
                .FirstOrDefaultAsync();

            int nextId = 1;

            if (latestRequest != null && !string.IsNullOrWhiteSpace(latestRequest.Request_Id))
            {
                // Extract numeric part from "REQ-0001"
                var match = System.Text.RegularExpressions.Regex.Match(latestRequest.Request_Id, @"REQ-(\d+)");
                if (match.Success && int.TryParse(match.Groups[1].Value, out int parsedId))
                {
                    nextId = parsedId + 1;
                }
            }

            string requestId = $"REQ-{nextId.ToString("D4")}";

            var newRequest = new ServiceRequest
            {
                Request_Id = requestId,
                User_Id = userId,
                Request_Type = model.Request_Type,
                Request_Subject = model.Request_Subject,
                Request_Date = model.Request_Date,
                Request_Time = model.Request_Time,
                Request_Description = model.Request_Description,
                Request_Creation = DateTime.Now
            };

            _context.Set<ServiceRequest>().Add(newRequest);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Request submitted successfully!" });
        }


    }
}
