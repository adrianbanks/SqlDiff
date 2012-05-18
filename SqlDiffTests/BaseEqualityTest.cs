using NUnit.Framework;

namespace AdrianBanks.SqlDiffTests
{
    public abstract class BaseEqualityTest<T>
    {
        protected abstract T GetInstance();
        protected abstract T GetDifferentInstance();

        [Test]
        public void TestThat_Null_IsNotEqualToAnInstance()
        {
            Assert.IsFalse(Equals(null, GetInstance()));
        }

        [Test]
        public void TestThat_AnInstance_IsNotEqualToNull()
        {
            Assert.IsFalse(GetInstance().Equals(null));
        }

        [Test]
        public void TestThat_AnInstance_IsEqualToItself()
        {
            T instance = GetInstance();
            Assert.IsTrue(instance.Equals(instance));
        }

        [Test]
        public void TestThat_Instance1_IsEqualToInstance2_WhenTheyAreEqual()
        {
            T instance1 = GetInstance();
            T instance2 = GetInstance();
            Assert.IsTrue(instance1.Equals(instance2));
        }

        [Test]
        public void TestThat_Instance2_IsEqualToInstance1_WhenTheyAreEqual()
        {
            T instance1 = GetInstance();
            T instance2 = GetInstance();
            Assert.IsTrue(instance2.Equals(instance1));
        }

        [Test]
        public void TestThat_Instance1_IsNotEqualToInstance2_WhenTheyAreNotEqual()
        {
            T instance1 = GetInstance();
            T instance2 = GetDifferentInstance();
            Assert.IsFalse(instance1.Equals(instance2));
        }

        [Test]
        public void TestThat_Instance2_IsNotEqualToInstance1_WhenTheyAreNotEqual()
        {
            T instance1 = GetInstance();
            T instance2 = GetDifferentInstance();
            Assert.IsFalse(instance2.Equals(instance1));
        }

        [Test]
        public void TestThat_Instance1AndInstance2_HaveTheSameHashCode_WhenTheyAreTheSameInstance()
        {
            T instance1 = GetInstance();
            Assert.AreEqual(instance1.GetHashCode(), instance1.GetHashCode());
        }

        [Test]
        public void TestThat_Instance1AndInstance2_HaveTheSameHashCode_WhenTheyAreEqual()
        {
            T instance1 = GetInstance();
            T instance2 = GetInstance();
            Assert.AreEqual(instance1.GetHashCode(), instance2.GetHashCode());
        }
    }
}
