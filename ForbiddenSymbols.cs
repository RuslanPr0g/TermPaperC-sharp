using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Term_Paper_Rudenko
{
    public static class ForbiddenSymbols
    {
        public static char[] SignUP
        {
            get
            {
                return Path.GetInvalidFileNameChars();
            }
        }
    }
}
