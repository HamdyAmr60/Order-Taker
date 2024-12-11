using Order_Taker.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Order_Taker.Core.Specifications
{
    public interface ISpecification<T> where T :BaseModel
    {
        public Expression<Func<T,bool>> Criteria { get; set; }
        public List<Expression<Func<T, object>>> Includes { get; set; }

        public Expression<Func<T,object>> OrderBy { get; set; }

        public Expression<Func<T,object>> OrderByDesc { get; set; }


        public int Take { get; set; }

        public int Skip { get; set; }

        public bool ApplyPagination { get; set; }
    }
}
