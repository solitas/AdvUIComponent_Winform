using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Rootech.UI.Component
{
    public class MenuButton : CButton
    {
        private Color _hoverForeColor;
        private Color _hoverButtonColor;
        private Color _hoverBorderColor;


        public MenuButton()
        {

        }

        public Color HoverBorderColor
        {
            get { return _hoverBorderColor; }
            set { _hoverBorderColor = value; }
        }
        public Color HoverButtonColor
        {
            get { return _hoverButtonColor; }
            set { _hoverButtonColor = value; }
        }
        public Color HoverForeColor
        {
            get { return _hoverForeColor; }
            set { _hoverForeColor = value; }
        }


        protected override void OnMouseDown(MouseEventArgs mevent)
        {
            base.OnMouseDown(mevent);
        }
        protected override void OnMouseUp(MouseEventArgs mevent)
        {
            base.OnMouseUp(mevent);
        }
        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
        }
        protected override void OnMouseMove(MouseEventArgs mevent)
        {
            base.OnMouseMove(mevent);
        }
        protected override void OnPaint(PaintEventArgs pevent)
        {
            base.OnPaint(pevent);

            Graphics g = pevent.Graphics;
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;

            using (var backbrush = new SolidBrush(BackColor))
            using (var textbrush = new SolidBrush(ForeColor))
            {
                g.FillRectangle(backbrush, ClientRectangle);

                using (StringFormat format = new StringFormat())
                {
                    format.LineAlignment = StringAlignment.Center;
                    format.Alignment = StringAlignment.Center;
                    g.DrawString(Text, Font, textbrush, ClientRectangle, format);
                }
            }
        }
    }
}
