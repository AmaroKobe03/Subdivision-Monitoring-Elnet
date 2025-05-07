using System;
using System.ComponentModel.DataAnnotations;
using ElnetSubdivi.Models;

namespace ElnetSubdivi.ViewModels
{
    public class ReservationViewModel
    {
        // Read-only properties for display
        public string ReservationId { get; set; } = string.Empty;
        public string UserId { get; set; } = string.Empty;

        // Editable properties with validation
        [Required(ErrorMessage = "Facility is required")]
        [Display(Name = "Facility ID")]
        public string FacilityId { get; set; } = "FAC-0001";

        [Required(ErrorMessage = "Reservation date is required")]
        [Display(Name = "Reservation Date")]
        [DataType(DataType.Date)]
        public DateTime ReservationDate { get; set; } = DateTime.Today;

        [Required(ErrorMessage = "Start time is required")]
        [Display(Name = "Start Time")]
        [DataType(DataType.Time)]
        public TimeSpan TimeIn { get; set; } = TimeSpan.FromHours(9); // Default to 9:00 AM

        [Required(ErrorMessage = "End time is required")]
        [Display(Name = "End Time")]
        [DataType(DataType.Time)]
        [CustomValidation(typeof(ReservationViewModel), "ValidateTimeOut")]
        public TimeSpan TimeOut { get; set; } = TimeSpan.FromHours(17); // Default to 5:00 PM

        [Required(ErrorMessage = "Purpose is required")]
        [Display(Name = "Purpose")]
        [StringLength(500, ErrorMessage = "Purpose cannot exceed 500 characters")]
        public string ReservationPurpose { get; set; } = string.Empty;

        // ✅ NEW: Reservation Status
        [Display(Name = "Status")]
        [StringLength(50, ErrorMessage = "Status cannot exceed 50 characters")]
        public string ReservationStatus { get; set; } = "Pending";

        // Additional UI properties
        public string FacilityName { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public bool IsEditable { get; set; } = true;

        // Validation method for time out
        public static ValidationResult ValidateTimeOut(TimeSpan timeOut, ValidationContext context)
        {
            var instance = (ReservationViewModel)context.ObjectInstance;
            if (timeOut <= instance.TimeIn)
            {
                return new ValidationResult("End time must be after start time");
            }
            return ValidationResult.Success;
        }

        // Mapping methods
        public static ReservationViewModel FromModel(Reservation reservation)
        {
            return new ReservationViewModel
            {
                ReservationId = reservation.reservation_id,
                FacilityId = reservation.facility_id,
                UserId = reservation.user_id,
                ReservationDate = reservation.reservation_date,
                TimeIn = reservation.time_in,
                TimeOut = reservation.time_out,
                ReservationPurpose = reservation.reservation_purpose,
                ReservationStatus = reservation.reservation_status // ✅ Added
            };
        }

        public Reservation ToModel()
        {
            return new Reservation
            {
                reservation_id = ReservationId,
                facility_id = FacilityId,
                user_id = UserId,
                reservation_date = ReservationDate,
                time_in = TimeIn,
                time_out = TimeOut,
                reservation_purpose = ReservationPurpose,
                reservation_status = ReservationStatus // ✅ Added
            };
        }

        public List<Users> Users { get; set; }
        public List<ReservationViewModel> Reservations { get; set; } = new();
    }
}
