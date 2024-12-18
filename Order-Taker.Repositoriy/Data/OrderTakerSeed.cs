using Order_Taker.Core.Models;
using Order_Taker.Core.Models.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace Order_Taker.Repositoriy.Data
{
    public static class OrderTakerSeed
    {
        public static async Task DataSeed(OrderTakerDBContext dBContext)
        {
            if (!dBContext.Set<ProductBrand>().Any())
            {
                var BrandData = File.ReadAllText("../../Order-Taker/Order-Taker.Repositoriy/Data/DataSeed/brands.json");
                var Brands = JsonSerializer.Deserialize<List<ProductBrand>>(BrandData);

                if (Brands?.Count > 0)
                {
                    foreach (var Brand in Brands)
                    {
                        dBContext.Set<ProductBrand>().Add(Brand);
                    }
                    await dBContext.SaveChangesAsync();
                }
            }
            if (!dBContext.Set<ProductType>().Any())
            {
                var TypeData = File.ReadAllText("../../Order-Taker/Order-Taker.Repositoriy/Data/DataSeed/categories.json");
                var Types = JsonSerializer.Deserialize<List<ProductType>>(TypeData);

                if (Types?.Count > 0)
                {
                    foreach (var Type in Types)
                    {
                        dBContext.Set<ProductType>().Add(Type);
                    }
                    await dBContext.SaveChangesAsync();
                }
            }
            if (!dBContext.Set<Product>().Any())
            {
                var ProductsData = File.ReadAllText("../../Order-Taker/Order-Taker.Repositoriy/Data/DataSeed/products.json");
                var Products = JsonSerializer.Deserialize<List<Product>>(ProductsData);

                if (Products?.Count > 0)
                {
                    foreach (var Product in Products)
                    {
                        dBContext.Set<Product>().Add(Product);
                    }
                    await dBContext.SaveChangesAsync();
                }
            }
            if (!dBContext.Set<DeliveryMethod>().Any())
            {
                var DeliveryData = File.ReadAllText("../../Order-Taker/Order-Taker.Repositoriy/Data/DataSeed/delivery.json");
                var Methods = JsonSerializer.Deserialize<List<DeliveryMethod>>(DeliveryData);

                if (Methods?.Count > 0)
                {
                    foreach (var Method in Methods)
                    {
                        dBContext.Set<DeliveryMethod>().Add(Method);
                    }
                    await dBContext.SaveChangesAsync();
                }
            }

        }
    }
}
