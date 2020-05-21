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
            try
            {
                //引数の個数の確認
                if (args.Length == 3
                    && Regex.IsMatch(args[0], "^[0-9]+$")
                    && Regex.IsMatch(args[2], "^[0-9]+$")
                 )
                {
                    calc(int.Parse(args[0]), int.Parse(args[2]), args[1]);
                }
                else
                {
                    Console.WriteLine("以下のように入力してください。");
                    Console.WriteLine("mycalc (整数値) (四則演算子) (整数値)");
                }
            }
            catch (OverflowException)
            {
                Console.WriteLine("引数にオーバーフローが発生しました。");
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
                            Console.WriteLine($"{int1} + {int2} = {ans}");
                            break;
                        case "-":
                            ans = int1 - int2;
                            Console.WriteLine($"{int1} - {int2} = {ans}");
                            break;
                        case "*":
                            ans = int1 * int2;
                            Console.WriteLine($"{int1} * {int2} = {ans}");
                            break;
                        case "/":
                            if (int2 != 0)
                            {
                                int ansQuo = int1 / int2;
                                int ansRem = int1 % int2;
                                Console.WriteLine($"{int1} / {int2} = {ansQuo} ({ansRem})");
                            }
                            else
                            {
                                Console.WriteLine("0で割ることはできません。");
                            }
                            break;
                        default:
                            Console.WriteLine("2つ目の引数には+,-,*,/のいづれかを入力してください。");
                            break;
                    }
                }
            }
            catch (OverflowException)
            {
                Console.WriteLine("計算結果にオーバーフローが発生しました。");
            }
        }
    }
}