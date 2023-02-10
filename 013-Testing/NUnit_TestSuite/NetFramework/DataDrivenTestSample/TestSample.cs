using NUnit.Framework;

namespace Sample.NUnit.DataDriven
{
    [TestFixture]
    public class TestSample
    {
        [Test, TestCaseSource(typeof(TestData), "TestCases")]
        public int TestMethod(int n, int d)
        {
            return n / d;
        }
    }
}