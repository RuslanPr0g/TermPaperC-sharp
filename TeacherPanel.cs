using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Term_Paper_Rudenko
{
    public partial class TeacherPanel : Form
    {
        Teacher teacher;
        FileHandler FH = new FileHandler();

        TimeSpan timeSpan;

        public event EventHandler OnPanelLogOut;

        public TeacherPanel()
        {
            teacher = new Teacher();

            InitializeComponent();
        }

        public TeacherPanel(Teacher t)
        {
            teacher = t;

            InitializeComponent();
        }

        private void TeacherPanel_Load(object sender, EventArgs e)
        {
            Form1.ModernLayout(this);

            label1.Text = "Welcome " + teacher.Username + "!";

            label6.Visible = FH.SelectPasswordRecoveryByUsername(teacher.Username) == null ? true : false;

            Total();

            AddTest.OnTestAdded += Totals;
            TestForm.OnGradeGot += Totals;
            OnPanelLogOut += Panel_LogOut;

            this.WindowState = FormWindowState.Maximized;
        }

        private void UpdateTeacher(Teacher teacher)
        {
            this.teacher = teacher;
        }

        private void Total()
        {
            label2.Text = "Total lectures: " + FH.ReadLecturesFromFile().Count;
            label3.Text = "Total tests: " + FH.ReadControlTasksFromFile().Count;
            label4.Text = "Total grades: " + FH.ReadGradesFromFile().Count;

            timeSpan = TimeSpan.FromSeconds(FH.ReadTimesSpentOnLecturesFromFile().Sum(x => x.Seconds));

            label5.Text = string.Format("Total time spent on lectures: {0:D2} hours {1:D2} minutes {2:D2} seconds",
                            timeSpan.Hours,
                            timeSpan.Minutes,
                            timeSpan.Seconds);
        }

        private void Totals(object sender, EventArgs e)
        {
            Total();
        }

        public void Panel_LogOut(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Authorization a = new Authorization();

            OnPanelLogOut?.Invoke(this, EventArgs.Empty);

            FormHandler.OpenAnotherFormWithDispose(this, a);
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            ListOfUsers LOU = new ListOfUsers("Student");

            FormHandler.OpenAnotherFormAsDialog(LOU);
        }

        private void button1_Click(object sender, EventArgs e)
        { // add lecture
            AddLecture AL = new AddLecture("Add");

            AL.OnLectureAdded += Totals;

            FormHandler.OpenAnotherFormAsDialog(AL);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            LecturesList LL = new LecturesList(teacher);

            FormHandler.OpenAnotherFormAsDialog(LL);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            AddTest AT = new AddTest();

            FormHandler.OpenAnotherFormAsDialog(AT);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            ControlTasksForm CTF = new ControlTasksForm(teacher);

            FormHandler.OpenAnotherFormAsDialog(CTF);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            ListOfGrades CTF = new ListOfGrades();

            FormHandler.OpenAnotherForm(CTF);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            UserSettings US = new UserSettings(teacher);

            US.OnTeacherSettingsChanged += UpdateTeacher;

            FormHandler.OpenAnotherFormAsDialog(US);
        }
    }
}
