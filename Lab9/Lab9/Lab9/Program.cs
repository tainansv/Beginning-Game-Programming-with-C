using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab9
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("*********\nMenu:");
            Console.WriteLine("N - new game\nL - load options\nO - options");
            Console.WriteLine("Q - quit\n*********\n");
            Console.WriteLine("Your choice? ");
            char option = char.Parse(Console.ReadLine());
            char opt = char.ToLower(option);

            if (opt == char.Parse("n"))
            {
                Console.WriteLine("Starting new game");
            }
            else if(opt == char.Parse("l"))
            {
                Console.WriteLine("Loading options");
            }
            else if (opt == char.Parse("o"))
            {
                Console.WriteLine("Opening options");
            }
            else if(opt == char.Parse("q"))
            {
                Console.WriteLine("Bye");
            }
            else
            {
                Console.WriteLine("Wrong input");
            }

            switch (opt)
            {
                case 'n':
                    Console.WriteLine("Starting new game");
                    break;
                case 'l':
                    Console.WriteLine("Loading options");
                    break;
                case 'o':
                    Console.WriteLine("Opening options");
                    break;
                case 'q':
                    Console.WriteLine("Bye");
                    break;
            }
        }
    }
}
