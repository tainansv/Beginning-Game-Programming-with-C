using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab7
{
    class Program
    {
        static void Main(string[] args)
        {
            String month;
            String day;
            int dayBefore;

            Console.Write("In what month were you born? ");
            month = Console.ReadLine();
            Console.Write("On what day were you born?" );
            day = Console.ReadLine();

            dayBefore = int.Parse(day) - 1;

            Console.WriteLine("Your birthday is {0} {1}", month, day);
            Console.WriteLine("You’ll receive an email reminder on {0} {1}", month, dayBefore);
        }
    }
}
