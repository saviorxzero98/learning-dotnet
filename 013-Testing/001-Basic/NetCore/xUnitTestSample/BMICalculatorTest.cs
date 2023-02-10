using MyToolkit;
using System;
using Xunit;

namespace xUnitTestSample
{
    public class BMICalculatorTest : IClassFixture<TestsFixture>, IDisposable
    {
        /// <summary>
        /// SetUp
        /// </summary>
        public BMICalculatorTest(TestsFixture data)
        {
            Console.WriteLine("SetUp");
        }

        /// <summary>
        /// Tear Down
        /// </summary>
        public void Dispose()
        {
            Console.WriteLine("TearDown");
        }


        #region Test Case

        [Fact]
        public void TestCalculateA()
        {
            // Arrange
            double height = 1.68;
            double weight = 60;
            var bmiCalcualtor = new BMICalculator(height, weight);

            // Act
            double result = bmiCalcualtor.Calculate();

            // Assert 
            double exception = weight / (height * height);
            Assert.Equal(exception, result);
        }

        [Fact]
        public void TestCalculateB()
        {
            // Arrange
            double height = 1.68;
            double weight = 55;
            var bmiCalcualtor = new BMICalculator(height, weight);

            // Act
            double result = bmiCalcualtor.Calculate();

            // Assert 
            double exception = weight / (height * height);
            Assert.Equal(exception, result);
        }

        #endregion
    }


    public class TestsFixture : IDisposable
    {
        /// <summary>
        /// One Time SetUp
        /// </summary>
        public TestsFixture()
        {
            Console.WriteLine("One Time SetUp");
        }

        /// <summary>
        /// One Time TearDown
        /// </summary>
        public void Dispose()
        {
            Console.WriteLine("One Time TearDown");
        }
    }
}
