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
    public partial class StudentGrades : Form
    {
        FileHandler FH = new FileHandler();

        private Student _student;

        Lecture _lecture;
        ControlTask _task;

        public StudentGrades(Student s)
        {
            InitializeComponent();

            _student = s;
        }

        private void StudentGrades_Load(object sender, EventArgs e)
        {
            Form1.StyleButtons(this);

            label1.Text = "Grade For Student " + _student.Username;

            List<Grade> grades = FH.ReadGradesFromFile();

            for (int i = 0; i < grades.Count; i++)
            {
                if(grades[i].StudentUsername == _student.Username)
                {
                    _task = FH.ReadControlTaskFromFile(grades[i].ControlTask);
                    _lecture = FH.ReadLectureFromFile(_task.LectureID);

                    dataGridView1.Rows.Add(grades[i].ID, _lecture.Name, grades[i].Grade5 , grades[i].Grade100);
                }
            }
            this.WindowState = FormWindowState.Maximized;
        }
    }
}
