using System.Data;
using NUnit.Framework;
using AdrianBanks.SqlDiff.SchemaItems;

namespace AdrianBanks.SqlDiffTests.SchemaItems
{
    [TestFixture]
    public class ColumnTest : BaseEqualityTest<Column>
    {
        protected override Column GetInstance()
        {
            return new Column("columnA", DbType.Int32, true, false, "Collation2", 25, 1, 1);
        }

        protected override Column GetDifferentInstance()
        {
            return new Column("columnB", DbType.Boolean, false, true, "Collation2", 250, 10, 2);
        }
    }
}