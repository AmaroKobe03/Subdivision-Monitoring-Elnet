using ElnetSubdivi.Models;

namespace ElnetSubdivi.Services
{
    public interface IFeedbackService
    {
        Task<List<Feedback>> GetAllFeedback();
    }
}
