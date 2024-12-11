using Order_Taker.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order_Taker.Core.Specifications.ProductSpec
{
    public class ProductCountSpecs : BaseSpecification<Product>
    {
        public ProductCountSpecs(ProductSpecsParams param) : base(P =>
            (!param.BrandId.HasValue || P.ProductBrandId == param.BrandId)
            &&
            (!param.TypeId.HasValue || P.ProductTypeId == param.TypeId)
            )
        {
            
        }
    }
}
