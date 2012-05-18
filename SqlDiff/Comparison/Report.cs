using System.Linq;
using System.Collections.Generic;

namespace AdrianBanks.SqlDiff.Comparison
{
    public sealed class Report
    {
        public IList<ComparisonObject> Additions{get {return additions;}}
        private readonly List<ComparisonObject> additions;

        public IList<ComparisonObject> Differences{get {return differences;}}
        private readonly List<ComparisonObject> differences;

        public IList<ComparisonObject> Equalities{get {return equalities;}}
        private readonly List<ComparisonObject> equalities;

        public IList<ComparisonObject> Missings{get {return missings;}}
        private readonly List<ComparisonObject> missings;

        public Report(IEnumerable<ComparisonObject> comparison)
        {
            var comparisonObjects = comparison.ToList();
            additions = comparisonObjects.Where(c => c.ComparisonType == ComparisonType.Addition).ToList();
            differences = comparisonObjects.Where(c => c.ComparisonType == ComparisonType.Difference).ToList();
            equalities = comparisonObjects.Where(c => c.ComparisonType == ComparisonType.Equality).ToList();
            missings = comparisonObjects.Where(c => c.ComparisonType == ComparisonType.Missing).ToList();
        }
    }
}
