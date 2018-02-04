using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StringSearch.Shared
{
    public static class GetRandomExtensions
    {
        private static readonly Random random = new Random();

        public static T GetRandom<T>(this ICollection<T> collection)
        {
            if (collection.Count == 0) { throw new InvalidOperationException("Collection contains no elements"); }
            return collection.ElementAt(random.Next(0, collection.Count));
        }

        public static char GetRandom(this string charSet)
        {
            return charSet.ToList().GetRandom();
        }
    }
}
