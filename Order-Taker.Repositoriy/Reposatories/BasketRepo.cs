using Order_Taker.Core.Models;
using Order_Taker.Core.Reposatories;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Order_Taker.Repositoriy.Reposatories
{
    public class BasketRepo : IBasketRepo
    {
        private readonly IDatabase _redis;

        public BasketRepo(IConnectionMultiplexer redis)
        {
            _redis = redis.GetDatabase();
        }
        public async Task<bool> DeleteBasketAsync(string id)
        {
            return await _redis.KeyDeleteAsync(id);
        }

        public async Task<CustomerBasket?> GetBasketAsync(string id)
        {
            var Basket =await _redis.StringGetAsync(id);
            if (Basket.IsNull) return null;
            var CustomerBasket = JsonSerializer.Deserialize<CustomerBasket>(Basket);
            return CustomerBasket;
        }

        public async Task<CustomerBasket?> UpdateBasketAsync(CustomerBasket basket)
        {
            var JsonBasket = JsonSerializer.Serialize(basket);
          var CreatedOrUpdated =   await _redis.StringSetAsync(basket.Id , JsonBasket,TimeSpan.FromDays(1));

            if (!CreatedOrUpdated) return null;
            return await GetBasketAsync(basket.Id);   
        }
    }
}
