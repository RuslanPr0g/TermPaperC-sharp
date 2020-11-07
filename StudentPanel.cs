using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Term_Paper_Rudenko
{
    public partial class StudentPanel : Form
    {
        Student student;

        FileHandler FH = new FileHandler();

        TimeSpan timeSpan;

        public event EventHandler OnPanelLogOut;

        public StudentPanel()
        {
            student = new Student();

            InitializeComponent();
        }

        public StudentPanel(Student s)
        {
            student = s;

            InitializeComponent();
        }

        private void StudentPanel_Load(object sender, EventArgs e)
        {
            label1.Text = "Welcome " + student.Username + "!";

            label4.Visible = FH.SelectPasswordRecoveryByUsername(student.Username) == null ? true : false;

            Total();

            LectureMaterial.OnLectureRead += Totals;
            TestForm.OnGradeGot += Totals;
            OnPanelLogOut += Panel_LogOut;

            this.WindowState = FormWindowState.Maximized;
        }

        private void UpdateStudent(Student student)
        {
            this.student = student;
        }

        private void Total()
        {
            timeSpan = TimeSpan.FromSeconds(SpentTimeOnLecture.AverageTimeSpentOnLectures(student.Username, FH.ReadTimesSpentOnLecturesFromFile()));

            label2.Text = string.Format("Average Time Spent On Lectures: {0:D2} hours {1:D2} minutes {2:D2} seconds",
                            timeSpan.Hours,
                            timeSpan.Minutes,
                            timeSpan.Seconds);

            label3.Text = "Average Grade For Tests: " + Grade.CalculateAverage(FH.SelectGradesByUsername(student.Username));
        }

        public void Totals(object sender, EventArgs e)
        {
            Total();
        }

        public void Panel_LogOut(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Authorization a = new Authorization();

            OnPanelLogOut?.Invoke(this, EventArgs.Empty);

            FormHandler.OpenAnotherFormWithDispose(this, a);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            LecturesList LL = new LecturesList(student);

            FormHandler.OpenAnotherFormAsDialog(LL);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            StudentGrades SG = new StudentGrades(student);

            FormHandler.OpenAnotherFormAsDialog(SG);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            ControlTasksForm CTF = new ControlTasksForm(student);

            FormHandler.OpenAnotherFormAsDialog(CTF);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            string info = string.Empty;

            List<Grade> grades = FH.SelectGradesByUsername(student.Username);

            foreach (Grade grade in grades)
            {
                info += FH.SelectControlTaskByID(grade.ControlTask).Name + " : " + grade.Grade5 + " : " + grade.Grade100 + ".\n";
            }

            MessageBox.Show(info);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            UserSettings US = new UserSettings(student);

            US.OnStudentSettingsChanged += UpdateStudent;

            FormHandler.OpenAnotherFormAsDialog(US);
        }
    }
}
