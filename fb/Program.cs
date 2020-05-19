using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kadai
{
    class Program
    {
        static void Main()
        {
            var sb = new StringBuilder();

            for (int i = 1; i <= 100; i++)
            {
                string line = "";

                if (i % 3 == 0)
                {
                    line += "Fizz";
                }
                if (i % 5 == 0)
                {
                    line += "Buzz";
                }
                if (string.IsNullOrEmpty(line))
                {
                    line = i.ToString();
                }
                sb.AppendLine(line);
            }
            Console.WriteLine(sb);
            Console.ReadKey();
        }
    }
}
