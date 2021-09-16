using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PickPoint.Data.Enums;

namespace PickPoint.Data.Entities
{
    public class Order
    {
        [Key]
        public int Id { get; set; }
        public OrderStatus Status { get; set; }
        public string[] Items { get; set; }
        public decimal Price { get; set; }
        public string PostamateId { get; set; }
        public string Phone { get; set; }
        public string Customer { get; set; }

        public Postamate Postamate {get; set;}
    }
}
