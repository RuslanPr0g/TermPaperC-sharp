using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Layouter
{
    public class ModernLayout : IModernLayout
    {
        public void Apply(Control control)
        {
            List<Button> b = new List<Button>();

            foreach (Control B in control.Controls)
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

            List<TextBox> t = new List<TextBox>();

            foreach (Control T in control.Controls)
            {
                if (T is TextBox)
                {
                    t.Add(T as TextBox);
                }
            }

            foreach (TextBox textbox in t)
            {
                textbox.BorderStyle = BorderStyle.FixedSingle;
            }
        }
    }
}
