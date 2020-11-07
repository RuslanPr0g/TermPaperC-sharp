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
    public partial class AddTest : Form
    {
        AddEditControlTask CT;
        FileHandler FH = new FileHandler();

        int currentID = 0;

        string mode = "Add";

        public static event EventHandler OnTestAdded;

        public AddTest()
        {
            InitializeComponent();

            CT = new AddEditControlTask();
        }

        public AddTest(AddEditControlTask ct)
        {
            InitializeComponent();

            mode = "Edit";

            CT = ct;
        }

        private void AddTest_Load(object sender, EventArgs e)
        {
            Form1.ModernLayout(this);

            if (mode == "Add")
            {
                HideShowGetValueFields(false);

                CT.ID = FH.ReadControlTasksFromFile().Count;

                button6.Text = "ADD CONTROL TASK";
            }

            textBox2.Text = CT.SecondsToPass.ToString();
            textBox3.Text = CT.LectureID.ToString();

            label2.Text = "You have added " + CT.NumberOfQuestions + " questions.";
            label7.Text = "You are on " + (currentID + 1) + " question of " + CT.NumberOfQuestions;

            if (mode == "Edit")
            {
                ShowCurrectQuestion(CT);

                button6.Text = "EDIT CONTROL TASK";
            }
            this.WindowState = FormWindowState.Maximized;
        }

        private void HideShowGetValueFields(bool show)
        {
            label4.Visible = show;
            textBox1.Visible = show;
        }

        private void HideShowChoiceFields(bool show)
        {
            dataGridView1.Visible = show;
            button4.Visible = show;
            button8.Visible = show;
        }

        private void ClearDataGridViewValues(DataGridView DGV, int columnIndex)
        {
            for (int i = 0; i < DGV.Rows.Count; i++)
            {
                if (columnIndex == 0)
                {
                    DGV.Rows[i].Cells[columnIndex].Value = false;
                }
                else
                {
                    DGV.Rows[i].Cells[columnIndex].Value = string.Empty;
                }
            }
        }

        private void TypeOfQuestionControl()
        {
            ClearDataGridViewValues(dataGridView1, 0);

            if (SingleChoice == true)
            {
                HideShowChoiceFields(true);
                HideShowGetValueFields(false);
            }
            else if (MultiChoice == true)
            {
                HideShowChoiceFields(true);
                HideShowGetValueFields(false);
            }
            else if (GetValue == true)
            {
                HideShowChoiceFields(false);
                HideShowGetValueFields(true);
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked == true)
            {
                TypeOfQuestionControl();
            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked == true)
            {
                TypeOfQuestionControl();
            }
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton3.Checked == true)
            {
                TypeOfQuestionControl();
            }
        }

        private int SecondsToPass
        {
            get
            {
                return Convert.ToInt32(textBox2.Text);
            }

            set
            {
                textBox2.Text = value.ToString();
            }
        }

        private int LectureID
        {
            get
            {
                return Convert.ToInt32(textBox3.Text);
            }

            set
            {
                textBox3.Text = value.ToString();
            }
        }

        private bool SingleChoice
        {
            get
            {
                return radioButton1.Checked;
            }

            set
            {
                radioButton1.Checked = value;
            }
        }

        private bool MultiChoice
        {
            get
            {
                return radioButton2.Checked;
            }

            set
            {
                radioButton2.Checked = value;
            }
        }

        private bool GetValue
        {
            get
            {
                return radioButton3.Checked;
            }

            set
            {
                radioButton3.Checked = value;
            }
        }

        private List<ChoiceAnswer> GetListOfAnswers()
        {
            List<ChoiceAnswer> answers = new List<ChoiceAnswer>();

            if (SingleChoice == true)
            {
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    answers.Add(new ChoiceAnswer(dataGridView1.Rows[i].Cells[1].Value.ToString(), Convert.ToBoolean(dataGridView1.Rows[i].Cells[0].Value.ToString())));
                }
            }
            else if (MultiChoice == true)
            {
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    answers.Add(new ChoiceAnswer(dataGridView1.Rows[i].Cells[1].Value.ToString(), Convert.ToBoolean(dataGridView1.Rows[i].Cells[0].Value.ToString())));
                }
            }

            return answers;
        }

        public GetValueAnswer GetAnswerGetValue()
        {
            GetValueAnswer answer;

            answer = new GetValueAnswer(textBox1.Text);

            return answer;
        }

        public void ReadOnlySwitcher(bool enabled, params RadioButton[] radioButtons)
        {
            foreach (RadioButton rb in radioButtons)
            {
                rb.Enabled = enabled;
            }
        }

        private string CheckTypeOfChoice(List<ChoiceAnswer> answers)
        {
            bool was = false;

            foreach (ChoiceAnswer c in answers)
            {
                if (c.Correct == true && was == true) // if checked more than one value
                {
                    return QuestionTypes.MultiChoice;
                }

                if (c.Correct == true)
                {
                    was = true;
                }
            }

            return QuestionTypes.SingleChoice;
        }

        public void ShowCurrectQuestion(ControlTask CT)
        {
            if (currentID == CT.NumberOfQuestions - 1)
            {
                button1.Enabled = true;
            }
            else
            {
                button1.Enabled = false;
            }

            SecondsToPass = CT.SecondsToPass;

            Question Q = CT.GetCurrentQuestionByID(currentID);

            richTextBox1.Text = Q.Name;

            if (Q is QuestionChoice)
            {
                List<ChoiceAnswer> answers = CT.GetListOfAnswers(currentID);

                if (CheckTypeOfChoice(answers) == QuestionTypes.SingleChoice)
                {
                    SingleChoice = true;
                }
                else
                {
                    MultiChoice = true;
                }

                dataGridView1.Rows.Clear();

                for (int i = 0; i < answers.Count; i++)
                {
                    dataGridView1.Rows.Add(answers[i].Correct, answers[i].Name);
                }
            }
            else
            {
                GetValue = true;
                GetValueAnswer answer = CT.GetValueAnswer(currentID);

                textBox1.Text = answer.CorrectValue;
            }

            label7.Text = "You are on " + (currentID + 1) + " question of " + CT.NumberOfQuestions;
        }

        private void ClearValues()
        {
            textBox1.Text = string.Empty;
            richTextBox1.Text = string.Empty;
            dataGridView1.Rows.Clear();
        }

        private bool CheckAnswers()
        {
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                if (Convert.ToBoolean(dataGridView1.Rows[i].Cells[0].Value.ToString()) == true)
                {
                    return true;
                }
            }

            return false;
        }

        private void button1_Click(object sender, EventArgs e)
        { // add question
            if (GetValue == false && CheckAnswers() == false)
            {
                MessageBox.Show("Please, make at least one value as correct.");
                return;
            }

            if (SingleChoice == true)
            {
                CT.AddQuestionChoice(richTextBox1.Text, GetListOfAnswers());
            }
            else if (MultiChoice == true)
            {
                CT.AddQuestionChoice(richTextBox1.Text, GetListOfAnswers());
            }
            else
            {
                CT.AddQuestionGetValue(richTextBox1.Text, GetAnswerGetValue());
            }

            currentID++;

            ClearValues();

            ReadOnlySwitcher(true, radioButton1, radioButton2, radioButton3);

            label7.Text = "You are on " + (currentID + 1) + " question of " + CT.NumberOfQuestions;
            label2.Text = "You have added " + CT.NumberOfQuestions + " questions.";
        }

        private void button4_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Add();
            dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[0].Value = false;
            dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[1].Value = "";

            ReadOnlySwitcher(false, radioButton1, radioButton2, radioButton3);
        }

        private void DataGridViewAnswerControl(DataGridViewCellEventArgs e)
        { // if it is single choice and user will select more than one value, then uncheck all the values except of that, which user has selected
            if (SingleChoice == true)
            {
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    int columnIndex = e.ColumnIndex;
                    if (e.ColumnIndex == columnIndex)
                    {
                        bool isChecked = (bool)dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
                        if (isChecked)
                        {
                            foreach (DataGridViewRow row in dataGridView1.Rows)
                            {
                                if (row.Index != e.RowIndex)
                                {
                                    row.Cells[columnIndex].Value = !isChecked;
                                }
                            }
                        }
                    }
                }
            }
        }

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex < 0 || e.RowIndex < 0) return;

            if (e.ColumnIndex == 0)
            {
                DataGridViewAnswerControl(e);
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text != string.Empty)
            {
                ReadOnlySwitcher(false, radioButton1, radioButton2, radioButton3);
            }
            else
            {
                ReadOnlySwitcher(true, radioButton1, radioButton2, radioButton3);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            CT.RemoveQuestionByID(currentID);

            if (CT.NumberOfQuestions - 1 >= 0)
            {
                currentID = CT.NumberOfQuestions - 1;

                ShowCurrectQuestion(CT);
            }
        }

        private void button6_Click(object sender, EventArgs e)
        { // add control task
            CT.SecondsToPass = SecondsToPass;
            CT.LectureID = LectureID;
            CT.FolderName = "controlTasks";

            if (CT.LectureID >= 0)
            {
                CT.Name = FH.ReadLectureFromFile(CT.LectureID).Name;

                if (mode == "Add")
                    CT.FileName = FH.ReadControlTasksFromFile().Count + ".txt";
                else
                {
                    CT.FileName = CT.ID + ".txt";
                }
            }
            else
            {
                CT.Name = (CT.ID + 1) + " Test";
                CT.FileName = FH.ReadControlTasksFromFile().Count + ".txt";
            }

            if (mode == "Add")
            {
                FH.WriteControlTaskToFile(CT);

                OnTestAdded?.Invoke(this, EventArgs.Empty);
            }
            else
            {
                FH.EditControlTaskInFile(CT);
            }

            MessageBox.Show("Control Task was " + mode + "ed" + " successfully!");

            this.Close();
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {
            LecturesList LL = new LecturesList();

            FormHandler.OpenAnotherForm(LL);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (currentID + 1 < CT.NumberOfQuestions)
                currentID++;

            if (CT.NumberOfQuestions == 0) return;

            ShowCurrectQuestion(CT);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (currentID - 1 >= 0)
                currentID--;

            if (CT.NumberOfQuestions == 0) return;

            ShowCurrectQuestion(CT);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentCell != null)
                dataGridView1.Rows.RemoveAt(dataGridView1.CurrentCell.RowIndex);

            if (CT.GetQuestionByID(currentID) != null && CT.GetQuestionByID(currentID).Answers.Count == dataGridView1.Rows.Count)
            {
                CT.RemoveAnswerChoice(currentID, dataGridView1.CurrentCell.RowIndex);
            }
            else
            {
                MessageBox.Show("Something wrong.");
            }

            if (dataGridView1.CurrentCell == null)
                ReadOnlySwitcher(true, radioButton1, radioButton2, radioButton3);
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button9_Click(object sender, EventArgs e)
        {
            if (CheckAnswers() == false)
            {
                MessageBox.Show("Please, make at least one value as correct.");
                return;
            }

            if (SingleChoice == true)
            {
                CT.EditQuestionChoice(currentID, richTextBox1.Text, GetListOfAnswers());
            }
            else if (MultiChoice == true)
            {
                CT.EditQuestionChoice(currentID, richTextBox1.Text, GetListOfAnswers());
            }
            else
            {
                CT.EditQuestionGetValue(currentID, richTextBox1.Text, GetAnswerGetValue());
            }

            ShowCurrectQuestion(CT);

            label7.Text = "You are on " + (currentID + 1) + " question of " + CT.NumberOfQuestions;
            label2.Text = "You have added " + CT.NumberOfQuestions + " questions.";
        }
    }
}
