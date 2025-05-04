using ElnetSubdivi.data;
using ElnetSubdivi.Models;
using ElnetSubdivi.Services;
using ElnetSubdivi.Views.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ElnetSubdivi.Controllers
{
    public class ServiceRequestController : Controller
    {
        private readonly IUserService _userService;
        private readonly ApplicationDbContext _context;
        private readonly IPostService _postService;
        private readonly IRequestService _requestService;
        private readonly IWebHostEnvironment _webHostEnvironment; // Add this field

        public ServiceRequestController(IUserService userService, ApplicationDbContext context, IPostService postService, IRequestService requestService, IWebHostEnvironment webHostEnvironment)
        {
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _postService = postService ?? throw new ArgumentNullException(nameof(postService));
            _requestService = requestService ?? throw new ArgumentNullException(nameof(requestService));
            _webHostEnvironment = webHostEnvironment ?? throw new ArgumentNullException(nameof(webHostEnvironment));
        }

        [HttpPost]
        public async Task<IActionResult> CreateRequest([FromForm] ServiceRequestViewModel model)
        {
            if (!User.Identity.IsAuthenticated)
                return Unauthorized();

            var userId = _userService.GetCurrentUserId(User);

            var latestRequest = await _context.Set<ServiceRequest>()
                .OrderByDescending(r => r.Request_Id)
                .FirstOrDefaultAsync();

            int nextId = 1;

            if (latestRequest != null && !string.IsNullOrWhiteSpace(latestRequest.Request_Id))
            {
                var match = System.Text.RegularExpressions.Regex.Match(latestRequest.Request_Id, @"REQ-(\d+)");
                if (match.Success && int.TryParse(match.Groups[1].Value, out int parsedId))
                {
                    nextId = parsedId + 1;
                }
            }

            string? savedFileName = null;

            if (model.Request_Attachment != null && model.Request_Attachment.Length > 0)
            {
                var uploads = Path.Combine(_webHostEnvironment.WebRootPath, "uploads"); // Use _webHostEnvironment.WebRootPath instead of _context.WebRootPath
                Directory.CreateDirectory(uploads);
                var fileName = $"{Guid.NewGuid()}{Path.GetExtension(model.Request_Attachment.FileName)}";
                var filePath = Path.Combine(uploads, fileName);
                using var stream = System.IO.File.Create(filePath);
                await model.Request_Attachment.CopyToAsync(stream);

                savedFileName = "/uploads/" + fileName;

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
                Request_Creation = DateTime.Now,
                Request_Status = model.Request_Status,
                Assigned_Staff = "Unassigned",
                Attachment_Path = savedFileName
            };

            _context.Set<ServiceRequest>().Add(newRequest);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Request submitted successfully!" });
        }
    }
}