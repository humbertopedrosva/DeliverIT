using DT.Domain.Base;
using DT.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DT.Api.Application.Bills
{
    public interface IBillService
    {
        Task<List<BillModel>> GetAllBillsByUser(string userId);
        Task<List<BillModel>> GetAllBills();
        Task<BillModel> GetByIdByUser(Guid id, string userId);
    }
}
