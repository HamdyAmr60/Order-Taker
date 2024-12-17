using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Order_Taker.client.API.DTOs;
using Order_Taker.Core.Models;
using Order_Taker.Core.Reposatories;
using Order_Taker.Repositoriy.Reposatories;

namespace Order_Taker.client.API.Controllers
{
    
    public class BasketController : APIBaseController
    {
        private readonly IBasketRepo _basketRepo;
        private readonly IMapper _mapper;

        public BasketController(IBasketRepo basketRepo , IMapper mapper)
        {
            _basketRepo = basketRepo;
            _mapper = mapper;
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerBasket>> GetBasket(string id)
        {
          var reslt =  await  _basketRepo.GetBasketAsync(id);
            if (reslt is null) return new CustomerBasket(id);
            return Ok(reslt);
        }

        [HttpPost]
        public async Task<ActionResult<CustomerBasket>> CreateBasket(CustomerBasketDTO basket) 
        {
            var mappedBasket = _mapper.Map<CustomerBasketDTO,CustomerBasket>(basket);
        var result =   await  _basketRepo.UpdateBasketAsync(mappedBasket);
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
    