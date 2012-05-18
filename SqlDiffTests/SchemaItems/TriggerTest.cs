using NUnit.Framework;
using AdrianBanks.SqlDiff.SchemaItems;

namespace AdrianBanks.SqlDiffTests.SchemaItems
{
    [TestFixture]
    public class TriggerTest : BaseEqualityTest<Trigger>
    {
        protected override Trigger GetInstance()
        {
            return new Trigger("Trigger1", "Definition1");
        }

        protected override Trigger GetDifferentInstance()
        {
            return new Trigger("Trigger2", "Definition2");
        }
    }
}