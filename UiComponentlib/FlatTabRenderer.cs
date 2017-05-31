using System.Drawing;
using System.Windows.Forms;

namespace Rootech.UI.Component
{
    public class FlatTabRenderer : CTabRenderer, IControl
    {
        #region "Code related to IControl Interface"
        public int Depth { set; get; }

        public MouseButtonState MouseButtonState { set; get; }

        public ComponentVisualization Visualization { set; get; }
        #endregion

        public override DockStyle[] SupportedTabDockStyles
        {
            get
            {
                return new DockStyle[] { DockStyle.Bottom, DockStyle.Top, DockStyle.Left, DockStyle.Right };
            }
        }

        public override bool UsesHighlghts
        {
            get
            {
                return false;
            }
        }
        public FlatTabRenderer()
        {
            Visualization = ComponentVisualization.Instance;
        }

        public override void DrawTab(Color foreColor, Color backColor, Color highlightColor, Color shadowColor, Color borderColor, bool active, bool mouseOver, DockStyle dock, Graphics graphics, SizeF tabSize)
        {
            Rectangle headerRect = new Rectangle(0, 0, (int)tabSize.Width, (int)tabSize.Height);
            var brush = active ? Visualization.ColorScheme.LightPrimaryBrush : Visualization.ColorScheme.DarkPrimaryBrush;
            graphics.FillRectangle(brush, headerRect);
        }

        public override bool SupportsTabDockStyle(DockStyle dock)
        {
            return (dock != DockStyle.Fill && dock != DockStyle.None);
        }
    }
}
