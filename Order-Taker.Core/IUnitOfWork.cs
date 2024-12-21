using Order_Taker.Core.Models;
using Order_Taker.Core.Reposatories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order_Taker.Core
{
    public interface IUnitOfWork: IAsyncDisposable
    {
        Task<int> SaveAsync();

        IOrderTakerRepo<T> repo<T>() where T : BaseModel;
    }
}
