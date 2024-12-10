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
        public ProductWithBrandAndType():base()
        {
            Includes.Add(P => P.ProductBrand);
            Includes.Add(P => P.ProductType);
        }

        public ProductWithBrandAndType(int id):base(P=>P.Id == id)
        {
            Includes.Add(P => P.ProductBrand);
            Includes.Add(P => P.ProductType);
        }
    }
}
