using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyToolkit;
using System;

namespace MSTestSample
{
    [TestClass]
    public class BMICalculatorTest
    {
        /// <summary>
        /// One Time SetUp
        /// </summary>
        [ClassInitialize]
        public static void OneTimeSetUp(TestContext testContext)
        {
            Console.WriteLine("One Time SetUp");
        }

        /// <summary>
        /// SetUp
        /// </summary>
        [TestInitialize]
        public void SetUp()
        {
            Console.WriteLine("SetUp");
        }

        /// <summary>
        /// Tear Down
        /// </summary>
        [TestCleanup]
        public void TearDown()
        {
            Console.WriteLine("TearDown");
        }

        /// <summary>
        /// One Time TearDown
        /// </summary>
        [ClassCleanup]
        public static void OneTimeTearDown()
        {
            Console.WriteLine("One Time TearDown");
        }


        #region Test Case

        [TestMethod]
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
            Assert.AreEqual(exception, result);
        }

        [TestMethod]
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
            Assert.AreEqual(exception, result);
        }

        #endregion
    }
}
