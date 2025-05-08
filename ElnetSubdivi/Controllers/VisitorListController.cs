using ElnetSubdivi.Models;
using ElnetSubdivi.Services;
using Microsoft.AspNetCore.Mvc;

public class VisitorListController : Controller
{
    private readonly IVisitorListService _visitorListService;

    public VisitorListController(IVisitorListService visitorService)
    {
        _visitorListService = visitorService;
    }
    [HttpPost]
    public async Task<IActionResult> AddVisitor(VisitorList visitor)
    {
        if (ModelState.IsValid)
        {
            await _visitorListService.AddVisitorAsync(visitor);
            return RedirectToAction("UserVisitors"); // or wherever your list is
        }

        return View("UserVisitors", visitor); // return with errors
    }
}
