using Microsoft.AspNetCore.Identity;
using Order_Taker.Core;
using Order_Taker.Core.Models;
using Order_Taker.Core.Models.Order;
using Order_Taker.Core.Reposatories;
using Order_Taker.Core.Services;
using Order_Taker.Core.Specifications.OrdersSpec;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order_Taker.Service
{
    public class OrderService : IOrderService
    {
        private readonly IBasketRepo _basketRepo;
        private readonly IUnitOfWork _unitOfWork;

        public OrderService(IBasketRepo basketRepo , IUnitOfWork unitOfWork)
        {
            _basketRepo = basketRepo;
            
            _unitOfWork = unitOfWork;
        }
        public async Task<Order> CreateOrder(string BuyerEmail, string BasketId, int DeliveryMethodId, Address ShippingAddress)
        {
            //1.Get Basket From Basket Repo
            var Basket = await _basketRepo.GetBasketAsync(BasketId);
            //2.Get Selected Items at Basket From Product Repo
            var OrderItems = new List<OrderItem>();
            if (Basket?.Items.Count> 0)
            {
                foreach (var item in Basket.Items)
                {
                    var Product = await _unitOfWork.repo<Product>().Get(item.Id);
                    var ProductOrdered = new ProductOrdered(Product.Id , Product.Name, Product.PictureUrl);
                    var OrderItem = new OrderItem(ProductOrdered, Product.Price, item.Quantity);
                    OrderItems.Add(OrderItem);
                }
            }

            //3.Calculate SubTotal
            var SubTotal = OrderItems.Sum(Product => Product.Price * Product.Quantity);
            //4.Get Delivery Method From DeliveryMethod Repo
            var Delivery = await _unitOfWork.repo<DeliveryMethod>().Get(DeliveryMethodId);
            //5.Create Order
            var Order = new Order(BuyerEmail,ShippingAddress, Delivery,OrderItems,SubTotal);
            //6.Add Order Locally
            //7.Save Order To Database[ToDo]

          await  _unitOfWork.repo<Order>().AddAsync(Order);
           await   _unitOfWork.SaveAsync();
            return Order;
        }

        public async Task<Order> GetOrder(int OrderId, string BuyerEmail)
        {
            var spec = new OrderSpec(OrderId, BuyerEmail);
            var Order = await _unitOfWork.repo<Order>().GetAsync(spec);
            return Order;
        }

        public async Task<IReadOnlyList<Order>> GetOrders(string BuyerEmail)
        {
            var spec = new OrderSpec(BuyerEmail);
        var Orders =   await  _unitOfWork.repo<Order>().GetAllAsync(spec);
            return Orders;
        }
    }
}
