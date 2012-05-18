using AdrianBanks.SqlDiff;
using NUnit.Framework;

namespace AdrianBanks.SqlDiffTests
{
    [TestFixture]
    public class ListExtensionsTest
    {
        [Test]
        public void TestThat_TwoListsAreEquivalent_WhenTheyContainTheSameItems_InTheSameOrder()
        {
            int[] x = new[] {1, 2, 3};
            int[] y = new[] {1, 2, 3};
            Assert.IsTrue(x.AreEquivalent(y));
        }

        [Test]
        public void TestThat_TwoListsAreEquivalent_WhenTheyContainTheSameItems_InDifferentOrders()
        {
            int[] x = new[] {1, 2, 3};
            int[] y = new[] {3, 2, 1};
            Assert.IsTrue(x.AreEquivalent(y));
        }

        [Test]
        public void TestThat_TwoListsAreNotEquivalent_WhenTheyContainTheSameNumberOfItems_ButTheItemsAreDifferent()
        {
            int[] x = new[] {1, 2, 3};
            int[] y = new[] {1, 2, 4};
            Assert.IsFalse(x.AreEquivalent(y));
        }

        [Test]
        public void TestThat_TwoListsAreNotEquivalent_WhenTheyContainADifferentNumberOfItems()
        {
            int[] x = new[] {1, 2, 3};
            int[] y = new[] {4, 5};
            Assert.IsFalse(x.AreEquivalent(y));
        }

        [Test]
        public void TestThat_TwoListsAreNotEquivalent_WhenTheFirstIsASubsetOfTheSecond()
        {
            int[] x = new[] {1, 2};
            int[] y = new[] {1, 2, 3};
            Assert.IsFalse(x.AreEquivalent(y));
        }

        [Test]
        public void TestThat_TwoListsAreNotEquivalent_WhenTheSecondIsASubsetOfTheFirst()
        {
            int[] x = new[] {1, 2, 3};
            int[] y = new[] {1, 2};
            Assert.IsFalse(x.AreEquivalent(y));
        }
    }
}