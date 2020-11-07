using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;

namespace Term_Paper_Rudenko
{
    public class AuthHandler
    {
        FileHandler FH = new FileHandler();

        public static string TRUE = "";

        private string login = "LOGIN";
        private string signup = "SIGNUP";
        private string needAnAccount = "Need an account?";
        private string alreadyHere = "Already here?";

        private string students = "students.bin";
        private string teachers = "teachers.bin";

        public AuthHandler()
        {

        }

        public bool RequiredSymbols(string s)
        {
            bool number = false;
            bool uppercase = false;
            bool lowercase = false;

            for (int i = 0; i < s.Length; i++)
            {
                if (Char.IsUpper(s[i]) == true)
                {
                    uppercase = true;
                }
                if (Char.IsLower(s[i]) == true)
                {
                    lowercase = true;
                }
                if (Char.IsDigit(s[i]) == true)
                {
                    number = true;
                }
            }

            return (uppercase == true && lowercase == true && number == true);
        }

        public bool AreForbiddenSymbols(string S)
        {
            foreach (char s in S)
            {
                if (ForbiddenSymbols.SignUP.Contains(s))
                {
                    return true;
                }
            }

            return false;
        }

        public void AddStudent(string username, string password, string name, string lastname, string group)
        {
            Hasher H = new Hasher();

            FH.WriteStudentToFile(username, H.HashString(password), name, lastname, group);
        }

        public void AddTeacher(string username, string password, string name, string lastname)
        {
            Hasher H = new Hasher();

            FH.WriteTeacherToFile(username, H.HashString(password), name, lastname);
        }

        public bool CheckStudent(string username, string password)
        {
            bool exists = false;

            Hasher H = new Hasher();

            List<Student> students = FH.ReadStudentsFromFile();

            foreach (Student student in students)
            {
                if (student.Username == username && H.UnHashString(student.Password) == password)
                {
                    exists = true;
                }
            }

            return exists;
        }

        public bool CheckTeacher(string username, string password)
        {
            bool exists = false;

            Hasher H = new Hasher();

            List<Teacher> teachers = FH.ReadTeachersFromFile();

            foreach (Teacher teacher in teachers)
            {
                if (teacher.Username == username && H.UnHashString(teacher.Password) == password)
                {
                    exists = true;
                }
            }

            return exists;
        }

        public string SignupRequirements(bool student, string username, string password, string passwordToConfirm, string name, string lastname)
        {
            if (password != passwordToConfirm)
            {
                return ("Passwords have to coincide.");
            }

            if (RequiredSymbols(password) == false)
            {
                return "You need to use at least one number, one lowercase letter, one uppercase letter.\nExample: Sl0n.";
            }

            if (AreForbiddenSymbols(password) == true || AreForbiddenSymbols(username) == true)
            {
                string ff = "";

                for (int i = 0; i < ForbiddenSymbols.SignUP.Length; i++)
                    ff += ForbiddenSymbols.SignUP[i];

                return "Fields cannot contain the following: " + ff;
            }

            if (username.Length > 16 || password.Length > 16)
            {
                return "Username and/or Password must be shorter than 16 letters.";
            }

            if (name.Length > 30 || lastname.Length > 30)
            {
                return "Name and/or Lastname is too long.";
            }

            if (student == false && CheckTeacher(username, password) == true)
            {
                return ("Teacher exists.");
            }
            else if (CheckStudent(username, password) == true)
            {
                return ("Student exists.");
            }

            if (username == password)
            {
                return ("Password and Username cannot coincide.");
            }

            return TRUE;
        }

        public string SignUPTeacher(string username, string password, string passwordToConfirm, string name, string lastname)
        {
            string requirements = this.SignupRequirements(false, username, password, passwordToConfirm, name, lastname);

            if (requirements != TRUE)
            {
                return requirements;
            }

            AddTeacher(username, password, name, lastname);

            return TRUE;
        }

        public string SignUPStudent(string username, string password, string passwordToConfirm, string name, string lastname, string group)
        {
            string requirements = this.SignupRequirements(true, username, password, passwordToConfirm, name, lastname);

            if (requirements != TRUE)
            {
                return requirements;
            }

            AddStudent(username, password, name, lastname, group);

            return TRUE;
        }

        public string LogINStudent(string username, string password)
        {
            try
            {
                if (CheckStudent(username, password) == true)
                {
                    return TRUE;
                }
                else
                {
                    return "Username does not exist or password is incorrect.";
                }
            }
            catch
            {
                return ("Username does not exist or password is incorrect.");
            }
        }

        public string LogINTeacher(string username, string password)
        {
            try
            {
                if (CheckTeacher(username, password) == true)
                {
                    return TRUE;
                }
                else
                {
                    return ("Username does not exist or password is incorrect.");
                }
            }
            catch
            {
                return ("Username does not exist or password is incorrect.");
            }
        }

        public string LoginText
        {
            get
            {
                return login;
            }
        }

        public string SignupText
        {
            get
            {

                return signup;
            }
        }

        public string NeedAccount
        {
            get
            {
                return needAnAccount;
            }
        }

        public string AccountExists
        {

            get
            {
                return alreadyHere;
            }
        }

        public string StudentsFile
        {
            get
            {
                return students;
            }
        }

        public string TeachersFile
        {
            get
            {
                return teachers;
            }
        }
    }
}
