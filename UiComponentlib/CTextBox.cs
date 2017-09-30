using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Rootech.UI.Component
{
    public class CTextBox : Control
    {
        private TextFormatFlags _textFlags = TextFormatFlags.Default;

        public Rectangle Bounds { set; get; }

        public CTextBox ()
            : base()
        {
            Bounds = new Rectangle(0, 0, 100, 30);
        }
        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);

            Bounds = new Rectangle(ClientRectangle.Left, ClientRectangle.Top, ClientRectangle.Width, ClientRectangle.Height);

            Invalidate();
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            if (TextBoxRenderer.IsSupported)
            {
                TextBoxRenderer.DrawTextBox(e.Graphics, Bounds, this.Text, this.Font, System.Windows.Forms.VisualStyles.TextBoxState.Normal);
            }
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);

            if (ImeMode == ImeMode.Hangul)
            {
                
            }
        }

        protected override void OnPreviewKeyDown(PreviewKeyDownEventArgs e)
        {
        }
    }
}
