using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mycalc
{
    /// <summary>
    /// �����͂��s���N���X
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
        /// MyCalcParser�N���X�̃R���X�g���N�^
        /// </summary>
        /// <param name="reader">���̓X�g���[��</param>
        public MyCalcParser(TextReader reader)
        {
            this.reader = reader;
        }

        /// <summary>
        /// �X�g���[������v�Z���p�̃g�[�N������ǂݍ��ށB
        /// </summary>
        /// <returns>�g�[�N���i����ȏ�ǂݍ��ރg�[�N�����Ȃ��ꍇ��null��ԋp�j</returns>
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