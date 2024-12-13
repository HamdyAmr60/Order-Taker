using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Order_Taker.Core.Models;
using Order_Taker.Core.Reposatories;
using Order_Taker.Repositoriy.Reposatories;

namespace Order_Taker.client.API.Controllers
{
    
    public class BasketController : APIBaseController
    {
        private readonly IBasketRepo _basketRepo;

        public BasketController(IBasketRepo basketRepo)
        {
            _basketRepo = basketRepo;
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerBasket>> GetBasket(string id)
        {
          var reslt =  await  _basketRepo.GetBasketAsync(id);
            if (reslt is null) return new CustomerBasket(id);
            return Ok(reslt);
        }

        [HttpPost]
        public async Task<ActionResult<CustomerBasket>> CreateBasket(CustomerBasket basket) 
        {
        var result =   await  _basketRepo.UpdateBasketAsync(basket);
            if (result is null) return BadRequest();
            return Ok(result);
        }
        [HttpDelete]
        public async Task<ActionResult<bool>> DeleteBasket(string id)
        {
            return await _basketRepo.DeleteBasketAsync(id);
        }
    }
}
    