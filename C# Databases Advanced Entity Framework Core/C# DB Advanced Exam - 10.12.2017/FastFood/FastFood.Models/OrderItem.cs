using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace FastFood.Models
{
    public class OrderItem
    {
        [Required]
        [ForeignKey(nameof(Order))]
        public int OrderId { get; set; }
        public Order Order { get; set; }

        [Required]
        [ForeignKey(nameof(Item))]
        public int ItemId { get; set; }
        public Item Item { get; set; }

        [Required]
        [Range(1,int.MaxValue)]
        public int Quantity { get; set; }
    }
}
