using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab8
{
    class Program
    {
        static void Main(string[] args)
        {
            
            string myString;

            Console.WriteLine("Enter with the following:");
            Console.WriteLine("<pyramid slot number>,<block letter>,<whether or not the block should be lit>\n");
            Console.WriteLine("For example: 5,M,true\n");
            myString = Console.ReadLine();

            int firstComma = myString.IndexOf(",");
            int seconComma = myString.IndexOf(",",firstComma+1);

            int slot = int.Parse(myString.Substring(0, firstComma));
            char block = char.Parse(myString.Substring(firstComma+1,seconComma-2));
            bool lit = Boolean.Parse(myString.Substring(seconComma+1));
            

            Console.WriteLine("Slot number: {0}", slot);
            Console.WriteLine("Block letter: {0}", block);
            Console.WriteLine("Block shoul be lit: {0}", lit);
        }
    }
}
