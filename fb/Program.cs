using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kadai
{
    class Program
    {
        static void Main(string[] args)
        {
            string s = "";
            string newS = "";
            for(int i = 1; i <= 100; i++)
            {
                if(i % (3 * 5) == 0)
                {
                    newS = "FizzBuzz";
                }
                else if(i % 5 == 0)
                {
                    newS = "Buzz";
                }
                else if(i % 3 == 0)
                {
                    newS = "Fizz";
                }
                else
                {
                    newS = i.ToString();
                }
                s = s + newS + "\n";
            }
            Console.WriteLine(s.Trim());
            Console.ReadLine();
        }
    }
}
