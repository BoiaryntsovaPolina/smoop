using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab8Task1_2_
{
    internal class AreaCalculator
    {
        public double CalculateArea(double param1, double param2, Func<double, double, double> areaMethod)
        {
            return areaMethod(param1, param2);
        }

        // Метод для обчислення площі трикутника
        public double TriangleArea(double baseLength, double height)
        {
            return 0.5 * baseLength * height;
        }

        // Метод для обчислення площі прямокутника
        public double RectangleArea(double width, double height)
        {
            return width * height;
        }
    }
}
