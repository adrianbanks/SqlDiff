using NUnit.Framework;
using AdrianBanks.SqlDiff.SchemaItems;

namespace AdrianBanks.SqlDiffTests.SchemaItems
{
    [TestFixture]
    public class ViewTest : BaseEqualityTest<View>
    {
        protected override View GetInstance()
        {
            return new View("View1", "Definition1");
        }

        protected override View GetDifferentInstance()
        {
            return new View("View2", "Definition2");
        }
    }
}