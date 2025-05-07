using ElnetSubdivi.Models;

namespace ElnetSubdivi.Services
{
    public interface IReservationService
    {
        Task<List<Reservation>> GetAllReservationAsync();
        Task<List<Reservation>> GetAllReservation();
        Task<Reservation> GetLastReservationAsync();
        Task<List<Reservation>> GetReservationByUserAsync(string userId);
        Task AddReservationAsync(Reservation reserevation);
    }
}
