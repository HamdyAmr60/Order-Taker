using Order_Taker.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order_Taker.Core.Reposatories
{
    public interface IOrderTakerRepo<T> where T : BaseModel
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetAsync(int id);

        Task<int> AddAsync(T item);
        Task<int> UpdateAsync(T item);
        Task<int> DeleteAsync(T item);
    }
}
