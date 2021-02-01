using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DT.Domain.Base
{
    public interface IRepository<TEntity> : IDisposable where TEntity : class, IEntity
    {
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<TEntity> GetByIdAsync(Guid id);
        Task<bool> AddAsync(TEntity entity);
        Task<bool> AddListAsync(List<TEntity> entities);
        Task<bool> UpdateListAsync(List<TEntity> entities);
        Task<bool> UpdateAsync(TEntity entity);
        Task<int> SaveChangesAsync();
    }
}
