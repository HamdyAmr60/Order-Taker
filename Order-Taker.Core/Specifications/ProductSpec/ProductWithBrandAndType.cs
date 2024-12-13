using Order_Taker.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Order_Taker.Core.Specifications.ProductSpec
{
    public class ProductWithBrandAndType :BaseSpecification<Product>
    {
        public ProductWithBrandAndType(ProductSpecsParams param)
            :base(P=>
            (string.IsNullOrEmpty(param.Search)|| P.Name.ToLower().Contains(param.Search))
            &&
            (!param.BrandId.HasValue || P.ProductBrandId == param.BrandId)
            &&
            (!param.TypeId.HasValue || P.ProductTypeId == param.TypeId)
            )
        {
            Includes.Add(P => P.ProductBrand);
            Includes.Add(P => P.ProductType);

            if (!string.IsNullOrEmpty(param.Sort))
            {
                switch (param.Sort)
                {
                    case "PriceAsc":
                        ApplyOrderBy(P => P.Price);
                        break;
                    case "PriceDesc":
                        ApplyOrderByDesc(P => P.Price);
                        break;
                    default:
                        ApplyOrderBy(P => P.Name);
                        break;
                }
            }
          
                ApplyOfPagination(param.PageSize, (param.PageIndex - 1) * param.PageSize);
            
        }

        public ProductWithBrandAndType(int id):base(P=>P.Id == id)
        {
            Includes.Add(P => P.ProductBrand);
            Includes.Add(P => P.ProductType);
        }
    }
}
