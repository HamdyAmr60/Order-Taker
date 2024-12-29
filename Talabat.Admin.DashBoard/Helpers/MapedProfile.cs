using AutoMapper;
using Order_Taker.Core.Models;
using Talabat.Admin.DashBoard.Models;

namespace Talabat.Admin.DashBoard.Helpers
{
    public class MapedProfile :Profile
    {
        public MapedProfile()
        {
           CreateMap<Product,ProductViewModel>().ReverseMap();
        }
    }
}
