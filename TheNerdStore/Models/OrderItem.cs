﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TheNerdStore.Models
{
    public class OrderItem
    {
        public int ID { get; set; }
        public int ItemID { get; set; }
        public int OrderID { get; set; }
        public string ItemName { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal TotalCost { get; set; }
    }
}
