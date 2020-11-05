using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Term_Paper_Rudenko
{
    public class GetValueAnswer : Answer
    {
        private string _correctValue;

        public GetValueAnswer()
        {
            _correctValue = "";
        }

        public GetValueAnswer(string value)
        {
            _correctValue = value;
        }

        public string CorrectValue
        {
            get
            {
                return _correctValue;
            }

            set
            {
                _correctValue = value;
            }
        }

        public override string String()
        {
            return _correctValue;
        }
    }
}
