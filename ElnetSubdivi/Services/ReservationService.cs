// Rename the class to avoid conflict with the existing 'PostService' class in the same namespace.
using ElnetSubdivi.data;
using ElnetSubdivi.Models;
using Microsoft.EntityFrameworkCore;

namespace ElnetSubdivi.Services
{
    public class ReservationService : IReservationService // Renamed from 'PostService' to 'ReservationPostService'
    {
        private readonly ApplicationDbContext _context;

        public ReservationService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Reservation>> GetAllReservationAsync()
        {
            var reservations = await _context.Reservation.ToListAsync();
            return reservations ?? new List<Reservation>(); // Ensure it never returns null
        }

        public async Task<List<Reservation>> GetReservationByUserAsync(string userId)
        {
            return await _context.Reservation
                                 .Where(p => p.user_id == userId)
                                 .OrderByDescending(p => p.reservation_date)
                                 .ToListAsync();
        }

        public async Task<List<Reservation>> GetAllReservation()
        {
            try
            {
                var reservations = await _context.Reservation
                    .Select(r => new Reservation
                    {
                        reservation_id = r.reservation_id ?? "",
                        facility_id = r.facility_id ?? "",
                        user_id = r.user_id ?? "",
                        reservation_date = r.reservation_date,
                        time_in = r.time_in,
                        time_out = r.time_out,
                        reservation_purpose = r.reservation_purpose,
                        reservation_status = r.reservation_status ?? ""
                    })
                    .ToListAsync();

                return reservations;
            }
            catch (Exception ex)
            {
                // Log errors and return an empty list
                Console.WriteLine($"Error fetching users: {ex.Message}");
                return new List<Reservation>();
            }
        }

        public async Task<Reservation> GetLastReservationAsync()
        {
            try
            {
                var reservations = await _context.Reservation
                    .Select(r => new Reservation
                    {
                        reservation_id = r.reservation_id ?? "RES-0000",
                        facility_id = r.facility_id ?? "FAC-0000",
                        user_id = r.user_id ?? string.Empty,
                        reservation_date = r.reservation_date,
                        time_in = r.time_in,
                        time_out = r.time_out,
                        reservation_purpose = r.reservation_purpose ?? string.Empty,
                        reservation_status = r.reservation_status ?? ""
                    })
                    .ToListAsync();

                // Get the reservation with the highest numeric ID
                var lastReservation = reservations
                    .Where(r => r.reservation_id.StartsWith("RES-"))
                    .OrderByDescending(r =>
                    {
                        var idPart = r.reservation_id.Replace("RES-", "");
                        return int.TryParse(idPart, out int num) ? num : 0;
                    })
                    .FirstOrDefault();

                return lastReservation;
            }
            catch (Exception ex)
            {
                // Log errors and return null
                Console.WriteLine($"Error fetching last reservation: {ex.Message}");
                return null;
            }
        }

        public async Task AddReservationAsync(Reservation reservation)
        {
            if (reservation == null)
                throw new ArgumentNullException(nameof(reservation));

            // Validate required fields
            if (string.IsNullOrEmpty(reservation.facility_id))
                throw new ArgumentException("Facility ID is required", nameof(reservation.facility_id));

            if (string.IsNullOrEmpty(reservation.user_id))
                throw new ArgumentException("User ID is required", nameof(reservation.user_id));

            if (reservation.time_in >= reservation.time_out)
                throw new ArgumentException("End time must be after start time");

            // Ensure reservation date is set (default to today if not specified)
            if (reservation.reservation_date == DateTime.MinValue)
            {
                reservation.reservation_date = DateTime.Today;
            }

            // Additional business logic validations can be added here
            // For example: check facility availability, user privileges, etc.

            await _context.Reservation.AddAsync(reservation);
            await _context.SaveChangesAsync();
        }

        public async Task<Reservation?> GetReservationByIdAsync(string reservationId)
        {
            return await _context.Reservation.FirstOrDefaultAsync(r => r.reservation_id == reservationId);
        }

        public async Task UpdateReservationAsync(Reservation reservation)
        {
            _context.Reservation.Update(reservation);
            await _context.SaveChangesAsync();
        }

    }
}
