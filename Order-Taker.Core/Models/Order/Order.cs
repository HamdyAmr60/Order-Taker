﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order_Taker.Core.Models.Order
{
    public class Order :BaseModel
    {
        public Order()
        {
            
        }
        public Order(string buyerEmail, Address shippingAddress, DeliveryMethod deliveryMethod, ICollection<OrderItem> items, decimal subTotal)
        {
            BuyerEmail = buyerEmail;
            ShippingAddress = shippingAddress;
            DeliveryMethod = deliveryMethod;
            Items = items;
            SubTotal = subTotal;
        }

        public string BuyerEmail {  get; set; }
        public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.Now;
        public OrderStatus Status { get; set; } = OrderStatus.Pending;
        public Address ShippingAddress { get; set; }

        public DeliveryMethod DeliveryMethod { get; set; }
        public ICollection<OrderItem> Items { get; set;} = new HashSet<OrderItem>();
        public decimal SubTotal { get; set; }
        public string PaymentIntentId { get; set; } = string.Empty;


        public decimal GetTotal()
        {
            return SubTotal + DeliveryMethod.Cost;
        }
    }
}
