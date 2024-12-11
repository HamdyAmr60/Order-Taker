using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Order_Taker.Core.Models;
using Order_Taker.Core.Reposatories;

namespace Order_Taker.client.API.Controllers
{
   
    public class TypesController : APIBaseController
    {
        private readonly IOrderTakerRepo<ProductType> _takerRepo;

        public TypesController(IOrderTakerRepo<ProductType> takerRepo)
        {
            _takerRepo = takerRepo;
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<ProductType>>> GetAll() 
        { 
            var Result = await _takerRepo.GetAll();
            return Ok(Result);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductType>> Get(int id)
        {
            var Result = await _takerRepo.Get(id);
            return Ok(Result);
        }
    }
}
