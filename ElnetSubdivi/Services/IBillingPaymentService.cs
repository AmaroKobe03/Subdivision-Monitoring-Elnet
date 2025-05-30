﻿// Services/IBillingPaymentService.cs
using ElnetSubdivi.Models;
using System.Collections.Generic;
using static ElnetSubdivi.Services.BillingPaymentService;

namespace ElnetSubdivi.Services
{
    public interface IBillingPaymentService
    {
        Task<List<BillingPaymentModel>> GetAll();
        Task<BillingPaymentModel> Add(BillingPaymentModel model);
        Task<List<UserBasicInfoDto>> GetAllUsersBasicInfo();
        Task Update(BillingPaymentModel model);
        Task<List<BillingPaymentModel>> GetPaymentHistoryByUserId(string userId);
        Task<List<Payments>> GetAllPayments();
        Task<decimal> GetTotalPaymentsAmount();
        Task<decimal> GetTotalPendingBillsAmount();
    }
}

