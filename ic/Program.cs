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
            string errorMessage = null;
            if (args.Length != 3)
            {
                errorMessage = usage;
            }
            else
            {
                string ope = args[1];
                if (ValidateOperand(args[0], out int a, out errorMessage)
                    && ValidateOperater(ope, out errorMessage)
                    && ValidateOperand(args[2], out int b, out errorMessage)
                 )
                {
                    int result = Calc(a, b, ope, out int surplus, out errorMessage);
                    if (string.IsNullOrEmpty(errorMessage))
                    {
                        Console.WriteLine($"{a} {ope} {b} = {result}" + ((surplus != 0) ? $" ({surplus})" : ""));
                    }
                }
            }
            if (!string.IsNullOrEmpty(errorMessage))
            {
                Console.Error.WriteLine(errorMessage);
            }
        }
        /// <summary>
        /// 四則演算の計算を行う
        /// </summary>
        /// <param name="a">一つ目の整数</param>
        /// <param name="b">二つ目の整数</param>
        /// <param name="ope">四則演算子</param>
        /// <param name="surplus">割り算の余り。割り算以外の時は0が入っており、で利用されない。</param>
        /// <param name="errorMessage">NGの場合はエラーメッセージ、OKの場合はnullが返る。</param>
        /// <returns>計算結果</returns>
        static int Calc(int a, int b, string ope, out int surplus, out string errorMessage)
        {
            surplus = 0;
            errorMessage = null;
            try
            {
                checked
                {
                    switch (ope)
                    {
                        case "+":
                            return a + b;
                        case "-":
                            return a - b;
                        case "*":
                            return a * b;
                        case "/":
                            if (b == 0)
                            {
                                errorMessage = "割り算の場合、割る数に0を指定できません。";
                            }
                            else
                            {
                                surplus = a % b;
                                return a / b;
                            }
                            break;
                        default:
                            throw new FormatException();
                    }
                }
            }
            catch (OverflowException)
            {
                errorMessage = "計算結果でオーバーフローが発生しました。";
            }
            return 0;
        }
        /// <summary>
        /// 入力文字の整数値の検証
        /// </summary>
        /// <param name="operand">検査対象の文字列</param>
        /// <param name="value">NGの場合は0、OKの場合は検査対象の文字列をint型に変換したものが返る。</param>
        /// <param name="errorMessage">NGの場合はエラーメッセージ、OKの場合はnullが返る。</param>
        /// <returns>true:OK, false:NG</returns>
        static bool ValidateOperand(string operand, out int value, out string errorMessage)
        {
            value = 0;
            errorMessage = null;
            if (Regex.IsMatch(operand, "^[0-9]+$"))
            {
                try
                {
                    value = int.Parse(operand);
                    return true;
                }
                catch (OverflowException)
                {
                    errorMessage = "引数でオーバーフローが発生しました。";
                }
            }
            else
            {
                errorMessage = "正しい整数値を指定してください。";
            }
            return false;
        }
        /// <summary>
        /// 入力文字列の四則演算子の検証
        /// </summary>
        /// <param name="ope">検査対象の文字列</param>
        /// <param name="errorMessage">NGの場合はエラーメッセージ、OKの場合はnullが返る。。</param>
        /// <returns>true:OK, false:NG</returns>
        static bool ValidateOperater(string ope, out string errorMessage)
        {
            errorMessage = null;
            if (Regex.IsMatch(ope, "^\\+$|^-$|^\\*$|^/$"))
            {
                return true;
            }
            else
            {
                errorMessage = "2つ目の引数には四則演算子(+,-,*,/)を入力してください。";
            }
            return false;
        }
    }
}