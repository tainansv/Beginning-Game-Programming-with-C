using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab3
{
    class Program
    {
        static void Main(string[] args)
        {
            float fahrenheit;
            float celsius;
            float fahrenheitAgain;

            Console.Write("Enter temperature in fahrenheit: ");
            fahrenheit = float.Parse(Console.ReadLine());

            celsius = ((fahrenheit - 32) / 9) * 5;
            fahrenheitAgain = ((celsius * 9) / 5) + 32;

            Console.WriteLine("{0} degrees Fahrenheit is {1} degrees Celsius", fahrenheit, celsius);
            Console.WriteLine("{0} degrees Celsius is {1} degrees Fahrenheit", celsius, fahrenheitAgain);
        }
    }
}