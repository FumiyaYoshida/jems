using System;
using System.Collections.Generic;
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

        public class MyCommandlineParams
        {
            public bool ShowUsage { set; get; }
            public string InputFilePath { set; get; }
            public string OutputFilePath { set; get; }
            public string InputContent { set; get; }
            public string ErrorMessage { set; get; }
            public bool HasError
            {
                get
                {
                    return !string.IsNullOrEmpty(ErrorMessage);
                }
            }
        }

        static void Main(string[] args)
        {
            string usage = @"計算機プログラムを実行します。
>mycalc /?⏎ → 標準エラー出力に使い方を表示
>mycalc /i ファイル名1⏎ → ファイル名1から入力し計算結果を標準出力に出力
>mycalc /i ファイル名1 /o ファイル名2⏎ → ファイル1から入力し計算結果をファイル2に出力
>mycalc 1+2+3+4+5⏎ → 引数を1行のみの入力とし計算結果を標準出力に出力";

            try
            {
                var cp = ParseCommandlineArgs(args);

                if (cp.HasError)
                {
                    Console.Error.WriteLine(cp.ErrorMessage);
                    Console.Error.WriteLine(usage);
                    return;
                }

                if (cp.ShowUsage)
                {
                    Console.Error.WriteLine(usage);
                    return;
                }

                Stream inputStream = null;
                Stream outputStream = null;

                try
                {
                    if (!string.IsNullOrEmpty(cp.InputFilePath))
                    {
                        try
                        {
                            inputStream = new FileStream(cp.InputFilePath, FileMode.Open);
                        }
                        catch (IOException ex)
                        {
                            throw new MyAppException($"入力ファイル{cp.InputFilePath}を開けませんでした。{ex.GetType().Name} {ex.Message}");
                        }
                    }
                    else if (!string.IsNullOrEmpty(cp.InputContent))
                    {
                        inputStream = new MemoryStream(encode.GetBytes(cp.InputContent));
                    }

                    if (!string.IsNullOrEmpty(cp.OutputFilePath))
                    {
                        try
                        {
                            outputStream = new FileStream(cp.OutputFilePath, FileMode.Create);
                        }
                        catch (IOException ex)
                        {
                            throw new MyAppException($"出力ファイル{cp.OutputFilePath}を更新できません。{ex.GetType().Name} {ex.Message}");
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

        static MyCommandlineParams ParseCommandlineArgs(string[] args)
        {
            var cp = new MyCommandlineParams();

            if (args.Length > 0)
            {
                if (args[0] == "/?")
                {
                    cp.ShowUsage = true;
                }
                else if (args[0] == "/i")
                {
                    if (args.Length == 2)
                    {
                        cp.InputFilePath = args[1];
                    }
                    else if (args.Length == 4 && args[2] == "/o")
                    {
                        cp.InputFilePath = args[1];
                        cp.OutputFilePath = args[3];
                    }
                    else
                    {
                        cp.ErrorMessage = "引数が正しくありません。";
                    }
                }
                else
                {
                    cp.InputContent = string.Join(" ", args);
                }
            }
            return cp;
        }

        static void Calclate(Stream inputStream, Stream outputStream)
        {
            using (var reader = new StreamReader(inputStream, encode))
            using (var writer = new StreamWriter(outputStream, encode))
            {
                var parser = new MyCalcParser(reader);
                string token;
                var line = new List<string>();

                do
                {
                    token = parser.ReadToken();

                    if (token == "\n" || token == null)
                    {
                        writer.WriteLine(string.Join(",", line));
                        writer.Flush();
                        line.Clear();
                    }
                    else
                    {
                        line.Add(token);
                    }
                }
                while (token != null);
            }
        }
    }
}