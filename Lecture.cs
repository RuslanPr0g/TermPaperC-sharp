using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace Term_Paper_Rudenko
{
    public class Lecture : Material
    {
        private string _topic;

        List<Portion> portions = new List<Portion>();

        public Lecture()
            : base()
        {
            folderName = "lectures";
            fileName = "unnamed.txt";
            newline = "(*new_line*)";
        }

        public Lecture(int ID, string name, string topic)
            : base(ID, name)
        {
            _topic = topic;
            fileName = _id + ".txt";

            folderName = "lectures";
            newline = "(*new_line*)";
        }

        public string GetTitle()
        {
            return "Lecture " + (_id + 1) + ". " + _name + ". " + _topic;
        }

        public string Info()
        {
            string info = string.Empty;

            string portionsInfo = GetPortionsInfo();

            info += _id + "\t" + _name + "\t" + _topic + "\t" + portionsInfo;

            return info;
        }

        public string GetPortionsInfo()
        {
            string portionsInfo = string.Empty;

            foreach (Portion portion in portions)
            {
                portionsInfo += portion.Content;

                portionsInfo += newline;
            }

            return portionsInfo;
        }

        public void AddPortion(string content)
        {
            portions.Add(new Portion(content));
        }

        public void RemovePortion(int ID)
        {
            try
            {
                portions.RemoveAt(ID);
            }
            catch
            {
            }
        }

        public string GetPortion(int ID)
        {
            try
            {
                return portions[ID].Content;
            }
            catch
            {
                return "";
            }
        }

        public void ModifyPortion(int id, string data)
        {
            portions[id].Content = data;
        }

        public int GetNumberOfPortions()
        {
            return portions.Count;
        }

        public string Topic
        {
            get
            {
                return _topic;
            }

            set
            {
                _topic = value;
            }
        }

        public class Portion
        {
            private string _content;

            public Portion()
            {
                _content = string.Empty;
            }

            public Portion(string content)
            {
                _content = content;
            }

            public string Content
            {
                get
                {
                    return _content;
                }

                set
                {
                    _content = value;
                }
            }
        }
    }
}
