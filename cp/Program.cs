using System;
using System.Diagnostics;
using System.IO;
using System.Security;
using System.Text;
using System.Text.RegularExpressions;

namespace mycalc
{
    class Program
    {
        static Encoding encode = Encoding.GetEncoding("shift_jis");

        static void Main(string[] args)
        {
            string usage = @"計算機プログラムを実行します。
>mycalc /?⏎ → 標準エラー出力に使い方を表示
>mycalc /i ファイル名1⏎ → ファイル名1から入力し計算結果を標準出力に出力
>mycalc /i ファイル名1 /o ファイル名2⏎ → ファイル1から入力し計算結果をファイル2に出力
>mycalc 1+2+3+4+5⏎ → 引数を1行のみの入力とし計算結果を標準出力に出力";
            string inputFile;
            string outputFile;
            string inputContent;


            try
            {
                (inputFile, outputFile, inputContent) = AnalysisArgs(args, usage);

                Stream inputStream = null;
                Stream outputStream = null;

                try
                {
                    if (!string.IsNullOrEmpty(inputFile))
                    {
                        try
                        {
                            inputStream = new FileStream(inputFile, FileMode.Open);
                        }
                        catch (IOException ex)
                        {
                            throw new MyAppException($"入力ファイル{inputFile}を開けませんでした。{ex.GetType().Name} {ex.Message}");
                        }
                    }
                    else if (!string.IsNullOrEmpty(inputContent))
                    {
                        inputStream = new MemoryStream(encode.GetBytes(inputContent));
                    }

                    if (!string.IsNullOrEmpty(outputFile))
                    {
                        try
                        {
                            outputStream = new FileStream(outputFile, FileMode.Create);
                        }
                        catch (IOException ex)
                        {
                            throw new MyAppException($"出力ファイル{outputFile}を更新できません。{ex.GetType().Name} {ex.Message}");
                        }
                    }

                    Calclate(inputStream ?? Console.OpenStandardInput(),
                                outputStream ?? Console.OpenStandardOutput());
                }
                finally
                {
                    if (inputStream != null)
                    {
                        inputStream.Close();
                    }
                    if (outputStream != null)
                    {
                        outputStream.Close();
                    }
                }
            }
            catch (MyAppException ex)
            {
                Console.Error.WriteLine(ex.Message + "プログラムを終了します。");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine("異常が発生しました。プログラムを終了します。\r\n" + ex.Message + ex.GetType().FullName);
            }
            Console.ReadKey();
        }

        static (string, string, string) AnalysisArgs(string[] args, string usage)
        {
            string inputFile = null;
            string outputFile = null;
            string inputContent = "";

            if (args.Length > 0)
            {
                if (args[0] == "/?")
                {
                    Console.Error.WriteLine(usage);
                    Exit();
                }
                else if (args[0] == "/i")
                {
                    if (args.Length == 2)
                    {
                        inputFile = args[1];
                    }
                    else if (args.Length == 4 && args[2] == "/o")
                    {
                        inputFile = args[1];
                        outputFile = args[3];
                    }
                    else
                    {
                        throw new MyAppException("引数が正しくありません。");
                    }
                }
                else
                {
                    foreach (var arg in args)
                    {
                        inputContent += arg + " ";
                    }
                }
            }
            return (inputFile, outputFile, inputContent);
        }

        private static void Exit()
        {
            throw new NotImplementedException();
        }

        static void Calclate(Stream inputStream, Stream outputStream)
        {
            using (var reader = new StreamReader(inputStream, encode))
            using (var writer = new StreamWriter(outputStream, encode))
            {
                int readChar;
                while ((readChar = reader.Read()) != -1)
                {
                    Debug.WriteLine($"{DateTime.Now} {readChar}");
                    char c = (char)readChar;

                    writer.Write(c);
                    writer.Flush();
                }
            }
        }
    }
}