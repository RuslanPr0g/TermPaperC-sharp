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
    public partial class UserSettings : Form
    {
        private Student _student;
        private Teacher _teacher;

        private List<Student> _students;
        private List<Teacher> _teachers;

        private PasswordRecovery _PR;

        FileHandler FH = new FileHandler();
        AuthHandler AH = new AuthHandler();
        Hasher H = new Hasher();

        public delegate void ChangedStudentHandler(Student user);
        public delegate void ChangedTeacherHandler(Teacher user);

        public event EventHandler OnUserSettingsChanging;
        public event ChangedStudentHandler OnStudentSettingsChanged;
        public event ChangedTeacherHandler OnTeacherSettingsChanged;

        public UserSettings(Student u)
        {
            InitializeComponent();

            _student = u;
            _teacher = null;

            if (FH.SelectPasswordRecoveryByUsername(_student.Username) != null)
                _PR = FH.SelectPasswordRecoveryByUsername(_student.Username);
        }

        public UserSettings(Teacher u)
        {
            InitializeComponent();

            _teacher = u;
            _student = null;

            if (FH.SelectPasswordRecoveryByUsername(_teacher.Username) != null)
                _PR = FH.SelectPasswordRecoveryByUsername(_teacher.Username);
        }

        private void UserSettings_Load(object sender, EventArgs e)
        {
            Form1.ModernLayout(this);

            this.question.Items.Clear();

            foreach (string S in PasswordRecovery.Questions)
            {
                this.question.Items.Add(S);
            }

            _students = _student != null ? FH.ReadStudentsFromFile() : null;
            _teachers = _teacher != null ? FH.ReadTeachersFromFile() : null;

            if (_student == null)
            {
                LoadUserData(_teacher);
            }
            else
            {
                LoadUserData(_student);
            }

            ShowPasswordHandler();

            group.Visible = _student == null ? false : true;
            label5.Visible = group.Visible;

            OnUserSettingsChanging += Save;

            this.WindowState = FormWindowState.Maximized;
        }

        private void ShowPasswordHandler()
        {
            password.PasswordChar = checkBox1.Checked == true ? '\0' : '*';
        }

        private void LoadUserData(User _user)
        {
            username.Text = _user.Username;
            password.Text = H.UnHashString(_user.Password);
            name.Text = _user.Name;
            lastname.Text = _user.Lastname;
            group.Text = _student != null ? _student.Group : string.Empty;

            List<PasswordRecovery> pass = FH.ReadPasswordRecoveryFromFile();

            if (pass == null) return;

            for (int i = 0; i < pass.Count; i++)
            {
                if (pass[i].Username == username.Text)
                {
                    question.Text = pass[i].Question;
                    answer.Text = pass[i].Answer;

                    break;
                }
            }
        }

        private void SaveUserGeneralData()
        {
            CheckByEmpty();

            if (_student != null)
            {
                for (int i = 0; i < _students.Count; i++)
                {
                    if (_students[i].Username == _student.Username)
                    {
                        if (username.Text.Length < 16)
                        {
                            _students[i].Username = _student.Username = username.Text;
                        }
                        else
                        {
                            username.Text = _student.Username;
                        }

                        if (password.Text.Length < 16 && AH.RequiredSymbols(password.Text) == true && AH.AreForbiddenSymbols(password.Text) == false)
                        {
                            _students[i].Password = _student.Password = H.HashString(password.Text);
                        }
                        else
                        {
                            password.Text = _student.Password;
                        }

                        if (name.Text.Length < 30)
                        {
                            _students[i].Name = _student.Name = name.Text;
                        }
                        else
                        {
                            name.Text = _student.Name;
                        }

                        if (lastname.Text.Length < 30)
                        {
                            _students[i].Lastname = _student.Lastname = lastname.Text;
                        }
                        else
                        {
                            lastname.Text = _student.Lastname;
                        }

                        if (group.Text.Length < 20)
                        {
                            _students[i].Group = _student.Group = group.Text;
                        }
                        else
                        {
                            group.Text = _student.Group;
                        }

                        break;
                    }
                }

                FH.ReWriteStudentsToFile(_students);
            }
            else
            {
                for (int i = 0; i < _teachers.Count; i++)
                {
                    if (_teachers[i].Username == _teacher.Username)
                    {
                        if (username.Text.Length < 16)
                        {
                            _teachers[i].Username = _teacher.Username = username.Text;
                        }
                        else
                        {
                            username.Text = _teacher.Username;
                        }

                        if (password.Text.Length < 16 && AH.RequiredSymbols(password.Text) == true && AH.AreForbiddenSymbols(password.Text) == false)
                            _teachers[i].Password = _teacher.Password = H.HashString(password.Text);
                        else
                        {
                            password.Text = _teacher.Password;
                        }

                        if (name.Text.Length < 30)
                        {
                            _teachers[i].Name = _teacher.Name = name.Text;
                        }
                        else
                        {
                            name.Text = _teacher.Name;
                        }

                        if (lastname.Text.Length < 30)
                        {
                            _teachers[i].Lastname = _teacher.Lastname = lastname.Text;
                        }
                        else
                        {
                            lastname.Text = _teacher.Lastname;
                        }

                        break;
                    }
                }

                FH.ReWriteTeachersToFile(_teachers);
            }
        }

        private void SaveUserPasswordRecoveryData()
        {
            List<PasswordRecovery> pass = FH.ReadPasswordRecoveryFromFile();

            if (_PR == null)
            {
                _PR = new PasswordRecovery(username.Text, question.Text, answer.Text);
            }

            if (pass.Contains(_PR) == false)
            {
                pass.Add(_PR);
            }

            for (int i = 0; i < pass.Count; i++)
            {
                if (pass[i].Username == username.Text)
                {
                    pass[i].Question = question.Text;
                    pass[i].Answer = answer.Text;
                }
            }

            FH.WritePasswordRecoveryToFile(pass);
        }

        private void CheckByEmpty()
        {
            if (username.Text == string.Empty)
            {
                username.Text = _student == null ? _teacher.Username : _student.Username;
            }

            if (password.Text == string.Empty)
            {
                password.Text = _student == null ? _teacher.Password : _student.Password;
            }

            if (name.Text == string.Empty)
            {
                name.Text = _student == null ? _teacher.Name : _student.Name;
            }

            if (lastname.Text == string.Empty)
            {
                lastname.Text = _student == null ? _teacher.Lastname : _student.Lastname;
            }

            if (_student != null && group.Text == string.Empty)
            {
                group.Text = _student.Group;
            }
        }

        private void SaveUserData()
        {
            SaveUserGeneralData();

            if (answer.Text.Trim() != string.Empty)
                SaveUserPasswordRecoveryData();

            this.Hide();
            this.Close();
            this.Dispose();
        }

        private void Save(object sender, EventArgs e)
        {
            SaveUserData();

            if (_student != null)
                OnStudentSettingsChanged?.Invoke(this._student);
            else
            {
                OnTeacherSettingsChanged?.Invoke(this._teacher);
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void save_Click(object sender, EventArgs e)
        {
            OnUserSettingsChanging?.Invoke(this, EventArgs.Empty);
        }

        private void username_TextChanged(object sender, EventArgs e)
        {
            if (_student != null)
            {
                if (username.Text != _student.Username && FH.SelectStudentByUsername(username.Text) != null)
                {
                    username.Text = _student.Username;

                    MessageBox.Show("Username already exists...");
                }
            }
            else
            {
                if (username.Text != _teacher.Username && FH.SelectTeacherByUsername(username.Text) != null)
                {
                    username.Text = _teacher.Username;

                    MessageBox.Show("Username already exists...");
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (_student == null)
            {
                LoadUserData(_teacher);
            }
            else
            {
                LoadUserData(_student);
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            ShowPasswordHandler();
        }
    }
}
