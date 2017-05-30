using System.Drawing;
using System.Windows.Forms;

namespace Rootech.UI.Component
{
    public class FlatTabRenderer : CTabRenderer
    {
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

        public override void DrawTab(Color foreColor, Color backColor, Color highlightColor, Color shadowColor, Color borderColor, bool active, bool mouseOver, DockStyle dock, Graphics graphics, SizeF tabSize)
        {
            Rectangle headerRect = new Rectangle(0, 0, (int)tabSize.Width, (int)tabSize.Height);

            if (active)
            {
                using (Brush brush = new SolidBrush(foreColor))
                using (Pen pen = new Pen(borderColor))
                {
                    graphics.FillRoundRect(brush, headerRect, 5f);
                }
            }
            else
            {
                using (Brush brush = new SolidBrush(backColor))
                {
                    //graphics.FillRectangle(brush, new RectangleF(0, 0, tabSize.Width, tabSize.Height));
                }
            }
        }

        public override bool SupportsTabDockStyle(DockStyle dock)
        {
            return (dock != DockStyle.Fill && dock != DockStyle.None);
        }
    }
}
