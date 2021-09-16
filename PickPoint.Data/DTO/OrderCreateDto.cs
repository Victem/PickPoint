using PickPoint.Data.Enums;

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PickPoint.Data.DTO
{
    public class OrderCreateDto
    {

        [Required]
        [MinLength(1)]
        [MaxLength(10)]
        public string[] Items { get; set; }

        [Required]
        public decimal Price { get; set; }
        
        [RegularExpression(@"^\d{4}-\d{3}$")]
        public string PostamateId { get; set; }

        [Required]
        [RegularExpression(@"^\+7\d{3}-\d{3}-\d{2}-\d{2}$")]
        public string Phone { get; set; }

        [Required]
        public string Customer { get; set; }
    }
}
