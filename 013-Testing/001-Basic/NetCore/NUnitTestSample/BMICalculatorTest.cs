using MyToolkit;
using NUnit.Framework;
using System;

namespace NUnitTestSample
{
    public class Tests
    {
        /// <summary>
        /// One Time SetUp
        /// </summary>
        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Console.WriteLine("One Time SetUp");
        }

        /// <summary>
        /// SetUp
        /// </summary>
        [SetUp]
        public void SetUp()
        {
            Console.WriteLine("SetUp");
        }

        /// <summary>
        /// Tear Down
        /// </summary>
        [TearDown]
        public void TearDown()
        {
            Console.WriteLine("TearDown");
        }

        /// <summary>
        /// One Time TearDown
        /// </summary>
        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            Console.WriteLine("One Time TearDown");
        }


        #region Test Case

        [Test]
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

        [Test]
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