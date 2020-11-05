using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Term_Paper_Rudenko
{
    public class AddEditControlTask : ControlTask
    {
        public AddEditControlTask()
            : base()
        {

        }

        public AddEditControlTask(int ID, string name)
            : base(ID, name)
        {

        }

        public AddEditControlTask(int ID, string name, int LID)
            : base(ID, name, LID)
        {

        }

        public void AddQuestionChoice(string name, List<ChoiceAnswer> answers)
        {
            _questionsChoice.Add(new QuestionChoice(name, answers));
            _questionsChoice[_questionsChoice.Count - 1].ID = id;
            id++;
            currentQuestionChoice++;
        }

        public void EditQuestionChoice(int ID, string name, List<ChoiceAnswer> answers)
        {
            for (int i = 0; i < _questionsChoice.Count; i++)
            {
                if (_questionsChoice[i].ID == ID)
                {
                    _questionsChoice[i].Type = "QuestionChoice";

                    _questionsChoice[i].Name = name;

                    _questionsChoice[i].Answers = answers;

                    return;
                }
            }
        }

        public void AddQuestionChoice(string name)
        {
            _questionsChoice.Add(new QuestionChoice(name));
            _questionsChoice[_questionsChoice.Count - 1].ID = id;
            id++;
            currentQuestionChoice++;
        }

        public void AddQuestionChoice(string name, int ID)
        {
            _questionsChoice.Add(new QuestionChoice(name));
            _questionsChoice[_questionsChoice.Count - 1].ID = ID;

            if (id < ID)
            {
                id = ID;
            }

            currentQuestionChoice++;
        }

        public void AddQuestionGetValue(string name, GetValueAnswer answer)
        {
            _questionsGetValue.Add(new QuestionGetValue(name, answer));
            _questionsGetValue[_questionsGetValue.Count - 1].ID = id;
            id++;
            currentQuestionGetValue++;
        }

        public void EditQuestionGetValue(int ID, string name, GetValueAnswer answer)
        {
            for (int i = 0; i < _questionsGetValue.Count; i++)
            {
                if (_questionsGetValue[i].ID == ID)
                {
                    _questionsGetValue[i].Type = "QuestionGetValue";

                    _questionsGetValue[i].Name = name;

                    _questionsGetValue[i].Answer = answer;

                    return;
                }
            }
        }

        public void AddQuestionGetValue(string name)
        {
            _questionsGetValue.Add(new QuestionGetValue(name));
            _questionsGetValue[_questionsGetValue.Count - 1].ID = id;
            id++;
            currentQuestionGetValue++;
        }

        public void AddQuestionGetValue(string name, int ID)
        {
            _questionsGetValue.Add(new QuestionGetValue(name));
            _questionsGetValue[_questionsGetValue.Count - 1].ID = ID;
            if (id < ID)
                id = ID;
            currentQuestionGetValue++;
        }

        public void RemoveQuestionChoice(int index)
        {
            int prev = _questionsChoice[index].ID;

            _questionsChoice.RemoveAt(index);

            for (int i = 0; i < _questionsGetValue.Count; i++)
            {
                if (_questionsGetValue[i].ID > prev)
                {
                    _questionsGetValue[i].ID--;
                }
            }

            for (int i = 0; i < _questionsChoice.Count; i++)
            {
                if (_questionsChoice[i].ID > prev)
                {
                    _questionsChoice[i].ID--;
                }
            }

            id--;
        }

        public void RemoveQuestionGetValue(int index)
        {
            int prev = _questionsGetValue[index].ID;

            _questionsGetValue.RemoveAt(index);

            for (int i = 0; i < _questionsGetValue.Count; i++)
            {
                if (_questionsGetValue[i].ID > prev)
                {
                    _questionsGetValue[i].ID--;
                }
            }

            for (int i = 0; i < _questionsChoice.Count; i++)
            {
                if (_questionsChoice[i].ID > prev)
                {
                    _questionsChoice[i].ID--;
                }
            }

            id--;
        }

        public void AddChoiceAnswer(int ID, string name, bool value)
        {
            for (int i = 0; i < _questionsChoice.Count; i++)
            {
                if (_questionsChoice[i].ID == ID)
                {
                    _questionsChoice[i].AddChoiceAnswer(name, value);
                }
            }
        }

        public void AddGetValueAnswer(int ID, string value)
        {
            for (int i = 0; i < _questionsGetValue.Count; i++)
            {
                if (_questionsGetValue[i].ID == ID)
                {
                    _questionsGetValue[i].ChangeGetValueAnswer(value);
                }
            }
        }

        public void RemoveAnswerChoice(int ID, int indexA)
        {
            for (int i = 0; i < _questionsChoice.Count; i++)
            {
                if (_questionsChoice[i].ID == ID)
                {
                    _questionsChoice[i].RemoveAnswer(indexA);
                }
            }
        }

        public void RemoveQuestionByID(int currentID)
        {
            for (int i = 0; i < _questionsChoice.Count; i++)
            {
                if (_questionsChoice[i].ID == currentID)
                {
                    this.RemoveQuestionChoice(i);

                    for (int j = 0; j < _questionsChoice.Count; j++)
                    {
                        if (_questionsChoice[j].ID > currentID)
                        {
                            _questionsChoice[j].ID--;
                        }
                    }
                }
            }

            for (int i = 0; i < _questionsGetValue.Count; i++)
            {
                if (_questionsGetValue[i].ID == currentID)
                {
                    this.RemoveQuestionGetValue(i);

                    for (int j = 0; j < _questionsGetValue.Count; j++)
                    {
                        if (_questionsGetValue[j].ID > currentID)
                        {
                            _questionsGetValue[j].ID--;
                        }
                    }
                }
            }

            id--;
        }
    }
}
