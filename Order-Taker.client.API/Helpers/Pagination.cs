using Order_Taker.client.API.DTOs;

namespace Order_Taker.client.API.Helpers
{
    public class Pagination<T>
    {

        public Pagination(int pageIndex, int pageSize, IReadOnlyList<T> mappedResult, int count)
        {
            PageIndex = pageIndex;
            PageSize = pageSize;
            Data = mappedResult;
            Count = count;
        }

        public int PageSize { get; set; }
        public int PageIndex { get; set; }

        public int Count { get; set; }

        public IReadOnlyList<T> Data { get; set; }
    }
}
