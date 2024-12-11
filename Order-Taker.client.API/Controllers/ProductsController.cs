using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Order_Taker.client.API.DTOs;
using Order_Taker.client.API.Helpers;
using Order_Taker.Core.Models;
using Order_Taker.Core.Reposatories;
using Order_Taker.Core.Specifications.ProductSpec;
using static System.Runtime.InteropServices.JavaScript.JSType;

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
        public async Task<ActionResult<IReadOnlyList<Pagination<ProductDTO>>>> GetAllProducts([FromQuery]ProductSpecsParams param)
        {
            var spec = new ProductWithBrandAndType(param);
            var result =    await _orderTaker.GetAllAsync(spec);
            var countspecs = new ProductCountSpecs(param);
            var Count = await _orderTaker.GetCountWithSpecs(countspecs);
            var mappedResult = _mapper.Map<IReadOnlyList<Product> , IReadOnlyList<ProductDTO>>(result);
            return Ok(new Pagination<ProductDTO>(param.PageIndex,
                param.PageSize, mappedResult , Count));
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
