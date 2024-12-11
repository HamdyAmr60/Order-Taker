using Microsoft.EntityFrameworkCore;
using Order_Taker.Core.Models;
using Order_Taker.Core.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order_Taker.Repositoriy.Helper
{
    public static class SpecificationEvaluator<T> where T : BaseModel
    {
        public static IQueryable<T> BuildQuery(IQueryable<T> inputQuery,ISpecification<T> specs)
        {

            var Query = inputQuery;

            if (specs.Criteria is not null)
                 Query = inputQuery.Where(specs.Criteria);
            if(specs.OrderBy is not null)
              Query =    Query.OrderBy(specs.OrderBy);
            if(specs.OrderByDesc is not null)
             Query =    Query.OrderByDescending(specs.OrderByDesc);

            if(specs.ApplyPagination)
                Query = Query.Skip(specs.Skip).Take(specs.Take);
            Query = specs.Includes.Aggregate(Query, (QurrentQuery, IncludeExp) => QurrentQuery.Include(IncludeExp));
            return Query;
        }
    }
}
