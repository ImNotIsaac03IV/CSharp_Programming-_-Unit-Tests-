/*--------------------------------------------------------------
*				HTBLA-Leonding / Class: 1xHIF
*--------------------------------------------------------------
*              Musterlösung-HA
*--------------------------------------------------------------
* Description: linear Interpolation
*--------------------------------------------------------------
*/

namespace LinearInterpolation
{
    using System;

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Linear Interpolation");
            Console.WriteLine("**********************");

            Console.Write("Please enter the count of x  / y points [1...100]: ");
            int size;
            while (!int.TryParse(Console.ReadLine(), out size) || size < 1 || size > 100)
            {
                Console.Write("Invalid input. Please enter a value between 1...100: ");
            }
            
            double[] xValues = new double[size];
            double[] yValues = new double[size];

            for (int i = 0; i < xValues.Length; i++)
            {
                Console.Write($"X-Values {i + 1}. number: ");
                xValues[i] = ReadUserInput();
            }

            for (int i = 0; i < yValues.Length; i++)
            {
                Console.Write($"Y-Values {i + 1}. number: ");
                yValues[i] = ReadUserInput();
            }

            double x;
            do
            {
                Console.Write("Please enter x to be converted (0 to exit): ");
                x = ReadUserInput();

            }
            while (x != 0);
            
        }
        static double ReadUserInput()
        {
            double value;
            while (!double.TryParse(Console.ReadLine(), out value) || value < double.MinValue || value > double.MaxValue)
            {
                Console.Write("Invalid value, make sure it's a valid number: ");
            }
            return value;
        }
    }
}