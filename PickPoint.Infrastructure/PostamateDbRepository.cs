using Microsoft.EntityFrameworkCore;

using PickPoint.Data.DB;
using PickPoint.Data.Entities;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PickPoint.Infrastructure
{
    public class PostamateDbRepository : DbRepository<Postamate, string>
    {
        public PostamateDbRepository(PickPointContext context) : base(context)
        {

        }

        public override IEnumerable<Postamate> Get()
        {
            return _entity.AsNoTracking()
                .Where(p=> p.Status)
                .OrderBy(p=> p.Id)
                .ToList();
        }
    }
}