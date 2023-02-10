using NUnit.Framework;
using System.Collections;

namespace Sample.NUnit.DataDriven
{
    public class TestData
    {
        public static IEnumerable TestCases
        {
            get
            {
                yield return new TestCaseData(12, 3).SetName("Test A").Returns(4);
                yield return new TestCaseData(12, 2).SetName("Test B").Returns(6);
                yield return new TestCaseData(12, 4).SetName("Test C").Returns(3);
                yield return new TestCaseData(12, 4).SetName("Test D").Returns(3);
            }
        }
    }
}
