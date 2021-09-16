using PickPoint.Data.Enums;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PickPoint.Data.DTO
{
    public class OrderDto
    {
        public int Id { get; set; }
        public OrderStatus Status { get; set; }
        public string[] Items { get; set; }
        public decimal Price { get; set; }
        public string PostamateId { get; set; }
        public string Phone { get; set; }
        public string Customer { get; set; }
    }
}
