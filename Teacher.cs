using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Term_Paper_Rudenko
{
    public class Teacher : User
    {
        public Teacher()
        {
        }

        public Teacher(string username, string password, string name, string lastname)
            : base(username, password, name, lastname)
        {
        }
    }
}
