using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Term_Paper_Rudenko
{
    public class User
    {
        protected string username;
        protected string password;
        protected string name;
        protected string lastname;

        public User()
        {
            username = "";
            password = "";
            name = "";
            lastname = "";
        }

        public User(string username, string password, string name, string lastname)
        {
            this.username = username;
            this.password = password;
            this.name = name;
            this.lastname = lastname;
        }

        public string Username
        {
            get
            {
                return username;
            }
            set
            {
                username = value;
            }
        }

        public string Password
        {
            get
            {
                return password;
            }
            set
            {
                password = value;
            }
        }

        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
            }
        }

        public string Lastname
        {
            get
            {
                return lastname;
            }
            set
            {
                lastname = value;
            }
        }
    }
}
