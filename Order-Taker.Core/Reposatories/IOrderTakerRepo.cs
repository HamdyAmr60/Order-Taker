﻿using Order_Taker.Core.Models;
using Order_Taker.Core.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order_Taker.Core.Reposatories
{
    public interface IOrderTakerRepo<T> where T : BaseModel
    {
        Task<IReadOnlyList<T>> GetAllAsync(ISpecification<T> specification);
        Task<T> GetAsync(ISpecification<T> specification);
        Task<IReadOnlyList<T>> GetAll();
        Task<T> Get(int id);

        Task<int> AddAsync(T item);
        Task<int> UpdateAsync(T item);
        Task<int> DeleteAsync(T item);

        Task<int> GetCountWithSpecs(ISpecification<T> specification);
    }
}
