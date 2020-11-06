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
    public partial class ListOfUsers : Form
    {
        string user;

        FileHandler FH = new FileHandler();

        List<Student> Students;
        List<Teacher> Teachers;

        public ListOfUsers()
        {
            user = "";

            InitializeComponent();
        }

        public ListOfUsers(string u)
        {
            user = u;

            InitializeComponent();
        }

        private void ListOfUsers_Load(object sender, EventArgs e)
        {
            label1.Text = "List of " + user;

            switch (user)
            {
                case "Student":
                    Students = FH.ReadStudentsFromFile();

                    foreach (Student student in Students)
                    {
                        dataGridView1.Rows.Add(student.Username, student.Name, student.Lastname, student.Group);
                    }
                    break;
                case "Teacher":
                    Teachers = FH.ReadTeachersFromFile();

                    foreach (Teacher teacher in Teachers)
                    {
                        dataGridView1.Rows.Add(teacher.Username, teacher.Name, teacher.Lastname);
                    }

                    break;
            }
            this.WindowState = FormWindowState.Maximized;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            StudentGrades SG = new StudentGrades(Students[dataGridView1.CurrentCell.RowIndex]);

            FormHandler.OpenAnotherFormAsDialog(SG);
        }
    }
}
