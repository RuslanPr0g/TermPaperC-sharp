using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Term_Paper_Rudenko
{
    public class ControlTask : Material
    {
        protected int lectureID;

        protected int secondsToPass = 2700; // 45 mins

        protected List<QuestionChoice> _questionsChoice = new List<QuestionChoice>();
        protected List<QuestionGetValue> _questionsGetValue = new List<QuestionGetValue>();

        protected int currentQuestionChoice = 0;
        protected int currentQuestionGetValue = 0;

        protected int id;

        public static string _tab = "(*task_tab*)";

        public ControlTask()
            : base()
        {
            fileName = "unnamed.txt";
            folderName = "controlTasks";

            lectureID = -1;
        }

        public ControlTask(int ID, string name)
            : base(ID, name)
        {
            fileName = _id + ".txt";

            folderName = "controlTasks";
            newline = "(*new_line*)";
        }

        public ControlTask(int ID, string name, int LID)
            : base(ID)
        {
            _name = name;
            lectureID = LID;

            fileName = _id + ".txt";

            folderName = "controlTasks";
            newline = "(*new_line*)";
        }

        public int LectureID
        {
            get
            {
                return lectureID;
            }

            set
            {
                lectureID = value;
            }
        }

        public string Tab
        {
            get
            {
                return _tab;
            }
        }

        public int CurrectQuestionChoice
        {
            get
            {
                return currentQuestionChoice;
            }

            set
            {
                currentQuestionChoice = value;
            }
        }

        public int CurrectQuestionGetValue
        {
            get
            {
                return currentQuestionGetValue;
            }

            set
            {
                currentQuestionGetValue = value;
            }
        }

        public int SecondsToPass
        {
            get
            {
                return secondsToPass;
            }

            set
            {
                secondsToPass = value;
            }
        }

        public int NumberOfQuestions
        {
            get
            {
                return _questionsChoice.Count + _questionsGetValue.Count;
            }
        }

        public int CurrentID
        {
            get
            {
                return id;
            }

            set
            {
                id = value;
            }
        }

        public Question GetCurrentQuestionByID(int ID)
        {
            for (int i = 0; i < _questionsChoice.Count; i++)
            {
                if (_questionsChoice[i].ID == ID)
                {
                    _questionsChoice[i].Type = "QuestionChoice";

                    return _questionsChoice[i];
                }
            }

            for (int i = 0; i < _questionsGetValue.Count; i++)
            {
                if (_questionsGetValue[i].ID == ID)
                {
                    _questionsGetValue[i].Type = "QuestionGetValue";
                    return _questionsGetValue[i];
                }
            }

            return null;
        }

        public int GetQuestionIDByCurrentQuestion(Question Q)
        {
            return Q.ID;
        }

        public QuestionChoice QuestionChoice
        {
            get
            {
                return _questionsChoice[currentQuestionChoice];
            }

            set
            {
                _questionsChoice[currentQuestionChoice] = value;
            }
        }

        public QuestionGetValue QuestionGetValue
        {
            get
            {
                return _questionsGetValue[currentQuestionGetValue];
            }

            set
            {
                _questionsGetValue[currentQuestionGetValue] = value;
            }
        }

        public Question GetQuestionByID(int ID)
        {
            return GetCurrentQuestionByID(ID);
        }

        public QuestionChoice GetCurrentQuestionChoice()
        {
            return _questionsChoice[currentQuestionChoice];
        }

        public QuestionGetValue GetCurrentQuestionGetValue()
        {
            return _questionsGetValue[CurrectQuestionGetValue];
        }

        public List<ChoiceAnswer> GetListOfAnswers(int questionID)
        {
            if (GetCurrentQuestionByID(questionID) == null) return null;

            return GetCurrentQuestionByID(questionID).Answers;
        }

        public GetValueAnswer GetValueAnswer(int questionID)
        {
            if (GetCurrentQuestionByID(questionID) == null) return null;

            return GetCurrentQuestionByID(questionID).Answer;
        }

        public string GetTitle()
        {
            int seconds = secondsToPass % 60;
            int minutes = secondsToPass / 60;
            string time = minutes + ":" + seconds;

            return "Task " + _id + 1 + ". " + _name + ". Minutes to pass: " + time + ".";
        }

        public string String()
        {
            string info = string.Empty;

            string questionsInfo = GetQuestionsInfo();

            info += _id + _tab + lectureID + _tab + _name + _tab + secondsToPass + _tab + questionsInfo;

            return info;
        }

        public string GetQuestionsInfo()
        {
            string info = "";

            // begin with getvalue questions

            for (int i = 0; i < _questionsGetValue.Count; i++)
            {
                if (i + 1 == _questionsGetValue.Count) // if it is the last item
                {
                    info += _questionsGetValue[i].String();
                }
                else
                {
                    info += _questionsGetValue[i].String() + _tab;
                }
            }

            if (_questionsChoice.Count > 0)
            {
                info += _tab;
            }

            // end up with choice ones

            for (int i = 0; i < _questionsChoice.Count; i++)
            {
                if (i + 1 == _questionsChoice.Count) // if it is the last item
                {
                    info += _questionsChoice[i].String();
                }
                else
                {
                    info += _questionsChoice[i].String() + _tab;
                }
            }

            return info;
        }

        public static string GetTypeOfChoice(List<ChoiceAnswer> a)
        {
            bool was = false;

            foreach (ChoiceAnswer CA in a)
            {
                if (CA.Correct == true && was == true)
                {
                    return QuestionTypes.MultiChoice;
                }

                if (CA.Correct == true)
                {
                    was = true;
                }
            }

            return QuestionTypes.SingleChoice;
        }
    }
}
