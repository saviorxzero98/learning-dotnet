using System;

namespace MyToolkit
{
    public class BMICalculator : ICalculator
    {
        public double Height { get; protected set; }
        public double Weight { get; protected set; }

        public BMICalculator(double height, double weight)
        {
            Height = height;
            Weight = weight;
        }

        public double Calculate()
        {
            if (Weight <= 0)
            {
                throw new Exception("體重不能小於或等於 0");
            }

            if (Height <= 0)
            {
                throw new Exception("身高不能小於或等於 0");
            }

            double bmi = Weight / (Height * Height);
            return bmi;
        }
    }
}
