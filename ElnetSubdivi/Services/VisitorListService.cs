using ElnetSubdivi.data;
using ElnetSubdivi.Models;
using ElnetSubdivi.Views.Shared;
using Microsoft.EntityFrameworkCore;

namespace ElnetSubdivi.Services
{
    public class VisitorListService : IVisitorListService
    {
        private readonly ApplicationDbContext _context;

        public VisitorListService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<List<VisitorList>> GetAllVisitors()
        {
            try
            {
                var posts = await _context.Visitor_List
                    .Select(u => new VisitorList
                    {
                        visitor_id = u.visitor_id,
                        first_name = u.first_name ?? string.Empty,
                        last_name = u.last_name,
                        phone_number = u.phone_number ?? string.Empty,
                        visit_date = u.visit_date,
                        visit_time = u.visit_time,
                        vehicle_plate = u.vehicle_plate ?? string.Empty,
                        visit_reason = u.visit_reason ?? string.Empty,
                        visit_status = u.visit_status ?? "Pending"
                    })
                    .ToListAsync();

                return posts;
            }
            catch (Exception ex)
            {
                // Log errors and return an empty list
                Console.WriteLine($"Error fetching users: {ex.Message}");
                return new List<VisitorList>();
            }
        }
        public async Task AddVisitorAsync(VisitorList visitor)
        {
            _context.Visitor_List.Add(visitor);
            await _context.SaveChangesAsync();
        }
    }
}
