using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Windows.Forms;

namespace Rootech.UI.Component
{
    public class CMenuStripRender : ToolStripProfessionalRenderer, IControl
    {
        #region "code related interface properties"
        public int Depth { set; get; }

        public MouseButtonState MouseButtonState { set; get; }

        public ComponentVisualization Visualization { set; get; }
        #endregion

        public CMenuStripRender()
        {
            Visualization = ComponentVisualization.Instance;
        }
        #region "code related to rednering override method"
        protected override void OnRenderItemText(ToolStripItemTextRenderEventArgs e)
        {
            var g = e.Graphics;
            g.TextRenderingHint = TextRenderingHint.AntiAlias;
            var itemRect = GetItemRect(e.Item);
            if (e.Item.IsOnDropDown)
            {
                var brush = e.Item.Enabled ? Visualization.GetPrimaryTextBrush() : Visualization.GetDisabledOrHintBrush();
                g.DrawString(e.Text, Visualization.ROBOTO_REGULAR_9, brush, itemRect, new StringFormat()
                {
                    LineAlignment = StringAlignment.Center,
                });
            }
            else
            {
                var brush = e.Item.Pressed ? Visualization.GetPrimaryTextBrush() : Visualization.ColorScheme.LightPrimaryBrush;
                var font = e.Item.Pressed ? Visualization.ROBOTO_REGULAR_9 : Visualization.ROBOTO_MEDIUM_9;
                //g.DrawString(e.Text, font, brush, e.Item.ContentRectangle, new StringFormat() { LineAlignment = StringAlignment.Center });
                g.DrawString(e.Text, font, brush, itemRect, new StringFormat()
                {
                    LineAlignment = StringAlignment.Center, 
                    Alignment = StringAlignment.Center
                });
            }
        }
        protected override void OnRenderSeparator(ToolStripSeparatorRenderEventArgs e)
        {
            var g = e.Graphics;
            var rect = GetItemRect(e.Item);
            using (var brush = new SolidBrush(Visualization.ColorScheme.WhiteColor)) ///TODO: ToolStrip의 기본 배경으로 일치 시켜야 함
            {
                g.Clear(brush.Color);
                g.FillRectangle(brush, rect);
                g.DrawLine(new Pen(Visualization.GetDividersColor()), new Point(rect.Left, rect.Height / 2), new Point(rect.Right, rect.Height / 2));
            }

        }
        protected override void OnRenderToolStripBorder(ToolStripRenderEventArgs e)
        {
            var g = e.Graphics;
            //g.DrawRectangle(new Pen(ComponentVisualization.DIVIDERS_BLACK), new Rectangle(e.AffectedBounds.X, e.AffectedBounds.Y, e.AffectedBounds.Width - 1, e.AffectedBounds.Height - 1));
        }
        protected override void OnRenderArrow(ToolStripArrowRenderEventArgs e)
        {
            var g = e.Graphics;
            const int ARROW_SIZE = 4;

            var arrowMiddle = new Point(e.ArrowRectangle.X + e.ArrowRectangle.Width / 2, e.ArrowRectangle.Y + e.ArrowRectangle.Height / 2);
            var arrowBrush = e.Item.Enabled ? Visualization.GetPrimaryTextBrush() : Visualization.GetDisabledOrHintBrush();
            using (var arrowPath = new GraphicsPath())
            {
                arrowPath.AddLines(new[] { new Point(arrowMiddle.X - ARROW_SIZE, arrowMiddle.Y - ARROW_SIZE), new Point(arrowMiddle.X, arrowMiddle.Y), new Point(arrowMiddle.X - ARROW_SIZE, arrowMiddle.Y + ARROW_SIZE) });
                arrowPath.CloseFigure();

                g.FillPath(arrowBrush, arrowPath);
            }
        }
        protected override void OnRenderToolStripBackground(ToolStripRenderEventArgs e)
        {
            base.OnRenderToolStripBackground(e);
            var g = e.Graphics;
            g.Clear(Visualization.ColorScheme.DarkPrimaryColor);
            using (var brush = new SolidBrush(Visualization.ColorScheme.WhiteColor))   // ToolStrip의 기본 배경
            {
                g.FillRectangle(brush, e.AffectedBounds);
            }

        }
        protected override void OnRenderMenuItemBackground(ToolStripItemRenderEventArgs e)
        {
            var g = e.Graphics;
            g.Clear(Visualization.ColorScheme.DarkPrimaryColor);

            //Draw background
            var itemRect = GetItemRect(e.Item);
            using (var brush = new SolidBrush(Visualization.ColorScheme.WhiteColor)) ///TODO: ToolStrip의 기본 배경으로 일치 시켜야 함
            {
                if (e.Item.IsOnDropDown)
                {
                    g.FillRectangle(e.Item.Selected && e.Item.Enabled ? Visualization.GetCmsSelectedItemBrush() : brush, itemRect);
                }
                else
                {
                    if (e.Item.Pressed)
                    {
                        ///TODO: ToolStrip의 기본 배경으로 일치 시켜야 함
                        g.FillRectangle(brush, itemRect);
                    }
                    else
                    {
                        g.FillRectangle(e.Item.Selected ? Visualization.GetFlatButtonPressedBackgroundBrush() : Visualization.ColorScheme.DarkPrimaryBrush, itemRect);
                    }
                }
            }
        }
        #endregion

        private Rectangle GetItemRect(ToolStripItem item)
        {
            return new Rectangle(0, 0, item.ContentRectangle.Width + 4, item.ContentRectangle.Height + 4);
        }
    }
}
