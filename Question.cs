using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Term_Paper_Rudenko
{
    public abstract class Question
    {
        protected int _id;
        protected string _name;
        protected string _type;

        public static string _tab = "(*question_tab*)";

        public Question()
        {
            _name = "";
        }

        public Question(string NAME)
        {
            _name = NAME;
        }

        protected List<ChoiceAnswer> _answers;
        protected GetValueAnswer _answer;

        public List<ChoiceAnswer> Answers
        {
            get
            {
                return _answers;
            }

            set
            {
                _answers = value;
            }
        }

        public GetValueAnswer Answer
        {
            get
            {
                return _answer;
            }

            set
            {
                _answer = value;
            }
        }

        public string Type
        {
            get
            {
                return _type;
            }

            set
            {
                _type = value;
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

        public abstract string String();
    }
}
