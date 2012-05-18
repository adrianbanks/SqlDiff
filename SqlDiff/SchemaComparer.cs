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
            var rightTables = right.Tables.ToDictionary(t => t.Name, t => t);

            foreach (var leftTable in left.Tables)
            {
                Table rightTable;

                if (rightTables.TryGetValue(leftTable.Name, out rightTable))
                {
                    // the table exists in both databases
                    rightTables.Remove(leftTable.Name);

                    // are the tables the same (this takes into account the columns as well)?
                    var comparisonType = leftTable.Equals(rightTable) ? ComparisonType.Equality : ComparisonType.Difference;
                    comparison.Add(new ComparisonObject(comparisonType, ObjectType.Table, leftTable.Name));
                }
                else
                {
                    // the table is missing from the right
                    var missing = new ComparisonObject(ComparisonType.Missing, ObjectType.Table, leftTable.Name);
                    comparison.Add(missing);
                }
            }

            // tables that are new in the right
            comparison.AddRange(rightTables.Values.Select(rightTable => new ComparisonObject(ComparisonType.Addition, ObjectType.Table, rightTable.Name)));
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
