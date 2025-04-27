using ElnetSubdivi.data;
using ElnetSubdivi.Models;
using ElnetSubdivi.Services;
using ElnetSubdivi.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ElnetSubdivi.Controllers
{
    public class ReservationController : Controller
    {
        private readonly IUserService _userService;
        private readonly ApplicationDbContext _context;
        private readonly IPostService _postService;
        private readonly IReservationService _reservationService;

        public ReservationController(IUserService userService, ApplicationDbContext context, IPostService postService, IReservationService reservationService)
        {
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _postService = postService ?? throw new ArgumentNullException(nameof(postService));
            _reservationService = reservationService ?? throw new ArgumentNullException(nameof(reservationService));
        }

        public async Task<IActionResult> Index()
        {
            var reservations = await _reservationService.GetAllReservation() ?? new List<Reservation>(); // Ensures it's not null
            return View(reservations);
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

            string newReservationId = $"RES-{nextIdNumber.ToString("D4")}";
            model.ReservationId = newReservationId;
            ModelState.Remove(nameof(model.ReservationId));

            if (ModelState.IsValid)
            {
                var reservation = new Reservation
                {
                    reservation_id = model.ReservationId,
                    facility_id = model.FacilityId ?? string.Empty,
                    user_id = model.UserId ?? string.Empty,
                    reservation_date = model.ReservationDate,
                    time_in = model.TimeIn,
                    time_out = model.TimeOut,
                    reservation_purpose = model.ReservationPurpose ?? string.Empty
                };

                await _reservationService.AddReservationAsync(reservation);
                return RedirectToAction("FacilityReservation");
            }

            // If we got this far, something failed
            return View("FacilityReservation", model);
        }
    }
}
