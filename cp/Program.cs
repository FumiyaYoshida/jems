using System;
using System.IO;
namespace mycalc
{
    class Program
    {
        static void Main(string[] args)
        {
            Stream inputStream;
            if (args.Length == 2 && args[0] == "/i")
            {
                inputStream = new FileStream(args[1], FileMode.Open);
            }
            else
            {
                inputStream = Console.OpenStandardInput();
            }
            using (var reader = new StreamReader(inputStream))
            {
                int readChar;
                while ((readChar = reader.Read()) != -1)
                {
                    char c = (char)readChar;

                    Console.Write(c);
                }
            }
        }
    }
}