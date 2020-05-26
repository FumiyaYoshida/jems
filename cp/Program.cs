using System;
using System.IO;
namespace mycalc
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                if (args.Length == 2 && args[0] == "/i")
                {
                    try
                    {
                        using (var fs = new FileStream(args[1], FileMode.Open))
                        {
                            Calclate(fs);
                        }
                    }
                    catch (FileNotFoundException ex)
                    {
                        throw new MyAppException("ファイルが見つかりませんでした。", ex);
                    }
                }
                else
                {
                    Calclate(Console.OpenStandardInput());
                }
            }
            catch (MyAppException ex)
            {
                Console.Error.WriteLine(ex.Message + "プログラムを終了します。");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine("異常が発生しました。プログラムを終了します。\r\n" + ex.Message);
            }
        }
        static void Calclate(Stream inputStream)
        {
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
