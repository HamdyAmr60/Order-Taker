using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Order_Taker.Core;
using Order_Taker.Core.Models;
using Order_Taker.Core.Specifications.ProductSpec;
using Talabat.Admin.DashBoard.Helpers;
using Talabat.Admin.DashBoard.Models;

namespace Talabat.Admin.DashBoard.Controllers
{
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProductController(IUnitOfWork unitOfWork , IMapper mapper)
        {
            this._unitOfWork = unitOfWork;
            this._mapper = mapper;
        }
        public async Task<IActionResult> Index()
        {
            var spec = new ProductWithBrandAndType(new ProductSpecsParams());
            var products =await _unitOfWork.repo<Product>().GetAllAsync(spec);
            var ProductView = _mapper.Map<IReadOnlyList<Product> , IReadOnlyList<ProductViewModel>>(products);
            return View(ProductView);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(ProductViewModel product)
        {
            if (!ModelState.IsValid) 
            {
                return BadRequest();
            }
            try
            {
                product.PictureUrl = ImageSettings.Upload(product.Image, "products");
                var store = _mapper.Map<ProductViewModel, Product>(product);
                await _unitOfWork.repo<Product>().AddAsync(store);
                await _unitOfWork.SaveAsync();
            }
            catch (Exception ex) 
            {
                ModelState.AddModelError("error",ex.Message);
            }
        return RedirectToAction(nameof(Index));
        }
    }
}
