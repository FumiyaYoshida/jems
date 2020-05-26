using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mycalc
{
    public class MyAppException: Exception
    {
        private MyAppException()
        {

        }

        public MyAppException(string errorMessage)
            :base(errorMessage)
        {

        }

        public MyAppException(string errorMessage, Exception innerException)
            : base(errorMessage, innerException)
        {

        }
    }
}
