using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Order_Taker.client.API.DTOs;
using Order_Taker.Core.Models.Order;
using Order_Taker.Core.Services;
using System.Security.Claims;

namespace Order_Taker.client.API.Controllers
{
    
    public class OrderController : APIBaseController
    {
        private readonly IOrderService _orderService;
        private readonly IMapper _mapper;

        public OrderController(IOrderService orderService , IMapper mapper)
        {
            this._orderService = orderService;
            _mapper = mapper;
        }
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<Order>> CreateOrder(OrderDTO orderDTO)
        {
            var BuyerEmail = User.FindFirstValue(ClaimTypes.Email);
            var MappedAddress = _mapper.Map<AddressDTO , Address>(orderDTO.Address);
           var Order = await _orderService.CreateOrder(BuyerEmail, orderDTO.BasketId, orderDTO.DeliveryMethodId, MappedAddress);
            if (Order == null) return BadRequest();
            return Ok(Order);
        }
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IReadOnlyList<Order>>> GetOrdersForUser()
        {
            var BuyerEmail = User.FindFirstValue(ClaimTypes.Email);
          var order = await  _orderService.GetOrders(BuyerEmail);
            if (order == null) return BadRequest();
            return Ok(order);
        }
        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<Order>> GetOrdersForUser(int id)
        {
            var BuyerEmail = User.FindFirstValue(ClaimTypes.Email);
            var order = await _orderService.GetOrder(id, BuyerEmail);
            if (order == null) return BadRequest();
            return Ok(order);
        }

    }
}
