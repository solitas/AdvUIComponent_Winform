using System.Windows.Forms;
namespace Rootech.UI.Component
{
    public class CLabel : Label, IControl
    {
        public int Depth { set; get; }

        public MouseButtonState MouseButtonState { set; get; }

        public ComponentVisualization Visualization { set; get; }
    }
}
