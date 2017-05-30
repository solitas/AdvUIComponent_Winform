using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Rootech.UI.Component
{
    public class CCheckBox : CheckBox, IControl
    {
        public int Depth { set; get; }

        public MouseButtonState MouseButtonState { set; get; }

        public ComponentVisualization Visualization { set; get; }
    }
}
