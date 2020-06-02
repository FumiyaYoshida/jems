using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mycalc
{
    /// <summary>
    /// 字句解析を行うクラス
    /// </summary>
    public class MyCalcParser
    {
        private char? preReadSymbol = null;
        private TextReader reader;
        private readonly string symbols = "+-*/()\n";

        private MyCalcParser()
        {

        }

        /// <summary>
        /// MyCalcParserクラスのコンストラクタ
        /// </summary>
        /// <param name="reader">入力ストリーム</param>
        public MyCalcParser(TextReader reader)
        {
            this.reader = reader;
        }

        /// <summary>
        /// ストリームから計算式用のトークンを一つ読み込む。
        /// </summary>
        /// <returns>トークン（それ以上読み込むトークンがない場合はnullを返却）</returns>
        public string ReadToken()
        {
            var token = new StringBuilder();
            if (preReadSymbol != null)
            {
                token.Append(preReadSymbol);
                preReadSymbol = null;
            }
            else
            {
                int readChar;
                while ((readChar = reader.Read()) != -1)
                {
                    char c = (char)readChar;
                    if (symbols.Contains(c))
                    {
                        if (token.Length == 0)
                        {
                            token.Append(c.ToString());
                        }
                        else
                        {
                            preReadSymbol = c;
                        }
                        break;
                    }
                    else
                    {
                        if (char.IsWhiteSpace(c))
                        {
                            if (token.Length > 0)
                            {
                                break;
                            }
                        }
                        else
                        {
                            token.Append(c.ToString());
                        }
                    }
                }
            }
            return token.Length > 0 ? token.ToString() : null;
        }
    }
}