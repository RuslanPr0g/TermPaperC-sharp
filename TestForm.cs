using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Term_Paper_Rudenko
{
    public partial class TestForm : Form
    {
        PassControlTask test;
        ControlTask task = null;

        FileHandler FH = new FileHandler();

        Student _student;

        private bool passAgain = false;
        private int lectID = -1;

        Timer MyTimer = new Timer();

        private int secondsPassed = 0;

        TimeSpan timeSpan;

        public static event EventHandler OnGradeGot;

        public TestForm(ControlTask ct)
        {
            InitializeComponent();

            _student = null;
            lectID = ct.LectureID;
            task = ct;
        }

        public TestForm(Student student, int lid)
        {
            InitializeComponent();

            _student = student;
            lectID = lid;
        }

        public TestForm(Student student, ControlTask ct)
        {
            InitializeComponent();

            _student = student;
            lectID = ct.LectureID;
            task = ct;
        }

        public void StartTest()
        {
            MyTimer.Start();
        }

        private void MyTimer_Tick(object sender, EventArgs e)
        {
            timeSpan = TimeSpan.FromSeconds(task.SecondsToPass - secondsPassed);

            TimeRemainedLabel.Text = string.Format("Time remained: {0:D2} minutes {1:D2} seconds",
                            timeSpan.Minutes,
                            timeSpan.Seconds);

            if (test.SecondsToPass - secondsPassed == 0)
            {
                MyTimer.Stop();

                MessageBox.Show("You have not passed this test. Time elapsed.");

                this.Close();
                this.Dispose();
            }

            secondsPassed++;
        }

        private void TestForm_Load(object sender, EventArgs e)
        {
            // view test

            Form1.StyleButtons(this);

            if (_student == null)
            {
                QuestionCountLabel.Enabled = false;
                c.Enabled = false;
                TimeRemainedLabel.Enabled = false;
                FinishButton.Visible = false;
                AnswerButton.Enabled = false;
                AnswersBox.Enabled = false;

                if (task == null)
                    task = FH.SelectControlTaskByLectureID(lectID);

                test = new PassControlTask(task);

                CurrentQuestionLabel();

                ViewTest();

                return;
            }

            // pass test

            MyTimer.Interval = (1000);
            MyTimer.Tick += new EventHandler(MyTimer_Tick);

            List<Grade> grades = FH.ReadGradesFromFile();

            if (task == null)
                task = FH.SelectControlTaskByLectureID(lectID);

            test = new PassControlTask(task);

            foreach (Grade g in grades)
            {
                if (g.StudentUsername == _student.Username && g.ControlTask == test.ControlTaskID)
                {
                    MessageBox.Show("This test was passed, your mark is " + g.Grade5);

                    if (g.Fail == true)
                    {
                        DialogResult dr = MessageBox.Show("Do you want to pass it again?", "Important", MessageBoxButtons.YesNo);
                        if (dr == DialogResult.Yes)
                        {
                            passAgain = true;

                            break;
                        }
                        else
                        {
                            this.Close();

                            return;
                        }
                    }
                    else
                    {
                        this.Close();

                        return;
                    }
                }
            }

            bool showDisclaimer = true;

            if (showDisclaimer == true)
                ShowDisclaimer();
            else
                StartPassing();

            FinishButton.Visible = false;

            CurrentQuestionLabel();
            AlreadyAnsweredLabel();
            this.WindowState = FormWindowState.Maximized;
        }

        private void ShowDisclaimer()
        {
            MainMenuPanel.Visible = false;

            panelShowMessage.Location = new Point(12, 12);
            // draw horizontal line
            label1.AutoSize = false;
            label1.Width = 900;
            label1.Height = 2;
            label1.BorderStyle = BorderStyle.Fixed3D;
        }

        private void StartPassing()
        {
            panelShowMessage.Visible = false;
            MainMenuPanel.Visible = true;

            ShowQuestion(test.GetQuestionByID(0));

            StartTest();
        }

        private void ViewTest()
        {
            panelShowMessage.Visible = false;
            MainMenuPanel.Visible = true;

            ShowQuestion(test.GetQuestionByID(0));
        }

        private void button1_Click(object sender, EventArgs e)
        {
            StartPassing();
        }

        private void MainMenuPanel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void NextQuestionButton_Click(object sender, EventArgs e)
        {
            NextQuestion();

            CurrentQuestionLabel();
        }

        private void PreviousQuestionButton_Click(object sender, EventArgs e)
        {
            PrevQuestion();

            CurrentQuestionLabel();
        }

        private void AnswerButton_Click(object sender, EventArgs e)
        {
            AnswerToQuestion();
        }

        public void NextQuestion()
        {
            Question q = test.GetNext();

            if (q != null)
                ShowQuestion(q);

            if (test.CurrentQuestionID == test.NumberOfQuestions - 1 && test.AllAnswered() == true)
            {
                FinishButton.Visible = true;
            }
        }

        public void PrevQuestion()
        {
            FinishButton.Visible = false;

            Question q = test.GetPrev();

            if (q != null)
                ShowQuestion(q);
        }

        public void AnswerToQuestion()
        {
            Question q = test.GetCurrentQuestion();

            if (q.Type == "QuestionChoice")
            {
                List<ChoiceAnswer> ca = new List<ChoiceAnswer>();

                bool wasAtLeastOne = false;

                int i = 0;

                if (AnswersBox.Controls[0].GetType().Name == "CheckBox")
                {
                    foreach (CheckBox item in AnswersBox.Controls)
                    {
                        if (item.Checked == true) wasAtLeastOne = true;

                        ca.Add(new ChoiceAnswer(item.Text, item.Checked));

                        i++;
                    }
                }
                else
                {
                    foreach (RadioButton item in AnswersBox.Controls)
                    {
                        if (item.Checked == true) wasAtLeastOne = true;

                        ca.Add(new ChoiceAnswer(item.Text, item.Checked));

                        i++;
                    }
                }

                if (wasAtLeastOne == false)
                {
                    MessageBox.Show("Please, answer to at least one of the questions.");
                    return;
                }

                try
                {
                    test.AnswerToTheQuestion(test.CurrentQuestionID, ca);
                }
                catch (ArgumentNullException)
                {
                    MessageBox.Show("Please, answer to at least one of the questions.");
                    throw;
                }
            }
            else if (q.Type == "QuestionGetValue")
            {
                GetValueAnswer gva = new GetValueAnswer();

                if (AnswersBox.Controls[AnswersBox.Controls.Count - 1].Text == string.Empty)
                {
                    MessageBox.Show("To answer, please input your answer to the text box.");
                    return;
                }

                gva.CorrectValue = AnswersBox.Controls[AnswersBox.Controls.Count - 1].Text;

                try
                {
                    test.AnswerToTheQuestion(test.CurrentQuestionID, gva);
                }
                catch (ArgumentNullException)
                {
                    MessageBox.Show("To answer, please input your answer to the text box.");
                    throw;
                }
            }

            if (test.CurrentQuestionID == test.NumberOfQuestions - 1 && test.AllAnswered() == false)
            {
                MessageBox.Show("Make sure, that you have answered to all the questions.");
            }

            AlreadyAnsweredLabel();

            NextQuestion();
        }

        public void PlaceButtonBase(ButtonBase b, ChoiceAnswer a, int startPosX, int startPosY, int space, int i)
        {
            if (i == 0)
                b.Location = new Point(startPosX, startPosY + space);
            else
                b.Location = new Point(startPosX, AnswersBox.Controls[i - 1].Location.Y + AnswersBox.Controls[i - 1].Size.Height + space);

            b.Text = a.Name;
            b.Width = AnswersBox.Width;
        }

        public void ShowAnswers(List<ChoiceAnswer> a)
        {
            AnswersBox.Controls.Clear();

            int startPosY = 10;
            int space = 25;
            int startPosX = 10;

            if (ControlTask.GetTypeOfChoice(a) == QuestionTypes.MultiChoice)
            {
                for (int i = 0; i < a.Count; i++)
                {
                    CheckBox checkbox = new CheckBox();

                    PlaceButtonBase(checkbox, a[i], startPosX, startPosY, space, i);

                    if (_student == null)
                        checkbox.Checked = a[i].Correct;

                    if (test.AnsweredID[test.CurrentQuestionID] == true)
                    {
                        checkbox.Checked = test.AnsweredQuestions[test.CurrentQuestionID].Answers[i].Correct;
                    }

                    AnswersBox.Controls.Add(checkbox);
                }
            }
            else
            {
                for (int i = 0; i < a.Count; i++)
                {
                    RadioButton radio = new RadioButton();

                    PlaceButtonBase(radio, a[i], startPosX, startPosY, space, i);

                    if (_student == null)
                        radio.Checked = a[i].Correct;

                    if (test.AnsweredID[test.CurrentQuestionID] == true)
                    {
                        radio.Checked = test.AnsweredQuestions[test.CurrentQuestionID].Answers[i].Correct;
                    }

                    AnswersBox.Controls.Add(radio);
                }
            }
        }

        public void ShowAnswer(GetValueAnswer a)
        {
            AnswersBox.Controls.Clear();

            int startPosY = 100;
            int startPosX = 20;

            TextBox textbox = new TextBox();

            textbox.Location = new Point(startPosX, startPosY);

            if (_student == null)
                textbox.Text = a.CorrectValue;

            if (test.AnsweredID[test.CurrentQuestionID] == true)
            {
                textbox.Text = test.AnsweredQuestions[test.CurrentQuestionID].Answer.CorrectValue;
            }

            AnswersBox.Controls.Add(textbox);
        }

        public void ShowQuestion(Question q)
        {
            QuestionPanel_TitleLabel.Text = q.Name;

            if (q.Type == "QuestionChoice")
                ShowAnswers(q.Answers);
            else if (q.Type == "QuestionGetValue")
            {
                ShowAnswer(q.Answer);
            }
        }

        private void CurrentQuestionLabel()
        {
            QuestionCountLabel.Text = test.CurrentQuestionID + 1 + "/" + test.NumberOfQuestions;
        }

        private void AlreadyAnsweredLabel()
        {
            c.Text = test.CountAnswered() + "/" + test.NumberOfQuestions;

            CurrentQuestionLabel();
        }

        private void FinishButton_Click(object sender, EventArgs e)
        {
            Grade grade = test.FinishTest();

            if (grade == null) return;

            MyTimer.Stop();

            List<Grade> grades = FH.ReadGradesFromFile();

            grade.StudentUsername = _student.Username;
            grade.ControlTask = test.ControlTaskID;

            if (passAgain == true)
            {
                List<Grade> withNewGrade = new List<Grade>();

                for (int i = 0; i < grades.Count; i++)
                {
                    if (grades[i].StudentUsername != _student.Username || grades[i].ControlTask != test.ControlTaskID)
                    {
                        withNewGrade.Add(grades[i]);
                    }
                    else
                    {
                        withNewGrade.Add(grade);
                        grade.ID = grade.ID;
                    }
                }

                FH.ReWriteGradeToFile(withNewGrade);
            }
            else
            {
                grade.ID = grades.Count;

                FH.WriteGradeToFile(grade);
            }

            OnGradeGot?.Invoke(this, EventArgs.Empty);

            MessageBox.Show("Test Done! Your grade is " + grade.Grade5);

            this.Close();
        }
    }
}
