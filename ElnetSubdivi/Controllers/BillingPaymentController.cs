using Microsoft.AspNetCore.Mvc;
using ElnetSubdivi.Models;
using ElnetSubdivi.Services;
using System;
using System.Collections.Generic;

namespace ElnetSubdivi.Controllers
{
    public class BillingPaymentController : Controller
    {
        private readonly IBillingPaymentService _billingService;


        public BillingPaymentController(IBillingPaymentService billingService)
        {
            _billingService = billingService;
        }
        public async Task<IActionResult> BillingManagement()
        {
            var bills = new BillingPaymentViewModel
            {
                BillStatements = await _billingService.GetAll() // Assume it returns List<BillingPaymentModel>
            };

            return View(bills);
        }

        public async Task<IActionResult> BillingManagement(string pageType = "PayBills")
        {
            var model = new BillingPaymentModel
            {
                PageType = pageType,
                Icons = new Dictionary<string, string>(),
                description = "Manage your bills below",
                amount = "0.00"
            };

            ViewData["Bills"] = await _billingService.GetAll();
            ViewData["Homeowners"] = await _billingService.GetAllUsersBasicInfo(); // Add this

            return View("BillingManagement", model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateInvoice(BillingPaymentModel model)
        {
            if (ModelState.IsValid)
            {
                model.bill_status = "Pending";
                _billingService.Add(model);
                TempData["Success"] = "Bill added successfully!";
                return RedirectToAction("BillingManagement");
            }

            ViewData["Bills"] = _billingService.GetAll();
            return View("BillingManagement", model);
        }
    }
}
