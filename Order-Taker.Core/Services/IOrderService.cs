using Order_Taker.Core.Models.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order_Taker.Core.Services
{
    public interface IOrderService
    {
        Task<Order> CreateOrder(string BuyerEmail, string BasketId, int DeliveryMethodId, Address ShippingAddress);
        Task<IReadOnlyList<Order>> GetOrders(string BuyerEmail);
        Task<Order>GetOrder(int OrderId, string BuyerEmail);
    }
}
