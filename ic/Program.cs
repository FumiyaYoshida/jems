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
                if (args.Length != 3)
                {
                    throw new ArgumentException();
                }

                int int1 = int.Parse(args[0]);
                int int2 = int.Parse(args[2]);

                try
                {
                    checked
                    {
                        var ope = args[1];
                        int ans;

                        switch (ope)
                        {
                            case "/":
                                int ansQuo = int1 / int2;
                                int ansRem = int1 % int2;
                                Console.WriteLine($"{int1} / {int2} = {ansQuo} ({ansRem})");
                                break;
                            case "*":
                                ans = int1 * int2;
                                Console.WriteLine($"{int1} * {int2} = {ans}");
                                break;
                            case "+":
                                ans = int1 + int2;
                                Console.WriteLine($"{int1} + {int2} = {ans}");
                                break;
                            case "-":
                                ans = int1 - int2;
                                Console.WriteLine($"{int1} - {int2} = {ans}");
                                break;
                            default:
                                throw new ArgumentException();
                        }
                    }
                }
                catch (OverflowException)
                {
                    Console.WriteLine("計算結果にオーバーフローが発生しました。");
                }
                catch (DivideByZeroException)
                {
                    Console.WriteLine("0で割ることはできません。");
                }
            }
            catch (ArgumentException)
            {
                Console.WriteLine("以下のように入力してください。");
                Console.WriteLine("mycalc (整数値) (四則演算子) (整数値)");
            }
            catch (OverflowException)
            {
                Console.WriteLine("引数にオーバーフローが発生しました。");
            }
            catch (FormatException)
            {
                Console.WriteLine("整数値の部分に数字以外の文字列が含まれています。");
            }
        }
    }
}
