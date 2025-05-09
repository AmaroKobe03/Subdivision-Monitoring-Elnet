using ElnetSubdivi.data;
using ElnetSubdivi.Models;
using Microsoft.EntityFrameworkCore;

namespace ElnetSubdivi.Services
{
    public class FeedbackService : IFeedbackService
    {
        private readonly ApplicationDbContext _context;
        public FeedbackService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<List<Feedback>> GetAllFeedback()
        {
            var feedback = await _context.Feedback.ToListAsync();
            return feedback ?? new List<Feedback>(); // Ensure it never returns null  
        }
    }
}
