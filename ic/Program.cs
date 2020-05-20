using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace intCalc
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                string arg = Console.ReadLine();
                string[] arr = arg.Split(' ');
                int int1;
                int int2;

                //引数の個数の確認
                if (arr.Length != 4)
                {
                    throw new ArgumentException();
                }

                //最初の引数がmycalcであることの確認
                if (arr[0] != "mycalc")
                {
                    throw new ArgumentException();
                }

                //2つ目の引数がint型に収まる整数値であるかの確認
                if (Regex.IsMatch(arr[1], "^[0-9]+$"))
                {
                    int1 = int.Parse(arr[1]);
                }
                else
                {
                    throw new ArgumentException();
                }

                //4つ目の引数がint型に収まる整数値であることの確認
                if (Regex.IsMatch(arr[3], "^[0-9]+$"))
                {
                    int2 = int.Parse(arr[3]);
                }
                else
                {
                    throw new ArgumentException();
                }

                //3つ目の引数値によって、どんな演算がされるかが決まる。
                checked
                {
                    var ope = arr[2];
                    int ans;

                    switch (ope)
                    {
                        case "/":
                            int ansQuo = int1 / int2;
                            int ansRem = int1 % int2;
                            Console.WriteLine($"{int1} * {int2} = {ansQuo} ({ansRem})");
                            break;
                        case "*":
                            ans = int1 * int2;
                            Console.WriteLine($"{int1} * {int2} = {ans}");
                            break;
                        case "+":
                            ans = int1 + int2;
                            Console.WriteLine($"{int1} * {int2} = {ans}");
                            break;
                        case "-":
                            ans = int1 - int2;
                            Console.WriteLine($"{int1} * {int2} = {ans}");
                            break;
                        default:
                            throw new ArgumentException();
                    }
                }
            }
            catch (ArgumentException)
            {
                Console.WriteLine("以下のように入力してください。");
                Console.WriteLine("mycalc (整数値) (四則演算子) (整数値)");
            }
            catch (OverflowException)
            {
                Console.WriteLine("オーバーフローが発生しました。");
            }
            catch (DivideByZeroException)
            {
                Console.WriteLine("0で割ることはできません。");
            }
        }
    }
}