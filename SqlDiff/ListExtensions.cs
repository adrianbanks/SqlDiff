using System.Linq;
using System.Collections.Generic;

namespace AdrianBanks.SqlDiff
{
    internal static class ListExtensions
    {
        internal static bool AreEquivalent<T>(this ICollection<T> x, ICollection<T> y)
        {
            return AreEquivalent(x, y, EqualityComparer<T>.Default);
        }

        internal static bool AreEquivalent<T>(this ICollection<T> x, ICollection<T> y, IEqualityComparer<T> equalityComparer)
        {
            if (x.Count != y.Count)
            {
                return false;
            }

            var ySet = new HashSet<T>(y, equalityComparer);
            var equivalent = x.All(ySet.Remove);
            return equivalent;
        }
    }
}
