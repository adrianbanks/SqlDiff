using System.Linq;
using System.Collections.Generic;
using AdrianBanks.SqlDiff.Comparison;
using AdrianBanks.SqlDiff.SchemaItems;

namespace AdrianBanks.SqlDiff
{
    public sealed class SchemaComparer
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
            var rightViews = right.Views.ToDictionary(v => v.Name, v => v);

            foreach (var leftView in left.Views)
            {
                View rightView;

                if (rightViews.TryGetValue(leftView.Name, out rightView))
                {
                    // the view exists in both databases
                    rightViews.Remove(leftView.Name);

                    // are the views the same?
                    var comparisonType = leftView.Equals(rightView) ? ComparisonType.Equality : ComparisonType.Difference;
                    comparison.Add(new ComparisonObject(comparisonType, ObjectType.View, leftView.Name));
                }
                else
                {
                    // the view is missing from the right
                    var missing = new ComparisonObject(ComparisonType.Missing, ObjectType.View, leftView.Name);
                    comparison.Add(missing);
                }
            }

            // views that are new in the right
            comparison.AddRange(rightViews.Values.Select(rightView => new ComparisonObject(ComparisonType.Addition, ObjectType.View, rightView.Name)));
            return comparison;
        }

        private IEnumerable<ComparisonObject> CompareTriggers()
        {
            var comparison = new List<ComparisonObject>();
            var rightTriggers = right.Triggers.ToDictionary(t => t.Name, t => t);

            foreach (var leftTrigger in left.Triggers)
            {
                Trigger rightTrigger;

                if (rightTriggers.TryGetValue(leftTrigger.Name, out rightTrigger))
                {
                    // the trigger exists in both databases
                    rightTriggers.Remove(leftTrigger.Name);

                    // are the triggers the same?
                    var comparisonType = leftTrigger.Equals(rightTrigger) ? ComparisonType.Equality : ComparisonType.Difference;
                    comparison.Add(new ComparisonObject(comparisonType, ObjectType.Trigger, leftTrigger.Name));
                }
                else
                {
                    // the trigger is missing from the right
                    var missing = new ComparisonObject(ComparisonType.Missing, ObjectType.Trigger, leftTrigger.Name);
                    comparison.Add(missing);
                }
            }

            // views that are new in the right
            comparison.AddRange(rightTriggers.Values.Select(rightTrigger => new ComparisonObject(ComparisonType.Addition, ObjectType.Trigger, rightTrigger.Name)));
            return comparison;
        }
    }
}