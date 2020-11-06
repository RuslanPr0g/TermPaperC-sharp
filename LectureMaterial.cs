using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Term_Paper_Rudenko
{
    public partial class LectureMaterial : Form
    {
        FileHandler FH = new FileHandler();

        Lecture lecture;

        Student _student;

        int currentPortion = 0;

        Timer MyTimer = new Timer();

        private SpentTimeOnLecture _time;

        private int secondsPassed = 0;

        TimeSpan timeSpan;

        public LectureMaterial(Lecture l)
        {
            InitializeComponent();

            lecture = l;

            _student = null;
        }

        public LectureMaterial(Lecture l, Student student)
        {
            InitializeComponent();

            lecture = l;

            _student = student;
        }

        public void StartLecture()
        {
            MyTimer.Start();
        }

        private void SaveTime()
        {
            List<SpentTimeOnLecture> t = FH.ReadTimesSpentOnLecturesFromFile();

            if (_time.Seconds < secondsPassed)
                _time.Seconds = secondsPassed;

            for (int i = 0; i < t.Count; i++)
            {
                if (t[i].Username == _time.Username && t[i].LectureID == _time.LectureID)
                {
                    t[i].Seconds = _time.Seconds;

                    break;
                }
            }

            FH.WriteTimesSpentOnLectureToFile(t);
        }

        private void MyTimer_Tick(object sender, EventArgs e)
        {
            timeSpan = TimeSpan.FromSeconds(secondsPassed);

            label3.Text = string.Format("Time passed: {0:D2} minutes {1:D2} seconds",
                            timeSpan.Minutes,
                            timeSpan.Seconds);

            secondsPassed++;
        }

        private void f_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (_student != null)
                SaveTime();
        }

        private void LectureMaterial_Load(object sender, EventArgs e)
        {
            if (_student != null)
            {
                _time = FH.SelectTimeByLectureIDAndUsername(lecture.ID, _student.Username);

                if (_time != null)
                {
                    this.secondsPassed = _time.Seconds;
                }
                else
                {
                    _time = new SpentTimeOnLecture(_student.Username, lecture.ID, secondsPassed);

                    FH.AppendTimeSpentOnLectureToFile(_time);
                }
            }
            else
            {
                _time = null;
            }

            MyTimer.Interval = (1000);
            MyTimer.Tick += new EventHandler(MyTimer_Tick);

            label1.Text = lecture.GetTitle();

            ShowPortion();

            DisplayCurrentPortion();

            if (_student != null)
            {
                StartLecture();

                label3.Visible = true;
            }

            this.FormClosed += new FormClosedEventHandler(f_FormClosed);
            this.WindowState = FormWindowState.Maximized;
        }

        private void DisplayCurrentPortion()
        {
            label2.Text = currentPortion + 1 + "/" + lecture.GetNumberOfPortions();
        }

        private void DisplayPassingTest()
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (currentPortion + 1 == lecture.GetNumberOfPortions() - 1 && _student != null)
            {
                PassTestButton.Visible = true;
            }

            if (currentPortion + 1 <= lecture.GetNumberOfPortions() - 1)
            {
                currentPortion++;
            }

            DisplayCurrentPortion();

            ShowPortion();

            DisplayPassingTest();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (currentPortion - 1 >= 0)
            {
                currentPortion--;
            }

            DisplayCurrentPortion();

            ShowPortion();
        }

        private void ShowPortion()
        {
            richTextBox1.Text = lecture.GetPortion(currentPortion);
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void test_FormClosed(object sender, FormClosedEventArgs e)
        {
            MyTimer.Start();
        }

        private void PassTestButton_Click(object sender, EventArgs e)
        {
            if (FH.SelectControlTaskByLectureID(lecture.ID) == null)
            {
                MessageBox.Show("Test does not exist yet.");
                return;
            }

            MyTimer.Stop();

            TestForm AT = new TestForm(_student, lecture.ID);

            AT.FormClosed += new FormClosedEventHandler(test_FormClosed);

            FormHandler.OpenAnotherFormAsDialog(AT);
        }
    }
}
