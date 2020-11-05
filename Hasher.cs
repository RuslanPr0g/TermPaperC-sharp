using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Term_Paper_Rudenko
{
    public class Hasher
    {
        public Hasher()
        {
        }

        public string HashString(string S)
        {
            string reversed = string.Empty;

            string number = "2";

            for (int i_Reverse = S.Length - 1; i_Reverse >= 0; i_Reverse--)
            {
                reversed += S[i_Reverse];
            }

            return reversed + number;
        }

        public string UnHashString(string S)
        {
            string reversed = string.Empty;

            for (int i_Reverse = S.Length - 2; i_Reverse >= 0; i_Reverse--)
            {
                reversed += S[i_Reverse];
            }

            return reversed;
        }
    }
}
