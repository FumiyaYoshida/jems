using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace intCalc
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                string arg = Console.ReadLine();
                string[] arr = arg.Split(' ');

                if (arr.Length != 4)
                {
                    throw new ArgumentException();
                }
                if (arr[0] != "mycalc")
                {
                    throw new ArgumentException();
                }
                if (!Regex.IsMatch(arr[1], "^[0-9]+$"))
                {
                    throw new ArgumentException();
                }
                if (!Regex.IsMatch(arr[3], "^[0-9]+$"))
                {
                    throw new ArgumentException();
                }
                if (arr[2] == "/")
                {
                    Console.WriteLine("{0} / {1} = {2} ({3})", arr[1], arr[3], int.Parse(arr[1]) / int.Parse(arr[3]), int.Parse(arr[1]) % int.Parse(arr[3]));
                }
                else if (arr[2] == "*")
                {
                    Console.WriteLine("{0} * {1} = {2}", arr[1], arr[3], int.Parse(arr[1]) * int.Parse(arr[3]));
                }
                else if (arr[2] == "+")
                {
                    Console.WriteLine("{0} + {1} = {2}", arr[1], arr[3], int.Parse(arr[1]) + int.Parse(arr[3]));
                }
                else if (arr[2] == "-")
                {
                    Console.WriteLine("{0} - {1} = {2}", arr[1], arr[3], int.Parse(arr[1]) - int.Parse(arr[3]));
                }
                else
                {
                    throw new ArgumentException();
                }
            }
            catch (ArgumentException)
            {
                Console.WriteLine("arg error");
            }
        }
    }
}
