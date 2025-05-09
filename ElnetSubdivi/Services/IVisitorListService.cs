using ElnetSubdivi.Models;

namespace ElnetSubdivi.Services
{
    public interface IVisitorListService
    {
        Task<List<VisitorList>> GetAllVisitors();
        Task AddVisitorAsync(VisitorList visitor);
    }
}
