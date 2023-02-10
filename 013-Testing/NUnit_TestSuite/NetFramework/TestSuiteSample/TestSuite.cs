using NUnit.Framework;
using System.Collections;

namespace Sample.NUnit.TestSuite
{
    [TestFixture]
    public class TestSuite
    {
        [TestFixtureSetUp]
        public void Setup()
        {
        }

        [TestFixtureTearDown]
        public void Teardown()
        {
        }

        [Suite]
        public static IEnumerable Suite
        {
            get
            {
                ArrayList suite = new ArrayList();

                suite.Add(new TestCase(1, 1));
                suite.Add(new TestCase(2, 2));
                suite.Add(new TestCase(1+1, 1));

                return suite;
            }
        }
    }
}