using System.Linq;
using System.Collections.Generic;
using AdrianBanks.SqlDiff.Comparison;
using AdrianBanks.SqlDiff.SchemaItems;

namespace AdrianBanks.SqlDiff
{
    internal sealed class SchemaComparer
    {
        private readonly Schema left;
        private readonly Schema right;

        public SchemaComparer(Schema left, Schema right)
        {
            this.left = left;
            this.right = right;
        }

        public Report Compare()
        {
            var tables = CompareTables();
            var views = CompareViews();
            var triggers = CompareTriggers();

            var comparison = tables.Concat(views).Concat(triggers);
            return new Report(comparison);
        }

        private IEnumerable<ComparisonObject> CompareTables()
        {
            var comparison = new List<ComparisonObject>();
            return comparison;
        }

        private IEnumerable<ComparisonObject> CompareViews()
        {
            var comparison = new List<ComparisonObject>();
            return comparison;
        }

        private IEnumerable<ComparisonObject> CompareTriggers()
        {
            var comparison = new List<ComparisonObject>();
            return comparison;
        }
    }
}
