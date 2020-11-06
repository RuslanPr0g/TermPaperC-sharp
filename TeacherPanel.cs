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
    public partial class TeacherPanel : Form
    {
        Teacher teacher;
        FileHandler FH = new FileHandler();

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
            label1.Text = "Welcome " + teacher.Username + "!";

            Totals();

            this.WindowState = FormWindowState.Maximized;
        }

        private void Totals()
        {
            label2.Text = "Total lectures: " + FH.ReadLecturesFromFile().Count;
            label3.Text = "Total tests: " + FH.ReadControlTasksFromFile().Count;
            label4.Text = "Total grades: " + FH.ReadGradesFromFile().Count;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Authorization a = new Authorization();

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
    }
}
