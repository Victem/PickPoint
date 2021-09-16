using PickPoint.Data.DB;
using PickPoint.Data.Entities;
using PickPoint.Data.Enums;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PickPoint.Infrastructure
{
    public class OrderDbRepository : DbRepository<Order, int>
    {
        public OrderDbRepository(PickPointContext context) : base(context)
        {

        }

        public override Order Create(Order item)
        {
            item.Status = OrderStatus.Registered;
            return base.Create(item);
        }

        public override void Remove(Order item)
        {
            item.Status = OrderStatus.Canceled;
            base.Update(item);
        }
    }
}
