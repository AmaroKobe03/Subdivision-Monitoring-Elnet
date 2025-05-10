using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using ElnetSubdivi.Models;
using ElnetSubdivi.data;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using ElnetSubdivi.Services;
using ElnetSubdivi.ViewModels;
using Microsoft.AspNetCore.Hosting;


namespace ElnetSubdivi.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUserService _userService;
        private readonly ApplicationDbContext _context;
        private readonly IPostService _postService;
        private readonly IFacilityService _facilityService;
        private readonly IRequestService _requestService;
        private readonly IReservationService _reservationService;
        private readonly IBillingPaymentService _billingService;
        private readonly IWebHostEnvironment _webHostEnvironment; // Add this field
        private readonly IVisitorListService _VisitorListService;
        private readonly IVehicleService _vehicleService;
        private readonly IFeedbackService _feedbackService;


        public HomeController(ILogger<HomeController> logger, IUserService userService, ApplicationDbContext context, IPostService postService, IFacilityService facilityService, IRequestService requestService, IReservationService reservationService, IBillingPaymentService billingService, IWebHostEnvironment webHostEnvironment, IVisitorListService visitorListService, IVehicleService vehicleService, IFeedbackService feedbackService)
        {
            _logger = logger;
            _userService = userService;
            _context = context;
            _postService = postService;
            _facilityService = facilityService;
            _requestService = requestService;
            _reservationService = reservationService;
            _billingService = billingService;
            _webHostEnvironment = webHostEnvironment ?? throw new ArgumentNullException(nameof(webHostEnvironment));
            _VisitorListService = visitorListService;
            _vehicleService = vehicleService;
            _feedbackService = feedbackService;
        }

        public async Task<IActionResult> Index()
        {
            // Get posts from your database/service
            var posts = _context.Posts.OrderByDescending(p => p.post_date).ToList();

            // Get current user (example - adjust based on your auth system)
            var currentUser = _context.Users.FirstOrDefault(u => u.user_id == User.Identity.Name);

            // Create the view model
            var viewModel = new PostAndUserViewModel
            {
                Posts = posts,
                User = currentUser
            };

            return View(viewModel);
        }

        public async Task<IActionResult> ServiceRequestManagement(bool showDashboard = false)
        {
            if (showDashboard)
            {
                ViewData["HideSearch"] = true;
                ViewData["Hidebtn"] = true;

                var dashboardStats = new List<dynamic>
        {
            new { Title = "Total Requests", Count = 123, Icon = "treqs.svg", BorderColor = "border-blue-400 border-b-2" },
            new { Title = "Pending ", Count = 34, Icon = "pendi.svg", BorderColor = "border-yellow-400 borber-b-2" },
            new { Title = "Ongoing ", Count = 15, Icon = "ongo.svg", BorderColor = "border-orange-400 borber-b-2" },
            new { Title = "Completed ", Count = 56, Icon = "apr.svg", BorderColor = "border-green-400 borber-b-2" },
            new { Title = "Canceled Service ", Count = 10, Icon = "canc.svg", BorderColor = "border-red-400 borber-b-2" }
        };

                return View("DashboardView", dashboardStats);
            }
            else
            {
                // Get posts from your database/service
                var requests = await _context.Service_Request
                    .OrderByDescending(p => p.Request_Date)
                    .ToListAsync();

                // Create the view model
                var viewModel = new ServiceRequestManagementViewModel
                {
                    ServiceRequests = requests,
                    Users = await _userService.GetAllUsers() // Fetch users from DB
                };

                return View(viewModel);
            }
        }

        public async Task<IActionResult> serviceRequest(bool showDashboard = false)
        {
            if (showDashboard)
            {
                ViewData["HideSearch"] = true;
                ViewData["Hidebtn"] = true;

                var dashboardStats = new List<dynamic>
        {
            new { Title = "Total Requests", Count = 123, Icon = "treqs.svg", BorderColor = "border-blue-400 border-b-2" },
            new { Title = "Pending ", Count = 34, Icon = "pendi.svg", BorderColor = "border-yellow-400 borber-b-2" },
            new { Title = "Ongoing ", Count = 15, Icon = "ongo.svg", BorderColor = "border-orange-400 borber-b-2" },
            new { Title = "Completed ", Count = 56, Icon = "apr.svg", BorderColor = "border-green-400 borber-b-2" },
            new { Title = "Canceled Service ", Count = 10, Icon = "canc.svg", BorderColor = "border-red-400 borber-b-2" }
        };

                return View("DashboardView", dashboardStats);
            }
            else
            {
                var userId = User.FindFirst("UserId")?.Value;
                var requests = await _context.Service_Request
                    .Where(p => p.User_Id == userId)
                    .OrderByDescending(p => p.Request_Date)
                    .ToListAsync();

                var viewModel = new ServiceRequestManagementViewModel
                {
                    ServiceRequests = requests
                };

                return View(viewModel);
            }

        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Landing()
        {
            ViewBag.HideNav = true;
            return View();
        }

        public async Task<IActionResult> UserDash()
        {
            // Get posts from your database/service
            var posts = _context.Posts.OrderByDescending(p => p.post_date).ToList();

            // Get current user (example - adjust based on your auth system)
            var currentUser = _context.Users.FirstOrDefault(u => u.user_id == User.Identity.Name);

            // Create the view model
            var viewModel = new PostAndUserViewModel
            {
                Posts = posts,
                User = currentUser,
                Users = await _userService.GetAllUsers()
            };

            return View(viewModel);
        }

        public IActionResult UserProfile()
        {
            ViewData["PageTitle"] = "Profile Settings";
            ViewData["HideBtn"] = true;
            return View();
        }


        /////////////////////////////////// FACILITY SCRIPTS /////////////////////////////////////////////////////////


        public async Task<IActionResult> FacilityReservationAsync()
        {
            var reservationList = await _reservationService.GetAllReservation() ?? new List<Reservation>(); // Ensures it's not null
            var userId = User.FindFirst("UserId")?.Value;
            var currentuser = await _context.Users.FirstOrDefaultAsync(u => u.user_id == userId);
            var facilities = await _facilityService.GetAllFacilitiesWithHoursAsync() ?? new List<Facility>();

            var reservations = new ReservationViewModel
            {
                Reservations = reservationList.Select(r => new ReservationViewModel
                {
                    ReservationId = r.reservation_id,
                    FacilityId = r.facility_id,
                    UserId = r.user_id,
                    ReservationDate = r.reservation_date,
                    TimeIn = r.time_in,
                    TimeOut = r.time_out,
                    ReservationPurpose = r.reservation_purpose,
                    ReservationStatus = r.reservation_status
                }).ToList(),

                Facilities = facilities.Select(f => new FacilityViewModel
                {
                    FacilityId = f.Facility_Id,
                    FacilityName = f.Facility_Name,
                    FacilityCategory = f.Facility_Category,
                    FacilityDescription = f.Facility_Description,
                    ImagePath = f.Facility_Img,
                    ServiceFeePerHour = f.Service_Fee_Per_Hour,
                    FacilityGuidelines = f.Facility_Guidelines,
                    FacilityAminities = f.Facility_Aminities,
                    FacilityStatus = f.Facility_Status,
                    OperatingHours = f.OperatingHours.Select(h => new FacilityViewModel.FacilityOperatingHours
                    {
                        Facility_Id = h.Facility_Id,
                        Day_Of_Week = h.Day_Of_Week,
                        Opening_Time = h.Opening_Time,
                        Closing_Time = h.Closing_Time
                    }).ToList()
                }).ToList(),

                User = currentuser,
            };

            return View(reservations);
        }

        public async Task<IActionResult> MyReservationAsync()
        {
            var reservationList = await _reservationService.GetAllReservation() ?? new List<Reservation>(); // Ensures it's not null
            var userId = User.FindFirst("UserId")?.Value;
            var currentuser = await _context.Users.FirstOrDefaultAsync(u => u.user_id == userId);
            var facilities = await _facilityService.GetAllFacilitiesWithHoursAsync() ?? new List<Facility>();

            var reservations = new ReservationViewModel
            {
                Reservations = reservationList.Select(r => new ReservationViewModel
                {
                    ReservationId = r.reservation_id,
                    FacilityId = r.facility_id,
                    UserId = r.user_id,
                    ReservationDate = r.reservation_date,
                    TimeIn = r.time_in,
                    TimeOut = r.time_out,
                    ReservationPurpose = r.reservation_purpose,
                    ReservationStatus = r.reservation_status
                }).ToList(),

                Facilities = facilities.Select(f => new FacilityViewModel
                {
                    FacilityId = f.Facility_Id,
                    FacilityName = f.Facility_Name,
                    FacilityCategory = f.Facility_Category,
                    FacilityDescription = f.Facility_Description,
                    ImagePath = f.Facility_Img,
                    ServiceFeePerHour = f.Service_Fee_Per_Hour,
                    FacilityGuidelines = f.Facility_Guidelines,
                    FacilityAminities = f.Facility_Aminities,
                    FacilityStatus = f.Facility_Status,
                    OperatingHours = f.OperatingHours.Select(h => new FacilityViewModel.FacilityOperatingHours
                    {
                        Facility_Id = h.Facility_Id,
                        Day_Of_Week = h.Day_Of_Week,
                        Opening_Time = h.Opening_Time,
                        Closing_Time = h.Closing_Time
                    }).ToList()
                }).ToList(),

                User = currentuser,
            };

            return View(reservations);
        }

        public async Task<IActionResult> viewProfileAsync()
        {
            // Get posts from your database/service
            var posts = _context.Posts.OrderByDescending(p => p.post_date).ToList();

            // Get current user (example - adjust based on your auth system)
            var currentUser = _context.Users.FirstOrDefault(u => u.user_id == User.Identity.Name);

            // Create the view model
            var viewModel = new PostAndUserViewModel
            {
                Posts = posts,
                User = currentUser,
                Users = await _userService.GetAllUsers()
            };

            return View(viewModel);
        }

        public async Task<IActionResult> viewSpecificProfileAsync(string user_id)
        {
            // Get posts from your database/service
            var posts = _context.Posts.OrderByDescending(p => p.post_date).ToList();

            // Get user by the provided user_id parameter
            var currentUser = _context.Users.FirstOrDefault(u => u.user_id == user_id);

            // Create the view model
            var viewModel = new PostAndUserViewModel
            {
                Posts = posts,
                User = currentUser,
                Users = await _userService.GetAllUsers()
            };

            return View(viewModel);
        }


        public async Task<IActionResult> VisitorsPassManagementAsync()
        {
            ViewData["HideSearch"] = true;
            ViewData["Hidebtn"] = true;

            var reservations1 = new List<dynamic>
            {
                new { Title = "Total Visitor Requests", Count = 123, Icon = "totalv.svg", BorderColor = "border-blue-400 border-b-2" },
                new { Title = "Pending Approval", Count = 34, Icon = "pendi.svg", BorderColor = "border-yellow-400 borber-b-2" },
                new { Title = "Active Visitors", Count = 56, Icon = "activev.svg", BorderColor = "border-green-400 borber-b-2" },
                new { Title = "Checked-Out Visitors", Count = 10, Icon = "outv.svg", BorderColor = "border-red-400 borber-b-2" }

            };

            // Get posts from your database/service
            var visitorlist = _context.Visitor_List.OrderByDescending(p => p.visitor_id).ToList();

            // Get current user (example - adjust based on your auth system)
            var currentuser = _context.Users.FirstOrDefault(u => u.user_id == User.Identity.Name);


            // Create the view model
            var visitors = new VIsitorListViewModel
            {
                VisitorList = visitorlist,
                Users = await _userService.GetAllUsers(),
            };

            return View(visitors);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateStatus([FromBody] VisitorStatusUpdateModel model)
        {
            var visitor = await _context.Visitor_List.FindAsync(model.VisitorId);
            if (visitor == null)
                return NotFound();

            visitor.visit_status = model.Status;
            await _context.SaveChangesAsync();
            return Ok();
        }

        public class VisitorStatusUpdateModel
        {
            public int VisitorId { get; set; }
            public string Status { get; set; }
        }

        public async Task<IActionResult> PaymentHistoryAsync(string userId)
        {
            ViewData["HideSearch"] = true;
            ViewData["Hidebtn"] = true;

            var billing1 = new List<dynamic>
            {
                new { Title = "Total Amount Due", Count = 123, Icon = "treqs.svg", BorderColor = "border-blue-400 border-b-2" },
                new { Title = "Pending Payments", Count = 34, Icon = "pendi.svg", BorderColor = "border-yellow-400 borber-b-2" },
                new { Title = "Overdue Payments", Count = 15, Icon = "overd.svg", BorderColor = "border-orange-400 borber-b-2" },
                new { Title = "Total Amount Paid (2025) ", Count = 56, Icon = "apr.svg", BorderColor = "border-green-400 borber-b-2" },

            };
            var billing = new BillingPaymentViewModel
            {
                BillStatements = await _billingService.GetPaymentHistoryByUserId(userId),// Await the Task to resolve the issue
                Users = await _userService.GetAllUsers() // Fetch users from DB
            };
            ViewBag.UserId = userId;
            return View(billing);
        }

        public async Task<IActionResult> BillingManagement()
        {
            ViewData["HideSearch"] = true;
            ViewData["Hidebtn"] = true;
            ViewData["Bills"] = await _billingService.GetAll();
            ViewData["Homeowners"] = await _billingService.GetAllUsersBasicInfo(); // Add this

            var billing = new BillingPaymentViewModel
            {
                BillStatements = await _billingService.GetAll(),// Await the Task to resolve the issue
                Users = await _userService.GetAllUsers() // Fetch users from DB
            };

            return View(billing);
        }

        public async Task<IActionResult> Billing()
        {
            ViewData["HideSearch"] = true;
            ViewData["Hidebtn"] = true;
            ViewData["Bills"] = await _billingService.GetAll();
            ViewData["Homeowners"] = await _billingService.GetAllUsersBasicInfo(); // Add this

            var billing = new BillingPaymentViewModel
            {
                BillStatements = await _billingService.GetAll(),// Await the Task to resolve the issue
                Users = await _userService.GetAllUsers() // Fetch users from DB
            };

            return View(billing);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateInvoice(BillingPaymentModel model)
        {
            if (ModelState.IsValid)
            {
                // Fetch list of homeowners
                var homeowners = await _billingService.GetAllUsersBasicInfo();

                // Find the user whose ID matches the selected user_id
                var selectedUser = homeowners.FirstOrDefault(u => u.UserId == model.user_id);

                // If found, assign the FullName to model.user_name
                if (selectedUser != null)
                {
                    model.user_name = selectedUser.FullName;
                }

                model.bill_status = "Pending";
                await _billingService.Add(model);
                TempData["Success"] = "Bill added successfully!";
                return RedirectToAction("BillingManagement");
            }

            // If model is invalid, reprepare data for the view
            ViewData["HideSearch"] = true;
            ViewData["Hidebtn"] = true;
            ViewData["Bills"] = await _billingService.GetAll();
            ViewData["Homeowners"] = await _billingService.GetAllUsersBasicInfo();

            var billingViewModel = new BillingPaymentViewModel
            {
                BillStatements = await _billingService.GetAll()
            };

            return View("BillingManagement", billingViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditInvoice(BillingPaymentModel model)
        {
            if (ModelState.IsValid)
            {
                await _billingService.Update(model);  // Make sure your service has an Update method.
                TempData["Success"] = "Invoice updated successfully!";
            }
            else
            {
                TempData["Error"] = "Failed to update invoice. Please check the data.";
            }

            return RedirectToAction("BillingManagement");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> MarkAsPaid(string bill_no)
        {
            if (string.IsNullOrEmpty(bill_no))
            {
                return BadRequest();
            }

            var bill = await _context.Billing_Statements.FirstOrDefaultAsync(b => b.bill_no == bill_no);
            if (bill == null)
            {
                return NotFound();
            }

            bill.bill_status = "Paid";
            await _context.SaveChangesAsync();

            return RedirectToAction("BillingManagement"); // Adjust action name as needed
        }


        [HttpPost]
        public async Task<IActionResult> SubmitPayment([FromBody] Payments model)
        {
            model.payment_date = DateTime.Now;

            var userId = User.FindFirst("UserId")?.Value;
            var currentuser = await _context.Users.FirstOrDefaultAsync(u => u.user_id == userId);
            model.user_id = currentuser.user_id;

            // Generate 6 random numbers for payment_id (e.g., 482195)
            var random = new Random();
            model.payment_id = random.Next(100000, 999999); // ensures 6 digits, from 100000 to 999999

            // Optional: remove payment_id and user_id from model state if it's bound from the client
            ModelState.Remove(nameof(model.user_id));
            ModelState.Remove(nameof(model.payment_id));

                if (ModelState.IsValid)
            {
                try
                {
                    _context.Payments.Add(model);
                    await _context.SaveChangesAsync();

                    return Ok(new { message = "Payment successful", paymentId = model.payment_id });
                }
                catch (Exception ex)
                {
                    return BadRequest("Error: " + ex.Message);
                }
            }

            return BadRequest("Invalid payment data.");
        }






        public async Task<IActionResult> AdminDash()
        {
            // Await asynchronous calls to avoid blocking and allow proper async handling
            var bills = (await _billingService.GetAll()).Count;
            var users = (await _userService.GetAllUsers()).Count;
            var feedbacks = (await _feedbackService.GetAllFeedback()).Count;
            var requests = (await _requestService.GetAllRequest()).Count; // Fix: Await the Task
            var events = 10; // Example placeholder
            var homeowners = (await _userService.GetAllHomeowners()).Count;
            var staff = (await _userService.GetAllStaff()).Count;
            var pending = (await _requestService.GetAllPendingRequest()).Count; // Fix: Await the Task
            var completed = (await _requestService.GetAllCompleteRequest()).Count;
            var posts = (await _postService.GetAllPost()).Count;
            var payments = await _billingService.GetTotalPaymentsAmount();
            var overdue = await _billingService.GetTotalPendingBillsAmount();

            var data = new List<dynamic>
    {
        new { Title = "User Count", Icon = "user.svg", Count = users.ToString(), Label1 = "Homeowners", Value1 = homeowners.ToString(), Label2 = "Staffs", Value2 = staff.ToString() },
        new { Title = "Events", Icon = "calen.svg", Count = events.ToString(), Label1 = "Pending", Value1 = pending.ToString(), Label2 = "Completed", Value2 = completed.ToString() },
        new { Title = "Forum Post", Icon = "msg.svg", Count = posts.ToString(), Label1 = "+14% from last month", Value1 = "", Label2 = "", Value2 = "" },
        new { Title = "Requests", Icon = "tool.svg", Count = requests.ToString(), Label1 = "Ongoing", Value1 = pending.ToString(), Label2 = "Completed", Value2 = completed.ToString() },
        new { Title = "Feedback", Icon = "str.svg", Count = feedbacks.ToString(), Label1 = "Avg Rating", Value1 = "4.8", Label2 = "", Value2 = "" },
        new { Title = "Financial", Icon = "pe.svg", Count = "", Label1 = "Paid", Value1 = payments.ToString(), Label2 = "Overdue", Value2 = overdue.ToString() }
    };

            var statistics = new StatisticsViewModel
            {
                BillStatements = await _billingService.GetAll(),
                Users = await _userService.GetAllUsers(),
                Payments = await _context.Payments.ToListAsync(),
                Feedback = await _feedbackService.GetAllFeedback(),
                IncidentReports = await _context.Incident_Reports.ToListAsync(),
                VisitorList = await _context.Visitor_List.ToListAsync(),
                Vehicle = await _context.Vehicle.ToListAsync(),
                ServiceRequests = await _requestService.GetAllRequest(),
                Reservations = await _reservationService.GetAllReservationAsync(),
                Facilities = await _facilityService.GetAllFacilitiesWithHoursAsync(),
                Posts = await _postService.GetAllPost(),
                DashboardData = data
            };

            return View(statistics);
        }


        public async Task<IActionResult> UserManagement()
        {
            var users = await _userService.GetAllUsers(); // Fetch users from DB
            return View(users);
        }

        public async Task<IActionResult> UserVisitorsAsync()
        {
            // Get posts from your database/service
            var visitorlist = _context.Visitor_List.OrderByDescending(p => p.visitor_id).ToList();

            // Get current user (example - adjust based on your auth system)
            var currentuser = _context.Users.FirstOrDefault(u => u.user_id == User.Identity.Name);


            // Create the view model
            var visitors = new VIsitorListViewModel
            {
                VisitorList = visitorlist,
                Users = await _userService.GetAllUsers(),
                User = currentuser
            };

            return View(visitors);
        }

        [HttpPost]
        public async Task<IActionResult> AddVisitor(VisitorList visitor)
        {
            if (ModelState.IsValid)
            {
                await _VisitorListService.AddVisitorAsync(visitor);
                _context.SaveChanges();
                return RedirectToAction("UserVisitors"); // or wherever your list is
            }

            return View("UserVisitors", visitor); // return with errors
        }

        [HttpPost]
        public IActionResult UpdateVisitor(VisitorList visitor)
        {
            if (ModelState.IsValid)
            {
                var existing = _context.Visitor_List.FirstOrDefault(v => v.visitor_id == visitor.visitor_id);
                if (existing != null)
                {
                    existing.first_name = visitor.first_name;
                    existing.last_name = visitor.last_name;
                    existing.phone_number = visitor.phone_number;
                    existing.visit_date = visitor.visit_date;
                    existing.visit_time = visitor.visit_time;
                    existing.vehicle_plate = visitor.vehicle_plate;
                    existing.visit_reason = visitor.visit_reason;
                    _context.SaveChanges();
                }
            }
            return RedirectToAction("UserVisitors", visitor); // or wherever the view is rendered
        }

        [HttpPost]
        public IActionResult CancelVisitor(int visitor_id)
        {
            var visitor = _context.Visitor_List.FirstOrDefault(v => v.visitor_id == visitor_id);
            if (visitor != null)
            {
                visitor.visit_status = "Cancelled";
                _context.SaveChanges();
            }
            return RedirectToAction("UserVisitors"); // Or "Visitors", depending on your route
        }

        public IActionResult UserCalendar()
        {
            ViewData["HideSearch"] = true;
            return View();
        }


        ///////////////////////////////////////////////// VEHICLE SCRIPTS //////////////////////////////////////////////////////

        public async Task<IActionResult> VehicleManagementAsync()
        {
            ViewData["HideSearch"] = true;
            ViewData["Hidebtn"] = true;

            var manageVehicle = new List<dynamic>
            {
                new { Title = "Total Vehicles", Count = 123, Icon = "vehicle.svg", BorderColor = "border-blue-400 border-b-2" },
                new { Title = "Pending Approval", Count = 34, Icon = "pendi.svg", BorderColor = "border-yellow-400 borber-b-2" },
                new { Title = "Approved Today", Count = 56, Icon = "activev.svg", BorderColor = "border-green-400 borber-b-2" },
                new { Title = "Declined", Count = 10, Icon = "outv.svg", BorderColor = "border-red-400 borber-b-2" }

            };

            // Get posts from your database/service
            var vehiclelist = _context.Vehicle.OrderByDescending(p => p.plate_number).ToList();
            var userId = User.FindFirst("UserId")?.Value;

            // Get current user (example - adjust based on your auth system)
            var currentuser = await _context.Users.FirstOrDefaultAsync(u => u.user_id == userId);

            // Create the view model
            var vehicles = new VehicleViewModel
            {
                Vehicle = vehiclelist,
                Users = await _userService.GetAllUsers(),
                User = currentuser
            };

            return View(vehicles);
        }


        public async Task<IActionResult> UserVehicleAsync()
        {
            ViewData["HideSearch"] = true;
            // Get posts from your database/service
            var vehiclelist = _context.Vehicle.OrderByDescending(p => p.plate_number).ToList();
            var userId = User.FindFirst("UserId")?.Value;

            // Get current user (example - adjust based on your auth system)
            var currentuser = await _context.Users.FirstOrDefaultAsync(u => u.user_id == userId);


            // Create the view model
            var vehicles = new VehicleViewModel
            {
                Vehicle = vehiclelist,
                Users = await _userService.GetAllUsers(),
                User = currentuser
            };

            return View(vehicles);
        }

        [HttpPost]
        public async Task<IActionResult> AddVehicle(Vehicle model, IFormFile vehicle_document)
        {
            if (vehicle_document != null && vehicle_document.Length > 0)
            {
                // Example: Save to wwwroot/uploads
                var uploadsFolder = Path.Combine("wwwroot/uploads");
                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                var uniqueFileName = Guid.NewGuid().ToString() + "_" + vehicle_document.FileName;
                var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await vehicle_document.CopyToAsync(stream);
                }

                // Save the file path or filename to the database
                model.vehicle_document = System.IO.File.ReadAllBytes(filePath);
            }
            model.vehicle_status = "Pending"; // or whatever default status
            // Save the vehicle model to the DB using your service or context
            _context.Vehicle.Add(model);
            await _context.SaveChangesAsync();

            return RedirectToAction("UserVehicle");
        }

        [HttpPost]
        public async Task<IActionResult> UpdateVehicle(Vehicle vehicle)
        {
            var existingVehicle = await _context.Vehicle.FirstOrDefaultAsync(v => v.plate_number == vehicle.plate_number);
            if (existingVehicle == null)
            {
                return NotFound();
            }

            existingVehicle.vehicle_type = vehicle.vehicle_type;
            existingVehicle.vehicle_model = vehicle.vehicle_model;
            existingVehicle.vehicle_brand = vehicle.vehicle_brand;
            existingVehicle.vehicel_color = vehicle.vehicel_color;

            if (Request.Form.Files.Count > 0)
            {
                using (var ms = new MemoryStream())
                {
                    await Request.Form.Files[0].CopyToAsync(ms);
                    existingVehicle.vehicle_document = ms.ToArray();
                }
            }

            await _context.SaveChangesAsync();
            return RedirectToAction("UserVehicle");
        }
        public IActionResult SecurityCalendar()
        {
            ViewData["HideSearch"] = true;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UpdateStatusByPlate([FromBody] VehicleStatusUpdateRequest request)
        {
            var vehicle = await _context.Vehicle.FirstOrDefaultAsync(v => v.plate_number == request.PlateNumber);
            if (vehicle == null)
            {
                return NotFound();
            }

            vehicle.vehicle_status = request.Status;
            _context.Update(vehicle);
            await _context.SaveChangesAsync();

            return Ok(new { success = true, newStatus = request.Status });
        }

        public class VehicleStatusUpdateRequest
        {
            public string PlateNumber { get; set; }
            public string Status { get; set; }
        }



        public async Task<IActionResult> MaintenanceServiceRequestAsync()
        {
            ViewData["HideSearch"] = true;
            var userId = User.FindFirst("UserId")?.Value;
            var requests = await _context.Service_Request
                .Where(p => p.Assigned_Staff == userId)
                .OrderByDescending(p => p.Request_Date)
                .ToListAsync();

            var viewModel = new ServiceRequestManagementViewModel
            {
                ServiceRequests = requests,
                 Users = await _userService.GetAllUsers() // Fetch users from DB
            };

            return View(viewModel);
        }

        public IActionResult MaintenanceCalendar()
        {
            ViewData["HideSearch"] = true;
            return View();

        }
        public async Task<IActionResult> IncidentReportAsync()
        {
            ViewData["HideSearch"] = true;
            var reports = new List<dynamic>
            {
                new {Status = "Pending", Title = "Broken Light Post", Type = "Incident Report", Description = "There’s a broken light post in street xxxx, it’s very hard to see...", Icon = "incident_icon.svg", Clock = "clock.svg", Duration = "2 Days ago"},
                new {Status = "Resolved", Title = "Street Hole", Type = "Incident Report", Description = "In the street xxx, there’s a massive hole filled with water, the vehicles...", Icon = "incident_icon.svg", Clock = "clock.svg", Duration = "3 Days ago" }
            };
            var userId = User.FindFirst("UserId")?.Value;
            var incidentreports = new IncidentReportsViewModel
            {
                IncidentReports = await _context.Incident_Reports
                    .Where(p => p.user_id == userId)
                    .OrderByDescending(p => p.creation_date)
                    .ToListAsync(),
                Users = await _userService.GetAllUsers(), // Fetch users from DB
                User = await _context.Users.FirstOrDefaultAsync(u => u.user_id == userId)
            };
            return View(incidentreports);
        }

        [HttpPost]
        public async Task<IActionResult> SubmitIncidentReport(IFormCollection form, IFormFile report_image)
        {
            var title = form["report_title"];
            var description = form["report_description"];
            var userId = User.FindFirst("UserId")?.Value;

            byte[] imageBytes = null;

            if (report_image != null && report_image.Length > 0)
            {
                using (var ms = new MemoryStream())
                {
                    await report_image.CopyToAsync(ms);
                    imageBytes = ms.ToArray();
                }
            }

            var newReport = new IncidentReports
            {
                user_id = userId,
                report_title = title,
                report_description = description,
                report_status = "Pending",
                creation_date = DateTime.Now,
                report_image = imageBytes
            };

            _context.Incident_Reports.Add(newReport);
            await _context.SaveChangesAsync();

            return RedirectToAction("IncidentReport");
        }

        public IActionResult SecurityServiceRequest ()
        {
            ViewData["HideSearch"] = true;
            return View();
        }
       public IActionResult serviceRequest()
        {
            ViewData["Title"] = "Service Request";
            ViewData["HideSearch"] = true;
            return View();
        }



        public IActionResult Calendar()
        {
            ViewData["HideSearch"] = true;
            return View();
        }
        public IActionResult Reports()
        {
            ViewData["Title"] = "Reports";
            return View();
        }
        public async Task<IActionResult> HousekeepingServiceRequestAsync()
        {
            var userId = User.FindFirst("UserId")?.Value;
            var requests = await _context.Service_Request
                .Where(p => p.Assigned_Staff == userId)
                .OrderByDescending(p => p.Request_Date)
                .ToListAsync();

            var viewModel = new ServiceRequestManagementViewModel
            {
                ServiceRequests = requests
            };
            ViewData["HideSearch"] = true;

            return View(viewModel);
        }
        public IActionResult HouseKeepingCalendar()
        {
            ViewData["HideSearch"] = true;
            return View();
        }
        public IActionResult SecurityVehicleManagement()
        {
            ViewData["HideSearch"] = true;
            return View();
        }
        public IActionResult VisitorManagement()
        {
            ViewData["HideSearch"] = true;
            return View();
        }
        public async Task<IActionResult> UserFeedbackAsync()
        {
            ViewData["HideSearch"] = true;

            var feedback1 = new List<dynamic>
            {
                new {Icon = "ratings.svg", Title = "Great User Experience", Type = "Feedback", Description = "The Interface is intuitive and easy to navigate. I particularly enjoyedthe smooth transitions and..", Clock = "clock.svg", Duration = "2 Days ago"},
                new {Icon = "ratings.svg", Title = "Great User Exponent", Type = "Feedback", Description = "The Interface is intuitive and easy to navigate. I particularly enjoyedthe smooth transitions and..", Clock = "clock.svg", Duration = "2 Days ago" },

            };

            var complaint = new List<dynamic>
            {
                new {Status = "Pending", Title = "Noise Complaint in Block A", Type = "Complaint", Description = "Continuous loud music from unit 302 during quiet hours...", Clock = "clock.svg", Duration = "2 Days ago"},
                new {Status = "Resolved", Title = "Noise Complaint in Block B", Type = "Complaint", Description = "Continuous loud music from unit 302 during quiet hours...", Clock = "clock.svg", Duration = "2 Days ago" },
            };

            ViewBag.Complaints = complaint;
            var userId = User.FindFirst("UserId")?.Value;

            var feedback = new FeedbackViewModel
            {
                Feedback = await _feedbackService.GetAllFeedback(), // Await the Task to resolve the issue
                Users = await _userService.GetAllUsers(), // Fetch users from DB
                User = await _context.Users.FirstOrDefaultAsync(u => u.user_id == userId)
            };

            return View(feedback);
        }

        public IActionResult feedback()
        {
            ViewData["HideSearch"] = true;
            ViewData["Hidebtn"] = true;

            var manageReports = new List<dynamic>
            {
                new { Title = "Total Feedback", Count = 123, Icon = "mailfeed.svg", BorderColor = "border-blue-400 border-b-2" },
                new { Title = "Total Complaints", Count = 34, Icon = "complaints.svg", BorderColor = "border-red-400 borber-b-2" },
                new { Title = "Total Reports", Count = 56, Icon = "activev.svg", BorderColor = "border-green-400 borber-b-2" },
                new { Title = "Average Satisfaction", Count = 10, Icon = "satisfaction.svg", BorderColor = "border-yellow-400 borber-b-2" }

            };
            return View(manageReports);
        }

        [HttpPost]
        public IActionResult SubmitFeedback(Feedback feedback)
        {
            if (ModelState.IsValid)
            {
                feedback.SubmittedAt = DateTime.Now;
                feedback.Type = "Feedback";
                feedback.Status = "N/A";
                _context.Feedback.Add(feedback);
                _context.SaveChanges();
                return RedirectToAction("UserFeedback");
            }
            return View("UserFeedback");
        }


        [HttpPost]
        public IActionResult SubmitComplaint(Feedback feedback)
        {
            if (ModelState.IsValid)
            {
                feedback.SubmittedAt = DateTime.Now;
                feedback.Type = "Complaint";
                feedback.Status = "Pending";
                _context.Feedback.Add(feedback);
                _context.SaveChanges();

                return RedirectToAction("UserFeedback");
            }

            TempData["Error"] = "Failed to submit complaint. Please try again.";
            return RedirectToAction("UserFeedback");
        }




        public IActionResult ContactDirectory()
        {
            var emergencies = new List<dynamic>
            {
                new { Title = "Medical Emergency", Number = "911", Description = "24/7 Emergency Response", Icon = "medic.svg", CallIcon = "phone-red.svg", BgColor = "bg-red-50", BorderColor = "border-red-500", TextColor = "text-red-700" },
                new { Title = "Fire Department", Number = "+1 234 567 891", Description = "Fire Emergency Response", Icon = "fire-exting.svg", CallIcon = "phone-orange.svg", BgColor = "bg-orange-50", BorderColor = "border-orange-500", TextColor = "text-orange-700" },
                new { Title = "Police Station", Number = "+1 234 567 890", Description = "24/7 Security Hotline", Icon = "protec.svg", CallIcon = "phone-blue.svg", BgColor = "bg-blue-50", BorderColor = "border-blue-500", TextColor = "text-blue-700" }
            };

            return View(emergencies);
        }
        public JsonResult GetEvents()
        {
            var events = new List<object>
        {
            new { title = "Meeting", start = "2025-03-20" },
            new { title = "Conference", start = "2025-03-25" },
            new { title = "Workshop", start = "2025-03-28" }
        };

            return Json(events);
        }


        public async Task<IActionResult> FacilityManagementAsync()
        {
            var facilities = await _facilityService.GetAllFacilitiesWithHoursAsync();

            var viewModels = facilities.Select(f => new FacilityViewModel
            {
                FacilityId = f.Facility_Id,
                FacilityName = f.Facility_Name,
                FacilityCategory = f.Facility_Category,
                FacilityDescription = f.Facility_Description,
                ImagePath = f.Facility_Img,
                ServiceFeePerHour = f.Service_Fee_Per_Hour,
                FacilityGuidelines = f.Facility_Guidelines,
                FacilityAminities = f.Facility_Aminities,
                FacilityStatus = f.Facility_Status,
                OperatingHours = f.OperatingHours.Select(h => new FacilityViewModel.FacilityOperatingHours
                {
                    Facility_Id = h.Facility_Id,
                    Day_Of_Week = h.Day_Of_Week,
                    Opening_Time = h.Opening_Time,
                    Closing_Time = h.Closing_Time,
                    Facility = h.Facility
                }).ToList(), // Ensure this is a List<FacilityViewModel.FacilityOperatingHours>
            }).ToList();

            ViewData["HideSearch"] = true;

            var reservations = new List<dynamic>
            {
                new { Title = "Total Facilities", Count = 10, Icon = "~/Images/dafac.svg", BorderColor = "border-b-[3px] border-blue-500" },
                new { Title = "Currently Booked", Count = 3, Icon = "~/Images/pendi.svg", BorderColor = "border-b-[3px] border-yellow-400" },
                new { Title = "Maintenance", Count = 4, Icon = "~/Images/canc.svg", BorderColor = "border-b-[3px] border-red-400" },
                new { Title = "Available Facilities", Count = 3, Icon = "~/Images/apr.svg", BorderColor = "border-b-[3px] border-green-400" }
            };

            ViewBag.Reservations = reservations;

            return View("FacilityManagement", viewModels); // Removed the third argument to fix CS1501
        }



        [HttpPost]
        public async Task<IActionResult> AddFacility(FacilityViewModel model)
        {
            // Define the list of operating days
            var operatingDays = new List<string> { "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday" };

            // Generate Facility ID
            var lastFacility = await _facilityService.GetLastFacilityAsync();
            int nextIdNumber = 1;
            if (lastFacility != null && !string.IsNullOrEmpty(lastFacility.Facility_Id))
            {
                var idPart = lastFacility.Facility_Id.Replace("FAC-", "");
                if (int.TryParse(idPart, out int numericId))
                {
                    nextIdNumber = numericId + 1;
                }
            }
            string newFacilityId = $"FAC-{nextIdNumber.ToString("D4")}";
            model.FacilityId = newFacilityId;
            ModelState.Remove(nameof(model.FacilityId));

            // Handle image upload
            if (model.ImageFile != null && model.ImageFile.Length > 0)
            {
                var uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "uploads");
                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                var uniqueFileName = Guid.NewGuid().ToString() + "_" + model.ImageFile.FileName;
                var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await model.ImageFile.CopyToAsync(fileStream);
                }

                model.ImagePath = "/uploads/" + uniqueFileName;
            }

            // Process operating hours from form data
            model.OperatingHours = new List<FacilityViewModel.FacilityOperatingHours>();
            foreach (var day in operatingDays) // Use the same list from your view
            {
                // Check if this day was selected
                var isDaySelected = Request.Form["SelectedDays"].Contains(day);

                if (isDaySelected)
                {
                    // Get the times for this day
                    var openingTimeStr = Request.Form[$"OpeningTime_{day}"].ToString();
                    var closingTimeStr = Request.Form[$"ClosingTime_{day}"].ToString();

                    if (TimeSpan.TryParse(openingTimeStr, out var openingTime) &&
                        TimeSpan.TryParse(closingTimeStr, out var closingTime))
                    {
                        model.OperatingHours.Add(new FacilityViewModel.FacilityOperatingHours
                        {
                            Day_Of_Week = day,
                            Opening_Time = openingTime,
                            Closing_Time = closingTime,
                            Facility_Id = model.FacilityId
                        });
                    }
                }
            }

            if (ModelState.IsValid)
            {
                var facility = new Facility
                {
                    Facility_Id = model.FacilityId,
                    Facility_Name = model.FacilityName,
                    Facility_Category = model.FacilityCategory,
                    Facility_Description = model.FacilityDescription,
                    Service_Fee_Per_Hour = model.ServiceFeePerHour,
                    Facility_Guidelines = model.FacilityGuidelines,
                    Facility_Aminities = model.FacilityAminities,
                    Facility_Status = model.FacilityStatus,
                    Facility_Img = model.ImagePath
                };

                // Convert operating hours to the correct type
                var operatingHours = model.OperatingHours.Select(oh => new FacilityOperatingHour
                {
                    Facility_Id = oh.Facility_Id,
                    Day_Of_Week = oh.Day_Of_Week,
                    Opening_Time = oh.Opening_Time ?? TimeSpan.Zero,
                    Closing_Time = oh.Closing_Time ?? TimeSpan.Zero
                }).ToList();

                // Fix: Pass the list of operating hours instead of a single object
                await _facilityService.AddFacilityAsync(facility, operatingHours);

                return RedirectToAction("FacilityManagement");
            }

            // If we got this far, something failed
            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> UpdateFacilityStatus(string facilityId, string status)
        {
            try
            {
                // Validate status
                if (status != "Available" && status != "Unavailable")
                {
                    return Json(new { success = false, message = "Invalid status" });
                }

                // Update the facility status in your database
                var facility = await _context.Facilities.FindAsync(facilityId);
                if (facility == null)
                {
                    return Json(new { success = false, message = "Facility not found" });
                }

                facility.Facility_Status = status;
                await _context.SaveChangesAsync();

                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }






        [HttpPost]
        public async Task<IActionResult> CreateReservation(ReservationViewModel model)
        {
            // Generate new Reservation ID
            var lastReservation = await _reservationService.GetLastReservationAsync();
            int nextIdNumber = 1;

            if (lastReservation != null && !string.IsNullOrEmpty(lastReservation.reservation_id))
            {
                var idPart = lastReservation.reservation_id.Replace("RES-", "");
                if (int.TryParse(idPart, out int numericId))
                { 
                    nextIdNumber = numericId + 1;
                }
            }

            var userId = User.FindFirst("UserId")?.Value;
            var currentuser = await _context.Users.FirstOrDefaultAsync(u => u.user_id == userId);
            model.User = currentuser;
            string newReservationId = $"RES-{nextIdNumber.ToString("D4")}";
            model.ReservationId = newReservationId;
            ModelState.Remove(nameof(model.User));
            ModelState.Remove(nameof(model.ReservationId));

            if (ModelState.IsValid)
            {
                var reservation = new Reservation
                {
                    reservation_id = model.ReservationId,
                    facility_id = model.FacilityId ?? string.Empty,
                    user_id = User.Identity.Name,
                    reservation_date = model.ReservationDate,
                    time_in = model.TimeIn,
                    time_out = model.TimeOut,
                    reservation_purpose = model.ReservationPurpose ?? string.Empty,
                    reservation_status = "Pending"
                };

                await _reservationService.AddReservationAsync(reservation);
                return RedirectToAction("FacilityReservation");
            }

            // If we got this far, something failed
            return RedirectToAction("FacilityReservation");
        }



        public async Task<IActionResult> ReservationsManagement()
        {
            ViewData["HideSearch"] = true;
            ViewData["Hidebtn"] = true;

            var reservations = new List<dynamic>
            {
                new { Title = "Total Bookings", Count = 248, Icon = "booking.svg", BorderColor = "border-blue-400 border-b-2" },
                new { Title = "Pending Approvals", Count = 3, Icon = "pendi.svg", BorderColor = "border-yellow-400 borber-b-2" },
                new { Title = "Cancelled Reservations", Count = 15, Icon = "canc.svg", BorderColor = "border-red-400 borber-b-2" },
                new { Title = "Approved Reservations", Count = 23, Icon = "apr.svg", BorderColor = "border-green-400 borber-b-2" }
            };

            var reservationsList = new ReservationViewModel
            {
                Reservations = (await _reservationService.GetAllReservationAsync())
                    .Select(r => ReservationViewModel.FromModel(r))
                    .ToList(),

                Users = await _userService.GetAllUsers(), // Fetch users from DB
            };

            return View(reservationsList);
        }

        public class ReservationStatusUpdateDto
        {
            public string ReservationId { get; set; }
            public string NewStatus { get; set; }
        }

        [HttpPost]
        public async Task<IActionResult> UpdateReservationStatus([FromBody] ReservationStatusUpdateDto data)

        {
            var reservation = await _reservationService.GetReservationByIdAsync(data.ReservationId);
            if (reservation == null)
            {
                return NotFound(new { success = false, message = "Reservation not found." });
            }

            reservation.reservation_status = data.NewStatus;

            try
            {
                await _reservationService.UpdateReservationAsync(reservation);
                return Ok(new { success = true, message = $"Reservation status updated to {reservation.reservation_status}." });
            }
            catch (Exception ex)
            {
                // Optional: Log the exception
                return StatusCode(500, new { success = false, message = "An error occurred while updating the reservation status.", error = ex.Message });
            }
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Landing", "Home"); // Redirect to the landing page
        }


        [HttpPost]
        public async Task<IActionResult> Login(string username, string password)
        {
            // Check predefined admin and user credentials
            if (username == "admin" && password == "a")
            {
                return await SignInUser("Admin", "", "AdminDash", "admin");
            }
            else if (username == "user" && password == "u")
            {
                return await SignInUser("User", "", "UserDash", "user");
            }
            // Handle staff login with predefined structure
            else if (username.StartsWith("staff/") && password == "s")
            {
                string[] parts = username.Split('/');
                if (parts.Length == 2)
                {
                    string staffType = parts[1];
                    return await SignInUser(username, "UserDash", "staff", staffType);
                }
                else
                {
                    TempData["Error"] = "Invalid Staff Username Format!";
                    return RedirectToAction("Landing");
                }
            }
            // Fetch user from database
            else
            {
                var userAccount = await _userService.GetUserByUsername(username);

                if (userAccount != null && userAccount.password == password) // (Replace with password hashing later)
                {
                    var userDetails = await _userService.GetUserDetailsById(userAccount.user_id);
                    if (userDetails != null)
                    {
                        string role = userDetails.type_of_user switch
                        {
                            1 => "admin",
                            2 => "homeowner",
                            3 => "housekeeping",
                            4 => "maintenance",
                            5 => "security",
                            _ => "user"
                        };
                        return await SignInUser(userDetails.first_name, userDetails.last_name, GetRedirectAction(role), role, userDetails.user_id, userDetails.profile_picture);
                    }
                }
            }

            TempData["Error"] = "Invalid Credentials!";
            return RedirectToAction("Landing");
        }

        // Fix for CS1001 and CS1737: Correcting the method signature by adding a parameter name for the byte[] parameter
        // and ensuring optional parameters appear after all required parameters.

        private async Task<IActionResult> SignInUser(
            string first_name,
            string last_name,
            string redirectAction,
            string role = "",
            string additionalClaim = "",
            byte[] profilePicture = null)
                {
                    var username = first_name + " " + last_name;

                    var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, username)
            };

            if (!string.IsNullOrEmpty(role))
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            if (!string.IsNullOrEmpty(additionalClaim))
            {
                claims.Add(new Claim("UserId", additionalClaim));
            }

            // ✅ Save profile picture and add URL as a claim
            if (profilePicture != null && profilePicture.Length > 0)
            {
                // Generate unique filename
                var fileName = Guid.NewGuid().ToString() + ".jpg";
                var filePath = Path.Combine("wwwroot/uploads", fileName);

                // Save image to wwwroot/uploads/
                await System.IO.File.WriteAllBytesAsync(filePath, profilePicture);

                // Generate accessible URL (adjust based on your hosting)
                var profilePictureUrl = "/uploads/" + fileName;

                // Add URL as a claim
                claims.Add(new Claim("ProfilePictureUrl", profilePictureUrl));
            }

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var authProperties = new AuthenticationProperties();

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                authProperties
            );

            return RedirectToAction(redirectAction, "Home");
        }



        private string GetRedirectAction(string role)
        {
            return role?.ToLower() switch
            {
                "admin" => "AdminDash",
                "maintenance" => "UserDash",
                "housekeeping" => "UserDash",
                "security" => "UserDash",
                _ => "UserDash"
            };
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


    }
}
