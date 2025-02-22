﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order_Taker.Core.Models.Order
{
    public class OrderItem :BaseModel
    {
        public OrderItem()
        {
            
        }
        public OrderItem(ProductOrdered product, decimal price, int quantity)
        {
            Product = product;
            Price = price;
            Quantity = quantity;
        }

        public ProductOrdered Product { get; set; }
        public decimal Price { get; set; }
        public int Quantity {  get; set; }
    }
}
