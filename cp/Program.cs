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

            try
            {
                if (args.Length == 0)
                {
                    Calclate(Console.OpenStandardInput(), Console.OpenStandardOutput());
                }
                else if (args[0] == "/?")
                {
                    Calclate(new MemoryStream(Encoding.Unicode.GetBytes(usage)), Console.OpenStandardOutput());
                }
                else if (args[0] == "/i")
                {
                    if (args.Length == 1)
                    {
                        throw new MyAppException("読み込むファイルが指定されませんでした。");
                    }
                    else if (args.Length == 2)
                    {
                        using (var fs = new FileStream(args[1], FileMode.Open))
                        {
                            Calclate(fs, Console.OpenStandardOutput());
                        }
                    }
                    else if (args[2] == "/o")
                    {
                        if (args.Length == 3)
                        {
                            throw new MyAppException("書き込むファイルが指定されませんでした。");
                        }
                        else if (args.Length == 4)
                        {
                            using (var fsOpen = new FileStream(args[1], FileMode.Open))
                            using (var fsClose = new FileStream(args[3], FileMode.Create))
                            {
                                Calclate(fsOpen, fsClose);
                            }
                        }
                        else
                        {
                            throw new MyAppException("引数が正しくありません。");
                        }
                    }
                    else
                    {
                        throw new MyAppException("引数が正しくありません。");
                    }
                }
                else
                {
                    var ms = new MemoryStream(Encoding.Unicode.GetBytes(args[0]));
                    Calclate(ms, Console.OpenStandardOutput());
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