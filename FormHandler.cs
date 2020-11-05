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
            open.Show();
            close.Dispose();
        }

        public static void OpenAnotherFormWithoutDispose(Form close, Form open)
        {
            close.Hide();
            open.Closed += (s, args) => close.Close();
            open.ShowDialog();
            open.Show();
        }

        public static void OpenAnotherFormAsDialog(Form open)
        {
            open.ShowDialog();
        }

        public static void OpenAnotherForm(Form open)
        {
            open.Show();
        }

        public static void OpenAnotherFormAsDialogAndClose(Form close, Form open)
        {
            open.ShowDialog();
            FormClose(close, open);
        }

        public static void OpenAnotherFormAndClose(Form close, Form open)
        {
            open.Show();
            FormClose(close, open);
        }

        public static void OpenAnotherFormAsDialogAndHide(Form hide, Form open)
        {
            open.ShowDialog();
            hide.Hide();
        }

        public static void OpenAnotherFormAndHide(Form hide, Form open)
        {
            open.Show();
            hide.Hide();
        }

        private static void FormClose(Form close, Form open)
        {
            close.Hide();
            close.Close();
        }
    }
}
