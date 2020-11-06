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
    public partial class ControlTasksForm : Form
    {
        List<ControlTask> tests;
        FileHandler FH = new FileHandler();
        User _user;

        List<SpentTimeOnLecture> times;

        int[] lectureIDS;

        public ControlTasksForm(User u)
        {
            InitializeComponent();

            _user = u;
        }

        private void ControlTasksForm_Load(object sender, EventArgs e)
        {
            groupBox1.Visible = false;

            button2.Visible = !(_user is Student);

            button1.Visible = !(_user is Teacher);

            if (_user is Teacher)
            {
                button1.Text = "View Test";
            }

            times = FH.SelectTimesByUsername(_user.Username);

            lectureIDS = new int[times.Count];

            for (int i = 0; i < lectureIDS.Length; i++)
                lectureIDS[i] = times[i].LectureID;

            LoadTests();
            this.WindowState = FormWindowState.Maximized;
        }

        private void LoadTests()
        {
            dataGridView1.Rows.Clear();

            tests = FH.ReadControlTasksFromFile();

            label1.Text = "Total number of tests " + tests.Count;

            for (int i = 0; i < tests.Count; i++)
            {
                dataGridView1.Rows.Add(tests[i].ID, tests[i].LectureID, tests[i].Name, tests[i].SecondsToPass);
            }

            if (_user is Student)
            {
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (lectureIDS.Contains(Convert.ToInt32(row.Cells[1].Value.ToString())) == false)
                    {
                        row.DefaultCellStyle.BackColor = Color.Red;
                    }
                }
            }

            foreach (DataGridViewColumn column in dataGridView1.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }

            if (_user is Student)
                CheckTestsByLectures();
        }

        private void CheckTestsByLectures()
        {
            button1.Visible = !(dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].DefaultCellStyle.BackColor == Color.Red);

            button3.Visible = !button1.Visible;

            label2.Visible = button3.Visible;
        }

        private void button2_Click(object sender, EventArgs e)
        { // edit
            AddTest CTF = new AddTest(tests[dataGridView1.CurrentCell.RowIndex] as AddEditControlTask);

            FormHandler.OpenAnotherFormAsDialog(CTF);

            LoadTests();
        }

        private void button1_Click(object sender, EventArgs e)
        { // pass test or view it
            if (_user is Student)
            {
                TestForm AT = new TestForm(_user as Student, tests[dataGridView1.CurrentCell.RowIndex]);

                FormHandler.OpenAnotherFormAsDialogAndHide(this, AT);
            }
            else
            {
                groupBox1.Visible = true;
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void dataGridView1_MouseClick(object sender, MouseEventArgs e)
        {
            CheckTestsByLectures();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].DefaultCellStyle.BackColor == Color.Red && dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex - 1].DefaultCellStyle.BackColor != Color.Red)
            {
                LectureMaterial LM = new LectureMaterial(FH.SelectLectureByID(Convert.ToInt32(dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[1].Value.ToString())), _user as Student);

                FormHandler.OpenAnotherFormAsDialogAndHide(this, LM);
            }
            else
            {
                MessageBox.Show("You need to read previous lecture first.");
            }
        }

        private void dataGridView1_CurrentCellChanged(object sender, EventArgs e)
        {
            if (_user is Student)
                CheckTestsByLectures();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == _user.Password)
            {
                TestForm AT = new TestForm(tests[dataGridView1.CurrentCell.RowIndex]);

                groupBox1.Visible = false;

                FormHandler.OpenAnotherForm(AT);
            }
            else
            {
                MessageBox.Show("Password is incorrert.");

                groupBox1.Visible = false;
            }

            textBox1.Text = string.Empty;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            groupBox1.Visible = false;
        }
    }
}
