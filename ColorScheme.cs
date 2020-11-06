using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Term_Paper_Rudenko
{
    public class ColorScheme
    {
        private string _theme;

        public Color PanelBG;
        public Color PanelFG;
        public Color ButtonBG;
        public Color ButtonFG;
        public Color TextBoxBG;
        public Color TextBoxFG;

        public ColorScheme()
        {
            Theme = "White";

            SetColor();
        }

        public ColorScheme(string theme)
        {
            Theme = theme;

            SetColor();
        }

        private void SetColor()
        {
            switch (_theme)
            {
                case "White":
                    this.PanelBG = Color.White;
                    this.PanelFG = Color.Black;
                    this.ButtonBG = Color.White;
                    this.ButtonFG = Color.Black;
                    this.TextBoxBG = Color.White;
                    this.TextBoxFG = Color.Black;
                    break;
                case "Dark":
                    this.PanelBG = Color.Black;
                    this.PanelFG = Color.White;
                    this.ButtonBG = Color.Black;
                    this.ButtonFG = Color.White;
                    this.TextBoxBG = Color.Black;
                    this.TextBoxFG = Color.White;
                    break;
            }
        }

        public string Theme { get => _theme; set => _theme = value; }
    }
}
