using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PickPoint.Data.Entities
{
    public class Postamate
    {
        public string Id { get; set; }
        public string Address { get; set; }
        public bool Status { get; set; }
        public List<Order> Orders { get; set; } = new List<Order>();
    }
}
