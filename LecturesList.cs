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
    public partial class LecturesList : Form
    {
        List<Lecture> lectures;
        FileHandler FH = new FileHandler();
        User _user;

        public LecturesList()
        {
            InitializeComponent();

            _user = null;
        }

        public LecturesList(User u)
        {
            InitializeComponent();
            _user = u;
        }

        private void LecturesList_Load(object sender, EventArgs e)
        {
            Form1.ModernLayout(this);

            lectures = FH.ReadLecturesFromFile();

            foreach (Lecture lecture in lectures)
            {
                dataGridView1.Rows.Add(lecture.ID, lecture.Name, lecture.Topic);
            }

            if (_user is Student)
            {
                FilterLecturesByTopics();

                button2.Visible = false;
            }

            foreach (DataGridViewColumn column in dataGridView1.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }

            label1.Text = "Total number of lectures " + lectures.Count;

            if (_user == null)
            {
                button1.Visible = false;
                button2.Visible = false;
            }

            CheckTestsByLectures();
            this.WindowState = FormWindowState.Maximized;
        }

        private void FilterLecturesByTopics()
        {
            List<SpentTimeOnLecture> times = FH.SelectTimesByUsername(_user.Username);

            int[] lectureIDS = new int[times.Count];

            List<string> topics = new List<string>();
            List<string> usedTopics = new List<string>();

            for (int J = 0; J < lectureIDS.Length; J++)
                lectureIDS[J] = times[J].LectureID;

            int i = 0;

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Cells[0].Value.ToString() != "0")
                {
                    if (lectureIDS.Contains(Convert.ToInt32(row.Cells[0].Value.ToString())) == true || lectureIDS.Contains(Convert.ToInt32(row.Cells[0].Value.ToString()) - 1) == true || topics.Contains(row.Cells[2].Value.ToString()) == true)
                    {
                        if (topics.Contains(row.Cells[2].Value.ToString()) == true && lectureIDS.Contains(Convert.ToInt32(row.Cells[0].Value.ToString())) == false && lectureIDS.Contains(Convert.ToInt32(row.Cells[0].Value.ToString()) - 1) == false)
                        {
                            row.DefaultCellStyle.BackColor = Color.Yellow;

                            if (usedTopics.Contains(row.Cells[2].Value.ToString()) == false)
                            {
                                usedTopics.Add(row.Cells[2].Value.ToString());
                            }
                            else
                            {
                                row.DefaultCellStyle.BackColor = Color.Red;
                            }
                        }

                        topics.Add(row.Cells[2].Value.ToString());
                    }
                    else
                    {
                        row.DefaultCellStyle.BackColor = Color.Red;
                    }
                }
                else
                {
                    topics.Add(row.Cells[2].Value.ToString());
                }

                i++;
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void CheckTestsByLectures()
        {
            button1.Visible = !(dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].DefaultCellStyle.BackColor == Color.Red);

            label2.Visible = !button1.Visible;

            label3.Visible = (dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].DefaultCellStyle.BackColor == Color.Yellow);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (_user is Student)
            {
                LectureMaterial LM = new LectureMaterial(lectures[dataGridView1.CurrentCell.RowIndex], _user as Student);

                FormHandler.OpenAnotherFormAsDialogAndHide(this, LM);
            }
            else
            {
                LectureMaterial LM = new LectureMaterial(lectures[dataGridView1.CurrentCell.RowIndex]);

                FormHandler.OpenAnotherFormAsDialogAndHide(this, LM);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            AddLecture AL = new AddLecture("Edit", lectures[dataGridView1.CurrentCell.RowIndex]);

            FormHandler.OpenAnotherFormAsDialogAndHide(this, AL);
        }

        private void dataGridView1_MouseClick(object sender, MouseEventArgs e)
        {
            CheckTestsByLectures();
        }

        private void dataGridView1_CurrentCellChanged(object sender, EventArgs e)
        {
            CheckTestsByLectures();
        }
    }
}
