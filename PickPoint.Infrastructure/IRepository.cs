using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PickPoint.Infrastructure
{
    public interface IRepository<TEntity, TKey> 
        where TEntity : class
    {
        TEntity Create(TEntity item);
        TEntity FindById(TKey id);
        IEnumerable<TEntity> Get();
        IEnumerable<TEntity> Get(Func<TEntity, bool> predicate);
        void Remove(TEntity item);
        void Update(TEntity item);
    }
}
