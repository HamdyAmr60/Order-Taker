using Order_Taker.Core.Models.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order_Taker.Core.Specifications.OrdersSpec
{
    public class OrderSpec : BaseSpecification<Order>
    {
        public OrderSpec(string Email) : base(O => O.BuyerEmail == Email)
        {
            Includes.Add(O => O.ShippingAddress);
            Includes.Add(O => O.Items);
            Includes.Add(O => O.DeliveryMethod);
            ApplyOrderBy(O => O.OrderDate);
        }
        public OrderSpec(int Id, string email) : base(O => O.Id == Id && O.BuyerEmail == email)
        {
            {
                Includes.Add(O => O.ShippingAddress);
                Includes.Add(O => O.Items);
                Includes.Add(O => O.DeliveryMethod);

            }
        }
    }
}
