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
        public async Task AddAsync(T item)
        {
          await  _dBContext.Set<T>().AddAsync(item);
        }

        public  void Delete(T item)
        {
             _dBContext.Set<T>().Remove(item);
        }

        public async Task<T> Get(int id)
        {
            return await _dBContext.Set<T>().FindAsync(id);
        }

        public async Task<IReadOnlyList<T>> GetAll()
        {
            return await _dBContext.Set<T>().ToListAsync();
        }

        public async Task<IReadOnlyList<T>> GetAllAsync(ISpecification<T> specification)
        {
            return await SpecificationEvaluator<T>.BuildQuery(_dBContext.Set<T>(), specification).ToListAsync();
        }

        public async Task<T> GetAsync(ISpecification<T> specification)
        {
            return await SpecificationEvaluator<T>.BuildQuery(_dBContext.Set<T>(), specification).FirstOrDefaultAsync();
        }

        public async Task<int> GetCountWithSpecs(ISpecification<T> specification)
        {
           return await SpecificationEvaluator<T>.BuildQuery(_dBContext.Set<T>() , specification).CountAsync();
        }

        public void Update(T item)
        {
            _dBContext.Set<T>().Update(item);
        }
    }
}
