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
    public partial class Authorization : Form
    {
        AuthHandler AH = new AuthHandler();
        FileHandler FH = new FileHandler();

        bool student;

        private bool _LOGINMODE = true;

        public Authorization()
        {
            InitializeComponent();
        }

        private void Authorization_Load(object sender, EventArgs e)
        {
            radioButton1.Checked = true;

            VisibilityConfirmField();

            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;

            pictureBox1.ImageLocation = @"C:\Users\theru\Desktop\Term Work OOP Rudenko\Term_Paper_Rudenko\Term_Paper_Rudenko\bin\Debug\password.png";

            DisableEnableLabels(false, label3, label5, label6);

            DisableEnableTextBoxes(false, textBox4, textBox5, textBox6);

            SignupRequirements.Visible = false;

            ShowPasswordHandler();
        }

        private string Username
        {
            get
            {
                return textBox1.Text;
            }
            set
            {
                textBox1.Text = value;
            }
        }

        private string Password
        {
            get
            {
                return textBox2.Text;
            }
            set
            {
                textBox2.Text = value;
            }
        }

        private string ConfirmPassword
        {
            get
            {
                return textBox3.Text;
            }
            set
            {
                textBox3.Text = value;
            }
        }

        private string NameField
        {
            get
            {
                return textBox4.Text;
            }
            set
            {
                textBox4.Text = value;
            }
        }

        private string Lastname
        {
            get
            {
                return textBox5.Text;
            }
            set
            {
                textBox5.Text = value;
            }
        }

        private string Group
        {
            get
            {
                return textBox6.Text;
            }
            set
            {
                textBox6.Text = value;
            }
        }

        private bool Student
        {
            get
            {
                return radioButton1.Checked;
            }
        }

        private void VisibilityConfirmField()
        {
            if (_LOGINMODE == false)
            {
                label4.Visible = true;
                textBox3.Visible = true;
            }
            else
            {
                label4.Visible = false;
                textBox3.Visible = false;
            }
        }

        private void VisibilityStudentTeacher()
        {
            if (Student == false && _LOGINMODE == false)
            {
                label6.Enabled = false;
                textBox6.Enabled = false;
            }
            else if (Student == true && _LOGINMODE == false)
            {
                label6.Enabled = true;
                textBox6.Enabled = true;
            }
        }

        private void LoginMode()
        {
            _LOGINMODE = true;
            VisibilityConfirmField();
            VisibilityStudentTeacher();
            button1.Text = AH.LoginText;
            button2.Text = AH.NeedAccount;
            pictureBox1.ImageLocation = @"C:\Users\theru\Desktop\Term Work OOP Rudenko\Term_Paper_Rudenko\Term_Paper_Rudenko\bin\Debug\password.png";
            ClearTextBoxes(textBox1, textBox2, textBox3, textBox4, textBox5, textBox6);
            DisableEnableLabels(false, label3, label5, label6);
            DisableEnableTextBoxes(false, textBox4, textBox5, textBox6);

            SignupRequirements.Visible = false;
        }

        private void SignUpMode()
        {
            _LOGINMODE = false;
            VisibilityConfirmField();
            VisibilityStudentTeacher();
            button1.Text = AH.SignupText;
            button2.Text = AH.AccountExists;
            pictureBox1.ImageLocation = @"C:\Users\theru\Desktop\Term Work OOP Rudenko\Term_Paper_Rudenko\Term_Paper_Rudenko\bin\Debug\username.png";
            DisableEnableLabels(true, label3, label5);
            DisableEnableTextBoxes(true, textBox4, textBox5);

            SignupRequirements.Visible = true;
        }

        private void DisableEnableLabels(bool state, params Label[] ls)
        {
            foreach (Label label in ls)
            {
                label.Enabled = state;
            }
        }

        private void DisableEnableTextBoxes(bool state, params TextBox[] tx)
        {
            foreach (TextBox textbox in tx)
            {
                textbox.Enabled = state;
            }
        }

        private void ClearTextBoxes(params TextBox[] tx)
        {
            foreach (TextBox textbox in tx)
            {
                textbox.Text = string.Empty;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            _LOGINMODE = !_LOGINMODE;

            switch (_LOGINMODE)
            {
                case true:
                    LoginMode();
                    break;
                case false:
                    SignUpMode();
                    break;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            switch (radioButton1.Checked)
            {
                case false:
                    student = false;
                    break;
                default:
                    student = true;
                    break;
            }

            switch (_LOGINMODE)
            {
                case false:
                    if (student == true)
                    {
                        if (AH.SignUPStudent(Username, Password, ConfirmPassword, NameField, Lastname, Group) == "")
                        {
                            MessageBox.Show("Now Login!");
                            LoginMode();
                        }
                        else
                        {
                            MessageBox.Show(AH.SignUPStudent(Username, Password, ConfirmPassword, NameField, Lastname, Group));
                        }
                    }
                    else
                    {
                        if (AH.SignUPTeacher(Username, Password, ConfirmPassword, NameField, Lastname) == "")
                        {
                            MessageBox.Show("Now Login!");
                            LoginMode();
                        }
                        else
                        {
                            MessageBox.Show(AH.SignUPTeacher(Username, Password, ConfirmPassword, NameField, Lastname));
                        }
                    }
                    break;
                case true:
                    if (student == true)
                    {
                        string result = AH.LogINStudent(Username, Password);

                        if (result == "")
                        {
                            StudentPanel SP = new StudentPanel(new Student(Username, Password, NameField, Lastname, Group));

                            FormHandler.OpenAnotherFormAsDialogAndClose(this, SP);
                        }
                        else
                        {
                            MessageBox.Show(result);
                        }
                    }
                    else
                    {
                        string result = AH.LogINTeacher(Username, Password);

                        if (result == "")
                        {
                            TeacherPanel TP = new TeacherPanel(new Teacher(Username, Password, NameField, Lastname));

                            FormHandler.OpenAnotherFormAsDialogAndClose(this, TP);
                        }
                        else
                        {
                            MessageBox.Show(result);
                        }
                    }
                    break;
            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            VisibilityStudentTeacher();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void ShowPasswordHandler()
        {
            if (checkBox1.Checked == true)
            {
                textBox2.PasswordChar = '\0';
                textBox3.PasswordChar = '\0';
            }
            else
            {
                textBox2.PasswordChar = '*';
                textBox3.PasswordChar = '*';
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            ShowPasswordHandler();
        }
    }
}
