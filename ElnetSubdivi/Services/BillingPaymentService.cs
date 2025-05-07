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

    }
}
