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
            string usage = @"2つの整数を計算します。
>mycalc 整数1 演算子(+|-|*|/) 整数2
例) 2 + 3";
            string errorMessage;
            if (args.Length != 3)
            {
                errorMessage = usage;
            }
            else
            {
                if (ValidateOperand(args[0], out int int1, out errorMessage)
                    && ValidateOperater(args[1], out string ope, out errorMessage)
                    && ValidateOperand(args[2], out int int2, out errorMessage)
                    && ValidatePair(ope, int2, out errorMessage)
                 )
                {
                    calc(int1, int2, args[1]);
                }
            }

            if (!string.IsNullOrEmpty(errorMessage))
            {
                Console.Error.WriteLine(errorMessage);
            }
        }
        static void calc(int int1, int int2, string ope)
        {
            try
            {
                checked
                {
                    int ans;
                    string output;
                    switch (ope)
                    {
                        case "+":
                            ans = int1 + int2;
                            output = $"{int1} + {int2} = {ans}";
                            break;
                        case "-":
                            ans = int1 - int2;
                            output = $"{int1} - {int2} = {ans}";
                            break;
                        case "*":
                            ans = int1 * int2;
                            output = $"{int1} * {int2} = {ans}";
                            break;
                        case "/":
                            int ansQuo = int1 / int2;
                            int ansRem = int1 % int2;
                            output = $"{int1} / {int2} = {ansQuo} ({ansRem})";
                            break;
                        default:
                            throw new FormatException();
                    }
                    Console.WriteLine(output);
                }
            }
            catch (OverflowException)
            {
                Console.Error.WriteLine("異常が発生しました。プログラムを終了します。" +
                    "\r\n（計算でオーバーフローが発生しました。）");
            }
            catch (FormatException)
            {
                Console.Error.WriteLine("異常が発生しました。プログラムを終了します。" +
                    "\r\n（四則演算子の判定でエラーが発生しました。）");
            }
        }
        static bool ValidateOperand(string operand, out int value, out string errorMessage)
        {
            value = 0;
            errorMessage = null;
            if (Regex.IsMatch(operand, "^[0-9]+$"))
            {
                try
                {
                    value = int.Parse(operand);
                }
                catch
                {
                    errorMessage = $"引数でオーバーフローが発生しました。";
                    return false;
                }
                return true;
            }
            else
            {
                errorMessage = "正しい整数値を指定してください。";
                return false;
            }
        }
        static bool ValidateOperater(string operand, out string value, out string errorMessage)
        {
            value = "";
            errorMessage = null;
            if (Regex.IsMatch(operand, "^\\+$|^-$|^\\*$|^/$"))
            {

                value = operand;
                return true;
            }
            else
            {
                errorMessage = "2つ目の引数には四則演算子(+,-,*,/)を入力してください。";
                return false;
            }
        }
        static bool ValidatePair(string ope, int int2, out string errorMessage)
        {
            errorMessage = null;
            if (!(ope == "/" && int2 == 0))
            {
                return true;
            }
            else
            {
                errorMessage = "割り算の場合、割る数に0を指定できません。";
                return false;
            }
        }
    }
}