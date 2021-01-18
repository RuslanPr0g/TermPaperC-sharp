using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace Layouter
{
    public interface ILayout
    {
        void Apply(Control control);
    }
}
