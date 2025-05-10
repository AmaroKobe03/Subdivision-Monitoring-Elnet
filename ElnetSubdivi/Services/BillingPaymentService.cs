// Services/BillingPaymentService.cs
using ElnetSubdivi.data;
using ElnetSubdivi.Models;
using ElnetSubdivi.Views.Shared;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ElnetSubdivi.Services
{
    public class BillingPaymentService : IBillingPaymentService
    {
        private readonly ApplicationDbContext _context;
        public BillingPaymentService(ApplicationDbContext context)
        {
            _context = context;
        }
        // New DTO class for the basic user info
        public class UserBasicInfoDto
        {
            public string UserId { get; set; }
            public string FullName { get; set; }
        }

        public async Task<List<UserBasicInfoDto>> GetAllUsersBasicInfo()
        {
            try
            {
                var users = await _context.Users
                    .Select(u => new UserBasicInfoDto
                    {
                        UserId = u.user_id ?? "",
                        FullName = string.Join(" ",
                            u.first_name ?? "",
                            u.middle_name ?? "",
                            u.last_name ?? "").Trim()
                    })
                    .ToListAsync();

                return users;
            }
            catch (Exception ex)
            {
                // Log errors and return an empty list
                Console.WriteLine($"Error fetching users: {ex.Message}");
                return new List<UserBasicInfoDto>();
            }
        }


        // Updated the return type of GetAll() to match the interface definition.
        public async Task<List<BillingPaymentModel>> GetAll()
        {
            var bills = await _context.Billing_Statements.ToListAsync();
            return bills;
        }

        public async Task<List<BillingPaymentModel>> GetPaymentHistoryByUserId(string userId)
        {
            try
            {
                if (string.IsNullOrEmpty(userId))
                {
                    return new List<BillingPaymentModel>();
                }

                var payments = await _context.Billing_Statements
                    .Where(b => b.user_id == userId)
                    .OrderByDescending(b => b.billing_period)
                    .ToListAsync();

                return payments;
            }
            catch (Exception ex)
            {
                // Log error (optional)
                Console.WriteLine($"Error fetching payment history for UserId {userId}: {ex.Message}");
                return new List<BillingPaymentModel>();
            }
        }


        public async Task<BillingPaymentModel> Add(BillingPaymentModel model)
        {
            try
            {
                // Set default values
                model.bill_no = GenerateBillNumber();  // Better generation method

                // Add to context
                _context.Billing_Statements.Add(model);

                // Save changes
                await _context.SaveChangesAsync();

                return model;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private string GenerateBillNumber()
        {
            var random = new Random();
            return $"BILL-{random.Next(100000, 1000000)}";
        }

        public async Task Update(BillingPaymentModel model)
        {
            var existing = await _context.Billing_Statements.FindAsync(model.bill_no);
            if (existing != null)
            {
                existing.user_id = model.user_id;
                existing.bill_type = model.bill_type;
                existing.billing_period = model.billing_period;
                existing.due_date = model.due_date;
                existing.amount_due = model.amount_due;
                existing.description = model.description;

                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<Payments>> GetAllPayments()
        {
            try
            {
                var payments = await _context.Payments
                    .OrderByDescending(p => p.payment_date)
                    .ToListAsync();

                return payments;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching payments: {ex.Message}");
                return new List<Payments>();
            }
        }

        public async Task<decimal> GetTotalPaymentsAmount()
        {
            try
            {
                var total = await _context.Payments
                    .SumAsync(p => p.amount);

                return total;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error calculating total payments: {ex.Message}");
                return 0;
            }
        }

        public async Task<decimal> GetTotalPendingBillsAmount()
        {
            try
            {
                var totalPending = await _context.Billing_Statements
                    .Where(b => b.bill_status == "Pending")
                    .SumAsync(b => b.amount_due);

                return totalPending;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error calculating total pending bills: {ex.Message}");
                return 0;
            }
        }
    }
}
