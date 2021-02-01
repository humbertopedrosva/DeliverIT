using DT.Domain.Base;
using DT.Infra.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DT.Infra.Repositories
{
    public abstract class RepositoryBase<TEntity> : IRepository<TEntity> where TEntity : class, IEntity
    {
        protected readonly DTContext _context;
        protected readonly DbSet<TEntity> _dbSet;
        public RepositoryBase(DTContext contexto)
        {
            _context = contexto;
            _dbSet = _context.Set<TEntity>();
        }

        public virtual async Task<IEnumerable<TEntity>> GetAllAsync() => await _dbSet.AsNoTracking().ToListAsync();

        public virtual async Task<TEntity> GetByIdAsync(Guid id) => await _dbSet.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);

        public async Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate) => await _dbSet.AsNoTracking().Where(predicate).ToListAsync();
        public async Task<int> SaveChangesAsync() => await _context.SaveChangesAsync();

        public virtual async Task<bool> AddAsync(TEntity entity)
        {
            _dbSet.Add(entity);
            return await SaveChangesAsync() > 0;
        }

        public async Task<bool> AddListAsync(List<TEntity> entities)
        {
            await _dbSet.AddRangeAsync(entities);
            return await SaveChangesAsync() == entities.Count();
        }

        public async Task<bool> UpdateListAsync(List<TEntity> entities)
        {
            _dbSet.UpdateRange(entities);
            return await SaveChangesAsync() == entities.Count();
        }

        public virtual async Task<bool> UpdateAsync(TEntity entity)
        {
            _dbSet.Update(entity);
            return await SaveChangesAsync() > 0;
        }
        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}
