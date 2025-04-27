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

        public ServiceRequestController(IUserService userService, ApplicationDbContext context, IPostService postService, IRequestService requestService)
        {
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _postService = postService ?? throw new ArgumentNullException(nameof(postService));
            _requestService = requestService ?? throw new ArgumentNullException(nameof(requestService));
        }

        public async Task<IActionResult> Index()
        {
            var requests = await _requestService.GetAllRequest() ?? new List<ServiceRequest>(); // Ensures it's not null
            return View(requests);
        }


        /*
        var summaryCards = new List<SummaryCard>
        {
            new SummaryCard {
                Title = "Pending",
                Count = serviceRequests.Count(r => r.Request_Status == "Pending"),
                Icon = "pending.svg",
                BorderColor = "border-yellow-500"
            },
            new SummaryCard {
                Title = "Approved",
                Count = serviceRequests.Count(r => r.Request_Status == "Approved"),
                Icon = "approved.svg",
                BorderColor = "border-green-500"
            },
            new SummaryCard {
                Title = "Declined",
                Count = serviceRequests.Count(r => r.Request_Status == "Declined"),
                Icon = "declined.svg",
                BorderColor = "border-red-500"
            }
        };

        var viewModel = new ServiceRequestManagementViewModel
        {
            ServiceRequests = serviceRequests,
            SummaryCards = summaryCards
        };
        */


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
                Request_Creation = DateTime.Now,
                Request_Status = model.Request_Status,
            };

            _context.Set<ServiceRequest>().Add(newRequest);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Request submitted successfully!" });
        }

        // ✅ New private helper method that just gets the list
        /*
        private async Task<List<ServiceRequest>> GetAllServiceRequestsAsync()
        {
            return await _context.Set<ServiceRequest>()
                .OrderByDescending(r => r.Request_Creation)
                .ToListAsync();
        }
        */
        /*
        public async Task<IActionResult> ServiceRequestManagement()
        {
            if (!User.Identity.IsAuthenticated)
                return Unauthorized();

            var viewModel = await BuildServiceRequestManagementViewModelAsync();
            return View(viewModel); // ✅ Correct type passed into the View
        }*/


      /*  public async Task<IActionResult> ListRequests()
        {
            var serviceRequests = await GetAllServiceRequestsAsync(); // ✅ now this is a real list

            var summaryCards = new List<dynamic>
        {
        new { Title = "Pending", Count = serviceRequests.Count(r => r.Request_Status == "Pending"), Icon = "pending.svg", BorderColor = "border-yellow-500" },
        new { Title = "Approved", Count = serviceRequests.Count(r => r.Request_Status == "Approved"), Icon = "approved.svg", BorderColor = "border-green-500" },
        new { Title = "Declined", Count = serviceRequests.Count(r => r.Request_Status == "Declined"), Icon = "declined.svg", BorderColor = "border-red-500" },
        };

            var viewModel = new ServiceRequestManagementViewModel
            {
                ServiceRequests = serviceRequests,
                SummaryCards = summaryCards
            };

            return View(viewModel);
        }

        private async Task<ServiceRequestManagementViewModel> BuildServiceRequestManagementViewModelAsync()
            {
                var serviceRequests = await GetAllServiceRequestsAsync();

                var summaryCards = new List<dynamic>
        {
            new { Title = "Pending", Count = serviceRequests.Count(r => r.Request_Status == "Pending"), Icon = "pending.svg", BorderColor = "border-yellow-500" },
            new { Title = "Approved", Count = serviceRequests.Count(r => r.Request_Status == "Approved"), Icon = "approved.svg", BorderColor = "border-green-500" },
            new { Title = "Declined", Count = serviceRequests.Count(r => r.Request_Status == "Declined"), Icon = "declined.svg", BorderColor = "border-red-500" },
        };

                return new ServiceRequestManagementViewModel
                {
                    ServiceRequests = serviceRequests,
                    SummaryCards = summaryCards
                };
            }

        */
    }
}