using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Term_Paper_Rudenko
{
    public class QuestionChoice : Question
    {
        public QuestionChoice()
            : base()
        {
            _name = "";
            _answers = new List<ChoiceAnswer>();
        }

        public QuestionChoice(string NAME)
            : base(NAME)
        {
            _name = NAME;
            _answers = new List<ChoiceAnswer>();
        }

        public QuestionChoice(string NAME, List<ChoiceAnswer> a)
            : base(NAME)
        {
            _name = NAME;
            _answers = a;
        }

        public override string String()
        {
            string info = "";

            string type = "QuestionChoice";

            info += _name + _tab + type + _tab + _id + _tab;

            for (int i = 0; i < _answers.Count; i++)
            {
                if (i + 1 == _answers.Count) // if it is the last item
                {
                    info += _answers[i].String();
                }
                else
                {
                    info += _answers[i].String() + _tab;
                }
            }

            return info;
        }

        public void AddChoiceAnswer(string name, bool value)
        {
            _answers.Add(new ChoiceAnswer(name, value));
        }

        public void RemoveAnswer(int index)
        {
            _answers.RemoveAt(index);
        }
    }
}
