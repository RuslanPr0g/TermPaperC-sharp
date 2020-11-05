using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Term_Paper_Rudenko
{
    public class Student : User
    {
        private string _group;

        public Student()
        {
            _group = "";
        }

        public Student(string username, string password, string name, string lastname, string group)
            : base(username, password, name, lastname)
        {
            _group = group;
        }

        public string Group
        {
            get
            {
                return _group;
            }
            set
            {
                _group = value;
            }
        }
    }
}
