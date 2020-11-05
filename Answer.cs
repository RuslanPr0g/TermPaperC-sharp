using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Term_Paper_Rudenko
{
    public abstract class Answer
    {
        public static string _tab = "(*answer_tab*)";
        public static string _end = "(*answer_end*)";

        public abstract string String();
    }
}
