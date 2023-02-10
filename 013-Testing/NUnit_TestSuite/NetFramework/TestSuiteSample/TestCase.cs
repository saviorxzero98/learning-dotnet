using NUnit.Framework;

namespace Sample.NUnit.TestSuite
{
    [TestFixture]
    public class TestCase
    {
        public int Actual;
        public int Expect;

        public TestCase()
        {
            Actual = 0;
            Expect = 0;
        }


        public TestCase(int actual, int expect)
        {
            Actual = actual;
            Expect = expect;
        }

        [Test]
        public void TestMethod()
        {
            Assert.AreEqual(Expect, Actual);
        }
    }
}
