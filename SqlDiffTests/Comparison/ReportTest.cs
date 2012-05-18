using NUnit.Framework;
using AdrianBanks.SqlDiff.Comparison;

namespace AdrianBanks.SqlDiffTests.Comparison
{
    [TestFixture]
    public sealed class ReportTest
    {
        [Test]
        public void TestThat_TheConstructorSeparatesTheDifferentComparisonTypesCorrectly()
        {
            var object1 = new ComparisonObject(ComparisonType.Addition, ObjectType.Column, "column1");
            var object2 = new ComparisonObject(ComparisonType.Difference, ObjectType.Column, "column2");
            var object3 = new ComparisonObject(ComparisonType.Equality, ObjectType.Column, "column3");
            var object4 = new ComparisonObject(ComparisonType.Missing, ObjectType.Column, "column4");
            var report = new Report(new[] {object1, object2, object3, object4});

            CollectionAssert.AreEquivalent(new[] {object1}, report.Additions);
            CollectionAssert.AreEquivalent(new[] {object2}, report.Differences);
            CollectionAssert.AreEquivalent(new[] {object3}, report.Equalities);
            CollectionAssert.AreEquivalent(new[] {object4}, report.Missings);
        }
    }
}
