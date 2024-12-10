using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Order_Taker.client.API.DTOs;
using Order_Taker.Core.Models;
using Order_Taker.Core.Reposatories;
using Order_Taker.Core.Specifications.ProductSpec;

namespace Order_Taker.client.API.Controllers
{
  
    public class ProductsController : APIBaseController
    {
        private readonly IOrderTakerRepo<Product> _orderTaker;
        private readonly IMapper _mapper;

        public ProductsController(IOrderTakerRepo<Product> orderTaker,IMapper mapper)
        {
            _orderTaker = orderTaker;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetAllProducts()
        {
            var spec = new ProductWithBrandAndType();
            var result =    await _orderTaker.GetAllAsync(spec);
            var mappedResult = _mapper.Map<IEnumerable<Product> , IEnumerable<ProductDTO>>(result);
            return Ok(mappedResult);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetById(int id)
        {
            var spec = new ProductWithBrandAndType(id);
            var Result = await _orderTaker.GetAsync(spec);
            var mappedResult = _mapper.Map<Product, ProductDTO>(Result);
            return Ok(Result);
        }
    }
}
