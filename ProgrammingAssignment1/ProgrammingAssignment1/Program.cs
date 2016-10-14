using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammingAssignment1
{
    class Program
    {
        static void Main(string[] args)
        {
            //Welcome message
            Console.WriteLine("Welcome!");
            Console.WriteLine("This application will calculate the distance \r\nbetween two points and the angle between those points.");
            Console.WriteLine("\r\n");

            //Prompt and get the values
            Console.Write("First point, x value: ");
            float point1x = float.Parse(Console.ReadLine());
            Console.Write("First point, y value: ");
            float point1y = float.Parse(Console.ReadLine());
            Console.Write("Second point, x value: ");
            float point2x = float.Parse(Console.ReadLine());
            Console.Write("Second point, y value: ");
            float point2y = float.Parse(Console.ReadLine());

            //Calculate the delta
            double deltax = point2x - point1x;
            double deltay = point2y - point1y;

            //Calculate the distance
            double dt = Math.Sqrt(Math.Pow(deltax, 2) + Math.Pow(deltay, 2));

            //Calculate the angle
            double ang = (Math.Atan2(deltax, deltay) * 180/Math.PI);


            //Print results
            Console.WriteLine("\r\n");
            Console.WriteLine("Distance between the points: {0}", dt.ToString("f3"));
            Console.WriteLine();
            Console.WriteLine("Angle between points: {0} degrees", ang.ToString("f3"));
        }
    }
}