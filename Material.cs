using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Term_Paper_Rudenko
{
    public abstract class Material
    {
        public static string folderName;
        protected string fileName;

        public static string newline;

        protected int _id;
        protected string _name;

        public Material()
        {
            _id = 0;
            _name = "";
        }

        public Material(int ID)
        {
            _id = ID;
        }

        public Material(string name)
        {
            _name = name;
        }

        public Material(int ID, string name)
        {
            _id = ID;
            _name = name;
        }

        public int ID
        {
            get
            {
                return _id;
            }

            set
            {
                try
                {
                    _id = value;
                }
                catch
                {
                }
            }
        }

        public string Name
        {
            get
            {
                return _name;
            }

            set
            {
                _name = value;
            }
        }

        public string FileName
        {
            get
            {
                return fileName;
            }

            set
            {
                fileName = value;
            }
        }

        public string FolderName
        {
            get
            {
                return folderName;
            }
            set
            {
                folderName = value;
            }
        }

        public string NewLine
        {
            get
            {
                return newline;
            }
        }
    }
}
