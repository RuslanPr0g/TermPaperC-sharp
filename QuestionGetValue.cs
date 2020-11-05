using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Term_Paper_Rudenko
{
    public class QuestionGetValue : Question
    {
        public QuestionGetValue()
            : base()
        {
            _name = "";
            _answer = new GetValueAnswer();
        }

        public QuestionGetValue(string NAME)
            : base(NAME)
        {
            _name = NAME;
            _answer = new GetValueAnswer();
        }

        public QuestionGetValue(string NAME, GetValueAnswer a)
            : base(NAME)
        {
            _name = NAME;
            _answer = a;
        }

        public override string String()
        {
            string info = "";

            string type = "QuestionGetValue";

            info += _name + _tab + type + _tab + _id + _tab + _answer.String();

            return info;
        }

        public void ChangeGetValueAnswer(string value)
        {
            _answer.CorrectValue = value;
        }
    }
}
