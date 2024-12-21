using Order_Taker.Core;
using Order_Taker.Core.Models;
using Order_Taker.Core.Reposatories;
using Order_Taker.Repositoriy.Data;
using Order_Taker.Repositoriy.Reposatories;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order_Taker.Repositoriy
{
    public class UnitOfWork : IUnitOfWork
    {
        private Hashtable _reposatories  ;
        private readonly OrderTakerDBContext _orderTakerDB;

        public UnitOfWork(OrderTakerDBContext orderTakerDB)
        {
            _reposatories = new Hashtable();
            this._orderTakerDB = orderTakerDB;
        }
        public async ValueTask DisposeAsync()
        {
          await  _orderTakerDB.DisposeAsync();   
        }

        public IOrderTakerRepo<T> repo<T>() where T : BaseModel
        {
            var Type = typeof(T).Name;
            if (!_reposatories.ContainsKey(Type)) 
            {
                var repo = new OrderTakerRepo<T>(_orderTakerDB);
                _reposatories.Add(Type, repo);
            }
            return _reposatories[Type] as IOrderTakerRepo<T>;
        }

        public async Task<int> SaveAsync()
        {
          return await _orderTakerDB.SaveChangesAsync();
        }
    }
}
