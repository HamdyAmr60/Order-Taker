using Microsoft.EntityFrameworkCore;
using Order_Taker.Core.Models;
using Order_Taker.Core.Reposatories;
using Order_Taker.Core.Specifications;
using Order_Taker.Repositoriy.Data;
using Order_Taker.Repositoriy.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order_Taker.Repositoriy.Reposatories
{
    public class OrderTakerRepo<T> : IOrderTakerRepo<T> where T : BaseModel
    {
        private readonly OrderTakerDBContext _dBContext;

        public OrderTakerRepo(OrderTakerDBContext dBContext)
        {
            _dBContext = dBContext;
        }
        public Task<int> AddAsync(T item)
        {
            throw new NotImplementedException();
        }

        public Task<int> DeleteAsync(T item)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<T>> GetAllAsync(ISpecification<T> specification)
        {
            return await SpecificationEvaluator<T>.BuildQuery(_dBContext.Set<T>(), specification).ToListAsync();
        }

        public async Task<T> GetAsync(ISpecification<T> specification)
        {
            return await SpecificationEvaluator<T>.BuildQuery(_dBContext.Set<T>(), specification).FirstOrDefaultAsync();
        }

        public Task<int> UpdateAsync(T item)
        {
            throw new NotImplementedException();
        }
    }
}
