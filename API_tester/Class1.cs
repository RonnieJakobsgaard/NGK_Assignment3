using System;
using NUnit.Framework;

namespace API_tester
{
    [TestFixture]
    public class Class1
    {
        [SetUp]
        public void SetUp()
        {

        }

        [Test]
        public void Test1()
        {
            Assert.AreEqual(1, 1);
        }
    }
}
