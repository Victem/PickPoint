using Microsoft.EntityFrameworkCore;

using PickPoint.Data.DB;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PickPoint.Infrastructure
{
    public class DbRepository<TEntity, TKey> : IRepository<TEntity, TKey>
        where TEntity : class
    {
        protected readonly PickPointContext _context;
        protected DbSet<TEntity> _entity;
        public DbRepository(PickPointContext context) 
        {
            _context = context;
            _entity = context.Set<TEntity>();
        }
        public virtual TEntity Create(TEntity item)
        {
            _entity.Add(item);
            _context.SaveChanges();
            return item;
        }

        public virtual TEntity FindById(TKey id)
        {
            return _entity.Find(id);
        }

        public virtual IEnumerable<TEntity> Get()
        {
            return _entity.AsNoTracking()
                .ToList();
        }

        public virtual IEnumerable<TEntity> Get(Func<TEntity, bool> predicate)
        {
            return _entity.AsNoTracking()
                .Where(predicate)
                .ToList();
        }

        public virtual void Remove(TEntity item)
        {
            _entity.Remove(item);
            _context.SaveChanges();
        }

        public virtual void Update(TEntity item)
        {
            _context.Entry(item).State = EntityState.Modified;
            _context.SaveChanges();
        }
    }
}
