using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Term_Paper_Rudenko
{
    public partial class AddLecture : Form
    {
        private Lecture lecture = new Lecture();
        FileHandler FH = new FileHandler();
        List<Lecture> lectures;

        private string mode = "";

        bool ENOUGH = false;

        int currentPortion = 0;

        public event EventHandler OnLectureAdded;
        public event EventHandler OnLectureAdding;
        public event EventHandler OnLectureEdited;
        public event EventHandler OnLectureEditing;

        public AddLecture(string MODE)
        {
            InitializeComponent();

            mode = MODE;

            OnLectureAdding += Lecture_Add;
            OnLectureEditing += Lecture_Edit;
        }

        public AddLecture(string MODE, Lecture l)
        {
            InitializeComponent();

            lecture = l;

            mode = MODE;

            OnLectureAdding += Lecture_Add;
            OnLectureEditing += Lecture_Edit;
        }

        private void PushInfoToLecture()
        {
            lecture.Name = textBox1.Text;
            lecture.Topic = textBox2.Text;

            lecture.ID = lectures.Count;

            lecture.FileName = lecture.ID + ".txt";
        }

        public void Lecture_Add(object sender, EventArgs e)
        {
            PushInfoToLecture();

            FH.WriteLectureToFile(lecture);

            OnLectureAdded?.Invoke(this, EventArgs.Empty);

            MessageBox.Show("Lecture " + lecture.Name + " added.");

            this.Close();
        }

        public void Lecture_Edit(object sender, EventArgs e)
        {
            PushInfoToLecture();

            FH.EditLectureInFile(lecture);

            OnLectureEdited?.Invoke(this, EventArgs.Empty);

            MessageBox.Show("Lecture " + lecture.Name + " edited.");

            this.Close();
        }

        public void Lecture_Read(object sender, EventArgs e)
        {

        }

        private void AddLecture_Load(object sender, EventArgs e)
        {
            Form1.ModernLayout(this);

            if (mode == "Add")
            {
                lectures = FH.ReadLecturesFromFile();
            }
            else
            {
                textBox1.Text = lecture.Name;
                textBox2.Text = lecture.Topic;
                ShowPortion();
            }

            label4.Text = "You have added " + lecture.GetNumberOfPortions() + " portions.";
            button2.Text = mode + " Lecture";
            ShowCurrent();
            this.WindowState = FormWindowState.Maximized;
        }

        private void ShowPortion()
        {
            richTextBox1.Text = lecture.GetPortion(currentPortion);
            ShowCurrent();
        }

        private void ShowCurrent()
        {
            label5.Text = "You are currently on " + (currentPortion + 1) + " of " + lecture.GetNumberOfPortions();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || textBox2.Text == "")
            {
                MessageBox.Show("Please fill up all the fields.");
                return;
            }

            if (lecture.GetNumberOfPortions() == 0)
            {
                MessageBox.Show("Please create at least one portion.");
                return;
            }

            if (mode == "Add")
            {
                OnLectureAdding?.Invoke(this, EventArgs.Empty);
            }
            else
            {
                OnLectureEditing?.Invoke(this, EventArgs.Empty);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            lecture.AddPortion(richTextBox1.Text);
            richTextBox1.Text = "";
            currentPortion = lecture.GetNumberOfPortions() - 1;
            label4.Text = "You have added " + lecture.GetNumberOfPortions() + " portions.";
            ShowCurrent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (currentPortion - 1 >= 0)
            {
                currentPortion--;
            }

            if (mode == "Add")
            {
                button1.Enabled = false;
            }

            ShowPortion();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (currentPortion + 1 <= lecture.GetNumberOfPortions() - 1)
            {
                currentPortion++;
            }

            if (currentPortion == lecture.GetNumberOfPortions() - 1 && mode == "Add")
            {
                button1.Enabled = true;
            }

            ShowPortion();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            lecture.RemovePortion(currentPortion);
            currentPortion = lecture.GetNumberOfPortions() - 1;
            label4.Text = "You have added " + lecture.GetNumberOfPortions() + " portions.";
            ShowPortion();
            button1.Enabled = true;
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            if (mode == "Edit")
            {
                lecture.ModifyPortion(currentPortion, richTextBox1.Text);
            }
        }

        private void PushFileDataAsPortions(string a)
        {
            if (a == string.Empty || a.Trim() == string.Empty) return;

            string temp = a;

            int length = 50;

            richTextBox1.Text = "";

            for (int i = lecture.GetNumberOfPortions(); i >= 0; i--)
            {
                try
                {
                    lecture.RemovePortion(i);
                }
                catch
                {
                    continue;
                }
            }

            while (temp != "")
            {
                if (ENOUGH == false)
                {
                    try
                    {
                        richTextBox1.Text += temp.Substring(0, length);
                        temp = temp.Substring(length);
                    }
                    catch
                    {
                        richTextBox1.Text += temp.Substring(0);
                        temp = string.Empty;
                    }
                }
                else
                {
                    lecture.AddPortion(richTextBox1.Text);
                    richTextBox1.Text = "";
                    currentPortion = lecture.GetNumberOfPortions() - 1;
                    label4.Text = "You have added " + lecture.GetNumberOfPortions() + " portions.";
                    ShowCurrent();
                }
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            string FILE_DATA = FileHandler.BrowseForFile();

            PushFileDataAsPortions(FILE_DATA);
        }

        private void richTextBox1_ContentsResized(object sender, ContentsResizedEventArgs e)
        {
            if (e.NewRectangle.Height >= richTextBox1.Height)
            {
                ENOUGH = true;
            }
            else
            {
                ENOUGH = false;
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < lectures.Count; i++)
            {
                if (lectures[i].ID == Convert.ToInt32(ID.Text))
                {
                    lecture = lectures[i];

                    textBox1.Text = lecture.Name;
                    textBox2.Text = lecture.Topic;
                    ShowPortion();
                    label4.Text = "You have added " + lecture.GetNumberOfPortions() + " portions.";
                    button2.Text = mode + " Lecture";
                    ShowCurrent();
                }
            }
        }
    }
}
