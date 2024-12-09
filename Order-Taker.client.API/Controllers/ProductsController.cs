using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Order_Taker.Core.Models;
using Order_Taker.Core.Reposatories;

namespace Order_Taker.client.API.Controllers
{
  
    public class ProductsController : APIBaseController
    {
        private readonly IOrderTakerRepo<Product> _orderTaker;

        public ProductsController(IOrderTakerRepo<Product> orderTaker)
        {
            _orderTaker = orderTaker;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetAllProducts()
        {
            var result =    await _orderTaker.GetAllAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetById(int id)
        {
            var Result = await _orderTaker.GetAsync(id);
            return Ok(Result);
        }
    }
}
