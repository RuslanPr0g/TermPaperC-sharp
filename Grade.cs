using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Term_Paper_Rudenko
{
    public class Grade
    {
        private int _id;
        private string _username;
        private int _taskID;
        private int _grade100;
        private string _grade5;

        public static int Failed = 60;

        public Grade()
        {
            _username = "";
            _taskID = 0;
            _grade100 = 0;
            _grade5 = "";
            _id = 0;
        }

        public Grade(int id)
        {
            _username = "";
            _taskID = 0;
            _grade100 = 0;
            _grade5 = "";
            _id = id;
        }

        public Grade(string s, int ct)
        {
            _username = s;
            _taskID = ct;
            _grade100 = 0;
            _grade5 = "";
            _id = 0;
        }

        public Grade(string s, int ct, string grade5)
        {
            _username = s;
            _taskID = ct;
            _grade5 = grade5;
            this.Calculate(grade5);
            _id = 0;
        }

        public Grade(int itsID, string s, int ct, string grade5)
        {
            _username = s;
            _taskID = ct;
            _grade5 = grade5;
            this.Calculate(grade5);
            _id = itsID;
        }

        public string String()
        {
            return _username + "\t" + _taskID + "\t" + _grade5;
        }

        public void Calculate(int numberOfCorrect, int total)
        {
            decimal val1 = Convert.ToDecimal(numberOfCorrect) / Convert.ToDecimal(total);
            decimal val2 = val1 * 100;

            this.TranslateTo5(Convert.ToDouble(val2));
        }

        public void Calculate(string v5)
        {
            TranslateTo100(v5);
        }

        public void TranslateTo5(double v100)
        {
            if (v100 >= 85 && v100 <= 100)
            {
                _grade5 = "A";
            }
            else if (v100 >= 70)
            {
                _grade5 = "B";
            }
            else if (v100 >= 55)
            {
                _grade5 = "C";

            }
            else if (v100 >= 40)
            {
                _grade5 = "D";
            }
            else if (v100 >= 25)
            {
                _grade5 = "E";
            }
            else if (v100 >= 10)
            {
                _grade5 = "F";
            }
            else if (v100 < 10)
            {
                _grade5 = "NG";
            }
        }

        public string GetAs5(double v100)
        {
            if (v100 >= 85 && v100 <= 100)
            {
                return "A";
            }
            else if (v100 >= 70)
            {
                return "B";
            }
            else if (v100 >= 55)
            {
                return "C";

            }
            else if (v100 >= 40)
            {
                return "D";
            }
            else if (v100 >= 25)
            {
                return "E";
            }
            else if (v100 >= 10)
            {
                return "F";
            }
            else if (v100 < 10)
            {
                return "NG";
            }

            return null;
        }

        public void TranslateTo100(string v5)
        {
            if (v5 == "A")
            {
                _grade100 = 100;
            }
            else if (v5 == "B")
            {
                _grade100 = 80;
            }
            else if (v5 == "C")
            {
                _grade100 = 55;
            }
            else if (v5 == "D")
            {
                _grade100 = 40;
            }
            else if (v5 == "E")
            {
                _grade100 = 25;
            }
            else if (v5 == "F")
            {
                _grade100 = 10;
            }
            else if (v5 == "NG")
            {
                _grade100 = 0;
            }
        }

        public double GetAs100(string v5)
        {
            if (v5 == "A")
            {
                return 100;
            }
            else if (v5 == "B")
            {
                return 80;
            }
            else if (v5 == "C")
            {
                return 55;
            }
            else if (v5 == "D")
            {
                return 40;
            }
            else if (v5 == "E")
            {
                return 25;
            }
            else if (v5 == "F")
            {
                return 10;
            }
            else if (v5 == "NG")
            {
                return 0;
            }

            return -1;
        }

        public static double CalculateAverage(List<Grade> grades)
        {
            double sum = 0;

            foreach (Grade grade in grades)
                sum += grade._grade100;

            return Math.Round(sum / grades.Count, 1);
        }

        public bool Fail
        {
            get
            {
                return _grade100 < Grade.Failed;
            }
        }

        public string StudentUsername
        {
            get
            {
                return _username;
            }

            set
            {
                _username = value;
            }
        }

        public int ControlTask
        {
            get
            {
                return _taskID;
            }

            set
            {
                _taskID = value;
            }
        }

        public int Grade100
        {
            get
            {
                return _grade100;
            }

            set
            {
                _grade100 = value;
            }
        }

        public string Grade5
        {
            get
            {
                return _grade5;
            }

            set
            {
                _grade5 = value;
            }
        }

        public int ID
        {
            get
            {
                return _id;
            }

            set
            {
                _id = value;
            }
        }
    }
}
