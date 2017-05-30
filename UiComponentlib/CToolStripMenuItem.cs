using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
namespace Rootech.UI.Component
{
    public class CToolStripMenuItem : ToolStripMenuItem, IControl
    {
        #region "Code related interface properties"
        public int Depth { set; get; }

        public MouseButtonState MouseButtonState { set; get; }

        public ComponentVisualization Visualization { set; get; }
        #endregion

        public CToolStripMenuItem()
        {

        }
    }
}
