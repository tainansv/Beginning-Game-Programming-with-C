using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2
{
    class Program
    {
        static void Main(string[] args)
        {
            int age = 24;
            int maxScore = 100;
            int score = 28;
            float percent = score / maxScore;

            Console.WriteLine("My age: {0}", age);
            Console.WriteLine("Percentage: {0}", percent);
        }
    }
}
