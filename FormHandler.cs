using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Term_Paper_Rudenko
{
    public static class FormHandler
    {
        public static void OpenAnotherFormWithDispose(Form close, Form open)
        {
            close.Hide();
            open.Closed += (s, args) => close.Close();
            open.ShowDialog();
            close.Dispose();

            FullScreen(open);
        }

        public static void OpenAnotherFormWithoutDispose(Form close, Form open)
        {
            close.Hide();
            open.Closed += (s, args) => close.Close();
            open.ShowDialog();

            FullScreen(open);
        }

        public static void OpenAnotherFormAsDialog(Form open)
        {
            open.ShowDialog();

            FullScreen(open);
        }

        public static void OpenAnotherForm(Form open)
        {
            open.Show();

            FullScreen(open);
        }

        public static void OpenAnotherFormAsDialogAndClose(Form close, Form open)
        {
            open.ShowDialog();
            FormClose(close);

            FullScreen(open);
        }

        public static void OpenAnotherFormAndClose(Form close, Form open)
        {
            open.Show();
            FormClose(close);

            FullScreen(open);
        }

        public static void OpenAnotherFormAsDialogAndHide(Form hide, Form open)
        {
            open.ShowDialog();
            hide.Hide();

            FullScreen(open);
        }

        public static void OpenAnotherFormAndHide(Form hide, Form open)
        {
            open.Show();
            hide.Hide();

            FullScreen(open);
        }

        private static void FormClose(Form close)
        {
            close.Hide();
            close.Close();
        }

        public static void FullScreen(Form open)
        {
            open.WindowState = FormWindowState.Maximized;
        }
    }
}
