using DT.Domain.Base;
using DT.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DT.Domain.Interfaces
{
    public interface IBillRepository : IRepository<Bill>
    {
        Task<Bill> GetByIdByUserAsync(string userId, Guid id);
        Task<List<Bill>> GetBillsByUserAsync(string userId);
    }
}
