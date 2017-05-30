using System.Windows.Forms;

namespace Rootech.UI.Component
{
    public class CMenuStrip : MenuStrip, IControl
    {
        public int Depth { set; get; }

        public MouseButtonState MouseButtonState { set; get; }

        public ComponentVisualization Visualization { set; get; }

        public CMenuStrip()
        {

        }
    }
}
