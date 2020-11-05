using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Term_Paper_Rudenko
{
    public class ChoiceAnswer : Answer
    {
        private string _name;
        private bool _correct;

        public ChoiceAnswer()
        {
            _name = "";
            _correct = false;
        }

        public ChoiceAnswer(string NAME)
        {
            _name = NAME;
            _correct = false;
        }

        public ChoiceAnswer(string NAME, bool c)
        {
            _name = NAME;
            _correct = c;
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

        public bool Correct
        {
            get
            {
                return _correct;
            }

            set
            {
                _correct = value;
            }
        }

        public override string String()
        {
            return _name + _tab + _correct;
        }
    }
}
