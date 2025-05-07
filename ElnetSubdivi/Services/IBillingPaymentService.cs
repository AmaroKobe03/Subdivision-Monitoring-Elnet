// Services/IBillingPaymentService.cs
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

    }
}

