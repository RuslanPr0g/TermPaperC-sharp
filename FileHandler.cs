using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace Term_Paper_Rudenko
{
    public class FileHandler
    {
        private string students = "students.bin";
        private string teachers = "teachers.bin";
        private string grades = "grades.bin";
        private string times = "times.bin";
        private string passwordRecoveries = "recoveries.bin";

        public FileHandler()
        {

        }

        public void WriteTeacherToFile(string username, string password, string name, string lastname)
        {
            FileStream fs;

            BinaryWriter bw;

            try
            {
                fs = new FileStream(teachers, FileMode.Append);
                bw = new BinaryWriter(fs);
            }
            catch
            {
                return;
            }

            try
            {
                bw.Write(username);
                bw.Write(password);
                bw.Write(name);
                bw.Write(lastname);
            }
            catch
            {
                return;
            }

            bw.Close();
            fs.Close();
        }

        public void ReWriteTeachersToFile(List<Teacher> t)
        {
            FileStream fs;

            BinaryWriter bw;

            try
            {
                fs = new FileStream(teachers, FileMode.Create);
                bw = new BinaryWriter(fs);
            }
            catch
            {
                return;
            }

            try
            {
                foreach (Teacher T in t)
                {
                    bw.Write(T.Username);
                    bw.Write(T.Password);
                    bw.Write(T.Name);
                    bw.Write(T.Lastname);
                }
            }
            catch
            {
                return;
            }

            bw.Close();
            fs.Close();
        }

        public List<Teacher> ReadTeachersFromFile()
        {
            List<Teacher> t = new List<Teacher>();

            FileStream fs;
            BinaryReader br;

            try
            {
                fs = new FileStream(teachers, FileMode.Open);
                br = new BinaryReader(fs);
            }
            catch
            {
                return t;
            }

            string username = "";
            string password = "";
            string name = "";
            string lastname = "";

            try
            {
                while (true)
                {
                    try
                    {
                        username = br.ReadString();
                        password = br.ReadString();
                        name = br.ReadString();
                        lastname = br.ReadString();

                        t.Add(new Teacher(username, password, name, lastname));
                    }
                    catch { break; }
                }
            }
            catch
            {
            }

            br.Close();
            fs.Close();

            return t;
        }

        public void WriteStudentToFile(string username, string password, string name, string lastname, string group)
        {
            FileStream fs;

            BinaryWriter bw;

            try
            {
                fs = new FileStream(students, FileMode.Append);
                bw = new BinaryWriter(fs);
            }
            catch
            {
                return;
            }

            try
            {
                bw.Write(username);
                bw.Write(password);
                bw.Write(name);
                bw.Write(lastname);
                bw.Write(group);
            }
            catch
            {
                return;
            }

            bw.Close();
            fs.Close();
        }

        public void ReWriteStudentsToFile(List<Student> s)
        {
            FileStream fs;

            BinaryWriter bw;

            try
            {
                fs = new FileStream(students, FileMode.Create);
                bw = new BinaryWriter(fs);
            }
            catch
            {
                return;
            }

            try
            {
                foreach (Student S in s)
                {
                    bw.Write(S.Username);
                    bw.Write(S.Password);
                    bw.Write(S.Name);
                    bw.Write(S.Lastname);
                    bw.Write(S.Group);
                }
            }
            catch
            {
                return;
            }

            bw.Close();
            fs.Close();
        }

        public List<Student> ReadStudentsFromFile()
        {
            List<Student> s = new List<Student>();

            FileStream fs;
            BinaryReader br;

            try
            {
                fs = new FileStream(students, FileMode.Open);
                br = new BinaryReader(fs);
            }
            catch
            {
                return s;
            }

            string username = "";
            string password = "";
            string name = "";
            string lastname = "";
            string group = "";

            try
            {
                while (true)
                {
                    try
                    {
                        username = br.ReadString();
                        password = br.ReadString();
                        name = br.ReadString();
                        lastname = br.ReadString();
                        group = br.ReadString();

                        s.Add(new Student(username, password, name, lastname, group));
                    }
                    catch { break; }
                }
            }
            catch
            {
            }

            br.Close();
            fs.Close();

            return s;
        }

        public Student SelectStudentByUsername(string username)
        {
            List<Student> st = this.ReadStudentsFromFile();

            foreach (Student student in st)
                if (student.Username == username)
                    return student;

            return null;
        }

        public Teacher SelectTeacherByUsername(string username)
        {
            List<Teacher> st = this.ReadTeachersFromFile();

            foreach (Teacher teacher in st)
                if (teacher.Username == username)
                    return teacher;

            return null;
        }

        public void WriteLectureToFile(Lecture L)
        {
            CreateNewFileInFolder(L.FolderName, L.FileName, L.Info());
        }

        public void EditLectureInFile(Lecture L)
        {
            ModifyFileInFolder(L.FolderName, L.FileName, L.Info());
        }

        public Lecture ReadLectureFromFile(int id)
        {
            Lecture lecture = new Lecture();
            string info = "";

            info += ReadFileFromFolder(lecture.FolderName, id + ".txt");

            string[] bufferLecture = info.Split('\t');

            lecture.ID = Convert.ToInt32(bufferLecture[0]);
            lecture.Name = bufferLecture[1];
            lecture.Topic = bufferLecture[2];

            string[] bufferPortions = bufferLecture[3].Split(new string[] { lecture.NewLine }, StringSplitOptions.None);

            for (int i = 0; i < bufferPortions.Length - 1; i++)
            {
                if (bufferPortions[i] == "") return null;

                lecture.AddPortion(bufferPortions[i]);
            }

            return lecture;
        }

        public List<Lecture> ReadLecturesFromFile()
        {
            List<Lecture> lectures = new List<Lecture>();

            int i = 0;

            while (true)
            {
                try
                {
                    lectures.Add(ReadLectureFromFile(i));

                    i++;
                }
                catch
                {
                    break;
                }
            }

            return lectures;
        }

        public Lecture SelectLectureByID(int lectureID)
        {
            List<Lecture> lectures = this.ReadLecturesFromFile();

            foreach (Lecture l in lectures)
            {
                if (l.ID == lectureID)
                {
                    return l;
                }
            }

            return null;
        }

        public ControlTask SelectControlTaskByID(int taskID)
        {
            List<ControlTask> controlTasks = this.ReadControlTasksFromFile();

            foreach (ControlTask t in controlTasks)
            {
                if (t.ID == taskID)
                {
                    return t;
                }
            }

            return null;
        }

        public void WriteControlTaskToFile(ControlTask CT)
        {
            CreateNewFileInFolder(CT.FolderName, CT.FileName, CT.String());
        }

        public void EditControlTaskInFile(ControlTask CT)
        {
            CT.FolderName = "controlTasks";

            ModifyFileInFolder(CT.FolderName, CT.FileName, CT.String());
        }

        public ControlTask ReadControlTaskFromFile(int id)
        {
            AddEditControlTask ct = new AddEditControlTask();
            string info = "";

            info += ReadFileFromFolder(ct.FolderName, id + ".txt");

            string[] bufferControlTask = info.Split(new string[] { ct.Tab }, StringSplitOptions.None);

            ct.ID = Convert.ToInt32(bufferControlTask[0]);
            if (ct.LectureID < 0)
                ct.LectureID = Convert.ToInt32(bufferControlTask[1]);
            ct.Name = bufferControlTask[2];
            ct.SecondsToPass = Convert.ToInt32(bufferControlTask[3]);

            string[] bufferAnswers;
            string[] bufferAnswersInfo;

            int countQuestions = 0;

            for (int i = 4; i < bufferControlTask.Length; i++)
            {
                // here i am adding questions

                bufferAnswers = bufferControlTask[i].Split(new string[] { Question._tab }, StringSplitOptions.None);

                if (bufferAnswers[1] == "QuestionChoice")
                {
                    ct.AddQuestionChoice(bufferAnswers[0], Convert.ToInt32(bufferAnswers[2]));

                    for (int j = 3; j < bufferAnswers.Length; j++)
                    {
                        bufferAnswersInfo = bufferAnswers[j].Split(new string[] { Answer._tab }, StringSplitOptions.None);

                        ct.AddChoiceAnswer(Convert.ToInt32(bufferAnswers[2]), bufferAnswersInfo[0], Convert.ToBoolean(bufferAnswersInfo[1]));
                    }
                }
                else
                {
                    ct.AddQuestionGetValue(bufferAnswers[0], Convert.ToInt32(bufferAnswers[2]));

                    bufferAnswersInfo = bufferAnswers[3].Split(new string[] { Answer._tab }, StringSplitOptions.None);

                    ct.AddGetValueAnswer(Convert.ToInt32(bufferAnswers[2]), bufferAnswersInfo[0]);
                }

                countQuestions++;
            }

            return ct;
        }

        public List<ControlTask> ReadControlTasksFromFile()
        {
            List<ControlTask> ct = new List<ControlTask>();

            int i = 0;

            while (true)
            {
                try
                {
                    ct.Add(ReadControlTaskFromFile(i));

                    i++;
                }
                catch
                {
                    break;
                }
            }

            return ct;
        }

        public ControlTask SelectControlTaskByLectureID(int id)
        {
            List<ControlTask> ct = this.ReadControlTasksFromFile();

            foreach (ControlTask test in ct)
                if (test.LectureID == id)
                    return test;

            return null;
        }

        public List<Grade> SelectGradesByUsername(string username)
        {
            List<Grade> grades = this.ReadGradesFromFile();
            List<Grade> studentGrades = new List<Grade>();

            foreach (Grade grade in grades)
                if (grade.StudentUsername == username)
                    studentGrades.Add(grade);

            return studentGrades;
        }

        public void WriteGradeToFile(Grade grade)
        {
            FileStream fs;

            BinaryWriter bw;

            try
            {
                fs = new FileStream(grades, FileMode.Append);
                bw = new BinaryWriter(fs);
            }
            catch
            {
                return;
            }

            try
            {
                bw.Write(grade.ID);
                bw.Write(grade.StudentUsername);
                bw.Write(grade.ControlTask);
                bw.Write(grade.Grade5);
            }
            catch
            {
                return;
            }

            bw.Close();
            fs.Close();
        }

        public void ReWriteGradeToFile(List<Grade> grades)
        {
            FileStream fs;

            BinaryWriter bw;

            try
            {
                fs = new FileStream(this.grades, FileMode.Create);
                bw = new BinaryWriter(fs);
            }
            catch
            {
                return;
            }

            try
            {
                foreach (Grade g in grades)
                {
                    bw.Write(g.ID);
                    bw.Write(g.StudentUsername);
                    bw.Write(g.ControlTask);
                    bw.Write(g.Grade5);
                }
            }
            catch
            {
                return;
            }

            bw.Close();
            fs.Close();
        }

        public List<Grade> ReadGradesFromFile()
        {
            List<Grade> grades = new List<Grade>();

            FileStream fs;
            BinaryReader br;

            try
            {
                fs = new FileStream(this.grades, FileMode.Open);
                br = new BinaryReader(fs);
            }
            catch
            {
                return grades;
            }

            int gradeID = 0;
            string username = "";
            int taskID = 0;
            string grade5 = "";

            try
            {
                while (true)
                {
                    try
                    {
                        gradeID = br.ReadInt32();
                        username = br.ReadString();
                        taskID = br.ReadInt32();
                        grade5 = br.ReadString();

                        grades.Add(new Grade(gradeID, username, taskID, grade5));
                    }
                    catch { break; }
                }
            }
            catch
            {
            }

            br.Close();
            fs.Close();

            return grades;
        }

        public void WritePasswordRecoveryToFile(List<PasswordRecovery> passwords)
        {
            FileStream fs;

            BinaryWriter bw;

            try
            {
                fs = new FileStream(this.passwordRecoveries, FileMode.Create);
                bw = new BinaryWriter(fs);
            }
            catch
            {
                return;
            }

            try
            {
                foreach (PasswordRecovery PR in passwords)
                {
                    bw.Write(PR.Username);
                    bw.Write(PR.Question);
                    bw.Write(PR.Answer);
                }
            }
            catch
            {
                return;
            }

            bw.Close();
            fs.Close();
        }

        public List<PasswordRecovery> ReadPasswordRecoveryFromFile()
        {
            List<PasswordRecovery> passwords = new List<PasswordRecovery>();

            FileStream fs;
            BinaryReader br;

            try
            {
                fs = new FileStream(this.passwordRecoveries, FileMode.Open);
                br = new BinaryReader(fs);
            }
            catch
            {
                return passwords;
            }

            string username = "";
            string question = "";
            string answer = "";

            try
            {
                while (true)
                {
                    try
                    {
                        username = br.ReadString();
                        question = br.ReadString();
                        answer = br.ReadString();

                        passwords.Add(new PasswordRecovery(username, question, answer));
                    }
                    catch { break; }
                }
            }
            catch
            {
            }

            br.Close();
            fs.Close();

            return passwords;
        }

        public PasswordRecovery SelectPasswordRecoveryByUsername(string username)
        {
            List<PasswordRecovery> passwords = this.ReadPasswordRecoveryFromFile();

            foreach (PasswordRecovery p in passwords)
            {
                if (p.Username == username)
                {
                    return p;
                }
            }

            return null;
        }

        public void AppendTimeSpentOnLectureToFile(SpentTimeOnLecture t)
        {
            FileStream fs;

            BinaryWriter bw;

            try
            {
                fs = new FileStream(this.times, FileMode.Append);
                bw = new BinaryWriter(fs);
            }
            catch
            {
                return;
            }

            try
            {
                bw.Write(t.Username);
                bw.Write(t.LectureID);
                bw.Write(t.Seconds);
            }
            catch
            {
                return;
            }

            bw.Close();
            fs.Close();
        }

        public void WriteTimesSpentOnLectureToFile(List<SpentTimeOnLecture> times)
        {
            FileStream fs;

            BinaryWriter bw;

            try
            {
                fs = new FileStream(this.times, FileMode.Create);
                bw = new BinaryWriter(fs);
            }
            catch
            {
                return;
            }

            try
            {
                foreach (SpentTimeOnLecture t in times)
                {
                    bw.Write(t.Username);
                    bw.Write(t.LectureID);
                    bw.Write(t.Seconds);
                }
            }
            catch
            {
                return;
            }

            bw.Close();
            fs.Close();
        }

        public List<SpentTimeOnLecture> ReadTimesSpentOnLecturesFromFile()
        {
            List<SpentTimeOnLecture> times = new List<SpentTimeOnLecture>();

            FileStream fs;
            BinaryReader br;

            try
            {
                fs = new FileStream(this.times, FileMode.Open);
                br = new BinaryReader(fs);
            }
            catch
            {
                return times;
            }

            string username = "";
            int lectureID = 0;
            int seconds = 0;

            try
            {
                while (true)
                {
                    try
                    {
                        username = br.ReadString();
                        lectureID = br.ReadInt32();
                        seconds = br.ReadInt32();

                        times.Add(new SpentTimeOnLecture(username, lectureID, seconds));
                    }
                    catch { break; }
                }
            }
            catch
            {
            }

            br.Close();
            fs.Close();

            return times;
        }

        public List<SpentTimeOnLecture> SelectTimesByLectureID(int lectureID)
        {
            List<SpentTimeOnLecture> times = this.ReadTimesSpentOnLecturesFromFile();
            List<SpentTimeOnLecture> ntimes = new List<SpentTimeOnLecture>();

            foreach (SpentTimeOnLecture t in times)
            {
                if (t.LectureID == lectureID)
                {
                    ntimes.Add(t);
                }
            }

            return ntimes;
        }

        public List<SpentTimeOnLecture> SelectTimesByUsername(string username)
        {
            List<SpentTimeOnLecture> times = this.ReadTimesSpentOnLecturesFromFile();
            List<SpentTimeOnLecture> ntimes = new List<SpentTimeOnLecture>();

            foreach (SpentTimeOnLecture t in times)
            {
                if (t.Username == username)
                {
                    ntimes.Add(t);
                }
            }

            return ntimes;
        }

        public SpentTimeOnLecture SelectTimeByLectureIDAndUsername(int lectureID, string username)
        {
            List<SpentTimeOnLecture> times = this.ReadTimesSpentOnLecturesFromFile();

            foreach (SpentTimeOnLecture t in times)
            {
                if (t.LectureID == lectureID && t.Username == username)
                {
                    return t;
                }
            }

            return null;
        }

        public void CreateNewFileInFolder(string foldername, string filename, string data)
        {
            FileAppendData(data, foldername + @"\" + filename);
        }

        public void ModifyFileInFolder(string foldername, string filename, string data)
        {
            FileReCreateData(data, foldername + @"\" + filename);
        }

        public string ReadFileFromFolder(string foldername, string filename)
        {
            string data = string.Empty;

            data += ReadFile(foldername + @"\" + filename);

            return data;
        }

        public void FileAppendData(string info, string filename)
        {
            FileStream toStream = new FileStream(filename,
            FileMode.Append, FileAccess.Write);
            StreamWriter fileWriter = new StreamWriter(toStream);
            fileWriter.WriteLine(info);
            fileWriter.Close();
            toStream.Close();
        }

        public void FileReCreateData(string info, string filename)
        {
            FileStream toStream = new FileStream(filename,
            FileMode.Create, FileAccess.Write);
            StreamWriter fileWriter = new StreamWriter(toStream);
            fileWriter.WriteLine(info);
            fileWriter.Close();
            toStream.Close();
        }

        public string ReadFile(string filename)
        {
            string data = "";

            FileStream fromStream = new FileStream(filename,
            FileMode.Open, FileAccess.Read);
            StreamReader fileReader = new StreamReader(fromStream);
            String input;

            while ((input = fileReader.ReadLine()) != null)
            {
                data += input + "\n";
            }

            fileReader.Close();
            fromStream.Close();

            return data;
        }

        public static string BrowseForFile()
        {
            string text = string.Empty;

            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            DialogResult result = openFileDialog1.ShowDialog();

            if (result == DialogResult.OK)
            {
                string file = openFileDialog1.FileName;

                try
                {
                    text = File.ReadAllText(file);
                }
                catch (IOException e)
                {
                    return e.Message;
                }
            }

            return text;
        }
    }
}
