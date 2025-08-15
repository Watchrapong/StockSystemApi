
using StockSystem.Api.Utils;

namespace StockSystem.Api.Extensions
{
    public static class IQueryableExtensions
    {
        public static IPagedList<T> ToPagedList<T>(this IQueryable<T> source, int pageIndex, int pageSize, bool getOnlyTotalCount = false)
        {
            if (source == null)
                return new PagedList<T>(new List<T>(), pageIndex, pageSize);

            pageSize = Math.Max(pageSize, 1);

            var count = source.Count();

            var data = new List<T>();

            var _pageIndex = pageIndex;
            if (!getOnlyTotalCount)
            {
                if (count < (pageIndex * pageSize))
                {
                    _pageIndex = 0;
                }
                data.AddRange(source.Skip(_pageIndex * pageSize).Take(pageSize).ToList());
            }

            return new PagedList<T>(data, _pageIndex, pageSize, count);
        }
    }
}