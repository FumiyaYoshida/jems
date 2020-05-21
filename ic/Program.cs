using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
namespace mycalc
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 3)
            {
                if (Regex.IsMatch(args[0], "^[0-9]+$"))
                {
                    try
                    {
                        int int1 = int.Parse(args[0]);
                        if (Regex.IsMatch(args[1], "^\\+$|^-$|^\\*$|^/$"))
                        {
                            if (Regex.IsMatch(args[2], "^[0-9]+$"))
                            {
                                try
                                {
                                    int int2 = int.Parse(args[2]);
                                    if (!(args[1] == "/" && args[2] == "0"))
                                    {
                                        calc(int1, int2, args[1]);
                                    }
                                    else
                                    {
                                        stdErrOut("割り算の場合、割る数に0を指定できません。");
                                    }
                                }
                                catch (OverflowException)
                                {
                                    stdErrOut("3つ目の引数にオーバーフローが発生しました。");
                                }
                            }
                            else
                            {
                                stdErrOut("3つ目の引数には整数値を入力してください。");
                            }
                        }
                        else
                        {
                            stdErrOut("2つ目の引数には四則演算子(+,-,*,/)を一つ入力してください。");
                        }
                    }
                    catch (OverflowException)
                    {
                        stdErrOut("1つ目の引数にオーバーフローが発生しました。");
                    }
                }
                else
                {
                    stdErrOut("1つ目の引数には整数値を入力してください。");
                }
            }
            else
            {
                stdErrOut("次のように入力してください。" +
                    "\r\n> mycalc (整数値) (四則演算子) (整数値)" +
                    "\r\n四則演算子の所には+,-,*,/のいずれかを入力してください。");
            }
        }
        static void calc(int int1, int int2, string ope)
        {
            try
            {
                checked
                {
                    int ans;
                    switch (ope)
                    {
                        case "+":
                            ans = int1 + int2;
                            stdOut($"{int1} + {int2} = {ans}");
                            break;
                        case "-":
                            ans = int1 - int2;
                            stdOut($"{int1} - {int2} = {ans}");
                            break;
                        case "*":
                            ans = int1 * int2;
                            stdOut($"{int1} * {int2} = {ans}");
                            break;
                        case "/":
                            int ansQuo = int1 / int2;
                            int ansRem = int1 % int2;
                            stdOut($"{int1} / {int2} = {ansQuo} ({ansRem})");
                            break;
                        default:
                            throw new FormatException();
                    }
                }
            }
            catch (OverflowException)
            {
                stdErrOut("異常が発生しました。プログラムを終了します。" +
                    "\r\n（計算でオーバーフローが発生しました。）");
            }
            catch (FormatException)
            {
                stdErrOut("異常が発生しました。プログラムを終了します。" +
                    "\r\n（四則演算子の判定でエラーが発生しました。）");
            }
        }
        static void stdOut(string msg)
        {
            Console.WriteLine(msg);
        }
        static void stdErrOut(string msg)
        {
            Console.Error.WriteLine(msg);
        }
    }
}