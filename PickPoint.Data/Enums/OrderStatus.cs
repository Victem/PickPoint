using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PickPoint.Data.Enums
{
    public enum OrderStatus
    {
        Registered = 1,
        AcceptedAtWarehouse = 2,
        GivenToCourier = 3,
        DeliveredToPostamate = 4,
        DeliveredToCustomer = 5,
        Canceled
    }
}
