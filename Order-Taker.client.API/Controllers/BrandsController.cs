using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Order_Taker.Core.Models;
using Order_Taker.Core.Reposatories;

namespace Order_Taker.client.API.Controllers
{
    public class BrandsController : APIBaseController
    {
        private readonly IOrderTakerRepo<ProductBrand> _orderTaker;

        public BrandsController(IOrderTakerRepo<ProductBrand> orderTaker)
        {
           _orderTaker = orderTaker;
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> Get() 
        { 
            var result = await _orderTaker.GetAll();

            return Ok(result);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductBrand>> Get(int id)
        {
            var Result = await _orderTaker.Get(id);
            return Ok(Result);
        }
    }
}
