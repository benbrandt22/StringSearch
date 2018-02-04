using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StringSearch.Shared
{
    public static class LinqExtensions
    {
        public static IEnumerable<IGrouping<int, T>> GroupByEvery<T>(this IEnumerable<T> source, int itemsPerGroup)
        {
            return source
                .Select((item, index) => new { item, index })
                .GroupBy(g => g.index / itemsPerGroup, i => i.item);
        }
    }
}
