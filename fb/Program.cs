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
            StringBuilder sb = new StringBuilder("");
            string s5 = "Buzz";
            string s3 = "Fizz";
            string s15 = s3 + s5;

            for (int i = 1; i <= 100; i++)
            {
                if(i % (3 * 5) == 0)
                {
                    sb.Append(s15);
                }
                else if(i % 5 == 0)
                {
                    sb.Append(s5);
                }
                else if(i % 3 == 0)
                {
                    sb.Append(s3);
                }
                else
                {
                    sb.Append(i.ToString());
                }
                sb.Append("\n");
            }
            Console.WriteLine(sb);
            Console.ReadKey();
        }
    }
}
