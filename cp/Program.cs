using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace mycalc
{
    class Program
    {
        static void Main(string[] args)
        {
            string usage = @"計算機プログラムを実行します。
>mycalc /?⏎ → 標準エラー出力に使い方を表示
>mycalc /i ファイル名1⏎ → ファイル名1から入力し計算結果を標準出力に出力
>mycalc /i ファイル名1 /o ファイル名2⏎ → ファイル1から入力し計算結果をファイル2に出力
>mycalc 1+2+3+4+5⏎ → 引数を1行のみの入力とし計算結果を標準出力に出力";
            string outputContent;

            try
            {
                switch (args.Length)
                {
                    case 0:
                        Calclate(Console.OpenStandardInput());
                        break;
                    case 1:
                        if (args[0] == "/?")
                        {
                            Console.WriteLine(usage);
                        }
                        else
                        {
                            var ms = new MemoryStream(Encoding.Unicode.GetBytes(args[0]));
                            outputContent = Calclate2(ms);
                            outputContent = outputContent.Replace(" ", "");
                            Console.Write(outputContent);
                        }
                        break;
                    case 2:
                        if (args[0] == "/i")
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
                            throw new FormatException();
                        }
                        break;
                    case 4:
                        if (args[0] == "/i" && args[2] == "/o")
                        {
                            try
                            {
                                using (var fs = new FileStream(args[1], FileMode.Open))
                                {
                                    outputContent = Calclate2(fs);
                                }
                            }
                            catch (FileNotFoundException ex)
                            {
                                throw new MyAppException("ファイルが見つかりませんでした。", ex);
                            }

                            using (var fs = new FileStream(args[3], FileMode.Create))
                            {
                                OutputFile(outputContent, fs);
                            }
                        }
                        else
                        {
                            throw new FormatException();
                        }
                        break;
                    default:
                        throw new FormatException();

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

        static void Calclate(Stream inputStream, Stream outputStream)
        {
            var encode = Encoding.GetEncoding("shift_jis");
            using (var reader = new StreamReader(inputStream, encode))
            using (var writer = new StreamWriter(outputStream, encode))
            {
                int readChar;
                while ((readChar = reader.Read()) != -1)
                {
                    char c = (char)readChar;

                    writer.Write(c);
                }
            }
        }
    }
}