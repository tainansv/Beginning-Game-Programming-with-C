using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab14
{
    class Program
    {
        static void Main(string[] args)
        {
            //variables
            int number = 0;
            int max = 0;
            int total = 0;
            int count = 0;
            List<int> numbersList = new List<int>();

            //loop until user enters -1
            while (number != -1)
            {
                Console.Write("Enter a number: ");
                number = int.Parse(Console.ReadLine());

                //validate the number and add to list
                while(number >= 0)
                {
                    numbersList.Add(number);
                    count++;
                    break;
                }
            }

            foreach(int n in numbersList)
            {
                if (n > max)
                {
                    max = n;
                }
            }

            foreach(int n in numbersList)
            {
                total += n;
            }

            Console.WriteLine("Maximum value: {0}", max);
            Console.WriteLine("Average: {0}", total / count);
        }

    }
}
