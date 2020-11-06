﻿using System;
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
    public partial class StudentPanel : Form
    {
        Student student;

        FileHandler FH = new FileHandler();

        public StudentPanel()
        {
            student = new Student();

            InitializeComponent();
        }

        public StudentPanel(Student s)
        {
            student = s;

            InitializeComponent();
        }

        private void StudentPanel_Load(object sender, EventArgs e)
        {
            label1.Text = "Welcome " + student.Username + "!";

            label2.Text = "Average Time Spent On Lectures: " + SpentTimeOnLecture.AverageTimeSpentOnLectures(student.Username, FH.ReadTimesSpentOnLecturesFromFile()) + "s";

            label3.Text = "Average Grade For Tests: " + Grade.CalculateAverage(FH.SelectGradesByUsername(student.Username));
            this.WindowState = FormWindowState.Maximized;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Authorization a = new Authorization();

            FormHandler.OpenAnotherFormWithDispose(this, a);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            LecturesList LL = new LecturesList(student);

            FormHandler.OpenAnotherFormAsDialog(LL);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            StudentGrades SG = new StudentGrades(student);

            FormHandler.OpenAnotherFormAsDialog(SG);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            ControlTasksForm CTF = new ControlTasksForm(student);

            FormHandler.OpenAnotherFormAsDialog(CTF);
        }
    }
}
