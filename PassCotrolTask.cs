using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Term_Paper_Rudenko
{
    public class PassControlTask : ControlTask
    {
        private int _currentQuestionID;

        FileHandler FH = new FileHandler();
        ControlTask CT;

        Question[] answeredQuestions;
        bool[] answeredID;

        public PassControlTask(ControlTask ct)
        {
            CT = ct;

            answeredID = new bool[CT.NumberOfQuestions];

            answeredQuestions = new Question[answeredID.Length];

            for (int i = 0; i < answeredID.Length; i++)
            {
                answeredID[i] = false;

                if (CT.GetQuestionByID(i).Type == "QuestionChoice")
                    answeredQuestions[i] = new QuestionChoice();
                else if (CT.GetQuestionByID(i).Type == "QuestionGetValue")
                {
                    answeredQuestions[i] = new QuestionGetValue();
                }
            }
        }

        public Question GetNext()
        {
            if (_currentQuestionID + 1 < CT.NumberOfQuestions)
                _currentQuestionID += 1;
            else return null;

            return CT.GetQuestionByID(_currentQuestionID);
        }

        public Question GetPrev()
        {
            if (_currentQuestionID - 1 >= 0)
                _currentQuestionID -= 1;
            else return null;

            return CT.GetQuestionByID(_currentQuestionID);
        }

        public void AnswerToTheQuestion(int ID, List<ChoiceAnswer> a)
        {
            if (a is null)
            {
                throw new ArgumentNullException(nameof(a));
            }

            answeredQuestions[ID].Answers = a;

            answeredID[ID] = true;
        }

        public void AnswerToTheQuestion(int ID, GetValueAnswer a)
        {
            if (a is null)
            {
                throw new ArgumentNullException(nameof(a));
            }

            answeredQuestions[ID].Answer = a;

            answeredID[ID] = true;
        }

        public bool AllAnswered()
        {
            for (int i = 0; i < answeredID.Length; i++)
                if (answeredID[i] == false)
                    return false;

            return true;
        }

        public int CountAnswered()
        {
            int c = 0;

            for (int i = 0; i < answeredID.Length; i++)
                if (answeredID[i] == true)
                    c++;

            return c;
        }

        public Grade FinishTest()
        {
            if (AllAnswered() == false) return null;

            return CalculateGrade();
        }

        public Grade CalculateGrade()
        {
            Grade grade = new Grade();

            grade.Calculate(this.CorrectAnsweredQuestions().Count, CT.NumberOfQuestions);

            return grade;
        }

        public List<Question> CorrectAnsweredQuestions()
        {
            List<Question> data = new List<Question>();

            bool all;

            for (int i = 0; i < answeredQuestions.Length; i++)
            {
                all = true;

                if (CT.GetQuestionByID(i).Type == "QuestionChoice")
                {
                    for (int j = 0; j < CT.GetQuestionByID(i).Answers.Count; j++)
                    {
                        if (CT.GetQuestionByID(i).Answers[j].Correct != this.answeredQuestions[i].Answers[j].Correct)
                        {
                            all = false;
                        }
                    }

                    if (all == true)
                        data.Add(CT.GetQuestionByID(i));
                }
                else if (CT.GetQuestionByID(i).Type == "QuestionGetValue")
                {
                    if (CT.GetQuestionByID(i).Answer.CorrectValue == this.answeredQuestions[i].Answer.CorrectValue)
                    {
                        data.Add(CT.GetQuestionByID(i));
                    }
                }
            }

            return data;
        }

        public new Question GetQuestionByID(int _id)
        {
            return CT.GetCurrentQuestionByID(_id);
        }

        public Question GetCurrentQuestion()
        {
            return CT.GetQuestionByID(_currentQuestionID);
        }

        public new List<ChoiceAnswer> GetListOfAnswers(int _id)
        {
            return CT.GetListOfAnswers(_id);
        }

        public new GetValueAnswer GetValueAnswer(int _id)
        {
            return CT.GetValueAnswer(_id);
        }

        public new int NumberOfQuestions
        {
            get
            {
                return CT.NumberOfQuestions;
            }
        }

        public int CurrentQuestionID
        {
            get
            {
                return _currentQuestionID;
            }

            set
            {
                _currentQuestionID = value;
            }
        }

        public bool[] AnsweredID
        {
            get
            {
                return answeredID;
            }
        }

        public Question[] AnsweredQuestions
        {
            get
            {
                return answeredQuestions;
            }
        }

        public int ControlTaskID
        {
            get
            {
                return CT.ID;
            }
        }
    }
}
