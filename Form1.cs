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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Authorization a = new Authorization();

            FormHandler.OpenAnotherFormWithDispose(this, a);
        }

        public static void StyleButtons(Control c)
        {
            List<Button> b = new List<Button>();

            foreach (Control B in c.Controls)
            {
                if (B is Button)
                {
                    b.Add(B as Button);
                }
            }

            foreach (Button button in b)
            {
                button.BackColor = Color.Transparent;
                button.FlatStyle = FlatStyle.Flat;
            }
        }
    }
}
