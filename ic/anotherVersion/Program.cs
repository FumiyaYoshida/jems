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

            try
            {
                if (args.Length != 3)
                {
                    throw new MyAppException("引数は3つ指定してください。");
                }
                string ope = args[1];
                ValidateOperater(ope);
                int a = ValidateOperand(args[0]);
                int b = ValidateOperand(args[2]);
                    
                int result = Calc(a, b, ope, out int surplus);
                Console.WriteLine($"{a} {ope} {b} = {result}" + ((surplus != 0) ? $" ({surplus})" : ""));
            }
            catch (MyAppException ex)
            {
                Console.Error.WriteLine(ex.Message + "\r\n" + usage);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine("異常が発生しました。プログラムを終了します。\r\n" + ex.Message);
            }
        }

        /// <summary>
        /// 四則演算の計算を行う
        /// </summary>
        /// <param name="a">一つ目の整数</param>
        /// <param name="b">二つ目の整数、割り算の場合は除数</param>
        /// <param name="ope">四則演算子</param>
        /// <param name="surplus">割り算の場合は剰余が格納される。割り算以外の場合は0が格納される。</param>
        /// <returns>計算結果</returns>
        /// <exception cref="MyAppException">計算結果がオーバーフローしたとき、または0除算のとき</exception>
        static int Calc(int a, int b, string ope, out int surplus)
        {
            surplus = 0;

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
                                throw new MyAppException("割り算の場合、割る数に0を指定できません。");
                            }
                            else
                            {
                                surplus = a % b;
                                return a / b;
                            }
                        default:
                            throw new FormatException();
                    }
                }
            }
            catch (OverflowException ex)
            {
                throw new MyAppException("計算結果でオーバーフローが発生しました。", ex);
            }
        }

        /// <summary>
        /// 入力文字の整数値の検証
        /// </summary>
        /// <param name="operand">検査対象の文字列</param>
        /// <returns>true:OK, false:NG</returns>
        /// <exception cref="MyAppException">文字列が整数に変換できない場合、またはオーバーフローしたとき</exception>
        static int ValidateOperand(string operand)
        {
            if (Regex.IsMatch(operand, "^[0-9]+$"))
            {
                try
                {
                    return int.Parse(operand);
                }
                catch (OverflowException ex)
                {
                    throw new MyAppException("引数でオーバーフローが発生しました。", ex);
                }
            }
            else
            {
                throw new MyAppException("正しい整数値を指定してください。");
            }
        }

        /// <summary>
        /// 入力文字列の四則演算子の検証
        /// </summary>
        /// <param name="ope">検査対象の文字列</param>
        /// <returns>true:OK, false:NG</returns>
        /// <exception cref="MyAppException">引数が四則演算子(+,-,*,/)以外だったとき</exception>
        static void ValidateOperater(string ope)
        {
            if (!Regex.IsMatch(ope, "^\\+$|^-$|^\\*$|^/$"))
            {
                throw new MyAppException("2つ目の引数には四則演算子(+,-,*,/)を入力してください。");
            }
        }
    }
}