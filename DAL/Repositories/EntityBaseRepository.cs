using Domain.interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class EntityBaseRepository<T> : IBaseRepository<T> where T : EntityBase, new()
    {
        private readonly ApplicationDbContext _context;

        public EntityBaseRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<T> GetSingle(string id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public virtual async Task<T> GetSingleWithPartitionKey(string id, string partitionKey)
        {
            throw new NotImplementedException();
        }

        public async Task Add(T entity)
        {
             _context.Set<T>().AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Update(string id, T entity)
        {
            _context.Set<T>().Update(entity);
            await _context.SaveChangesAsync();
        }


        public async Task Delete(string id)
        {
            var entity = await _context.Set<T>().FindAsync(id);
            _context.Set<T>().Remove(entity);
        }

        public async Task SaveChanges()
        {
            await _context.SaveChangesAsync();
        }
    }
}
