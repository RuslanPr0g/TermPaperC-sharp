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
    public partial class ListOfGrades : Form
    {
        List<Grade> grades;
        FileHandler FH = new FileHandler();

        public ListOfGrades()
        {
            InitializeComponent();
        }

        private void ListOfGrades_Load(object sender, EventArgs e)
        {
            grades = FH.ReadGradesFromFile();

            SetupData();
            this.WindowState = FormWindowState.Maximized;
        }

        private void SetupData()
        {
            foreach (Grade grade in grades)
            {
                dataGridView1.Rows.Add(grade.ID, grade.ControlTask, grade.StudentUsername, Grade.CalculateAverage(FH.SelectGradesByUsername(grade.StudentUsername)), grade.GetAs5(Grade.CalculateAverage(FH.SelectGradesByUsername(grade.StudentUsername))));
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            StudentGrades SG = new StudentGrades(FH.SelectStudentByUsername(dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[2].Value.ToString()));

            FormHandler.OpenAnotherFormAsDialog(SG);
        }
    }
}
