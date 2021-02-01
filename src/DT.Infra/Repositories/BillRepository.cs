using DT.Domain.Entities;
using DT.Domain.Interfaces;
using DT.Infra.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DT.Infra.Repositories
{
    public class BillRepository : RepositoryBase<Bill>, IBillRepository
    {
        public BillRepository(DTContext context) : base(context)
        {

        }

        public async Task<List<Bill>> GetBillsByUserAsync(string userId)
        {
            return await _dbSet.Where(x => x.User.Id == userId).ToListAsync();
        }

        public async Task<Bill> GetByIdByUserAsync(string userId, Guid id)
        {
            return await _dbSet.FirstOrDefaultAsync(x => x.Id == id &&  x.User.Id == userId);
        }
    }
}
