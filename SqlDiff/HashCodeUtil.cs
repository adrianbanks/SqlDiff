using System.Linq;
using System.Collections.Generic;

namespace AdrianBanks.SqlDiff
{
    internal static class HashCodeUtil
    {
        internal static int GetHashCode(params object[] args)
        {
            return GetHashCode(17, args);
        }

        internal static int GetHashCode(int baseHashCode, params object[] args)
        {
            unchecked
            {
                return args.Select(GetHashCodeImpl).Aggregate(baseHashCode, (current, hc) => current * 23 + hc);
            }
        }

        private static int GetHashCodeImpl(object arg)
        {
            var enumerable = arg as IEnumerable<object>;
            
            if (enumerable != null)
            {
                var items = (enumerable).ToArray();
                return GetHashCode(items);
            }

            return arg == null ? 29 : arg.GetHashCode();
        }
    }
}
