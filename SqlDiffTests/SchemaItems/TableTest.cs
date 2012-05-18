using NUnit.Framework;
using AdrianBanks.SqlDiff.SchemaItems;

namespace AdrianBanks.SqlDiffTests.SchemaItems
{
    [TestFixture]
    public class TableTest : BaseEqualityTest<Table>
    {
        protected override Table GetInstance()
        {
            return new Table("Table1");
        }

        protected override Table GetDifferentInstance()
        {
            return new Table("Table2");
        }
    }
}