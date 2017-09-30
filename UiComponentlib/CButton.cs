using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.Windows.Forms;

namespace Rootech.UI.Component
{
    [ToolboxBitmap(typeof(CButton)), ToolboxItem(true), ToolboxItemFilter("System.Windows.Forms"), Description("Raises an event when the user clicks it.")]
    public class CButton : Button, IControl
    {
        private int _radius;
        private Color _backColor;
        private Color _foreColor;

        private Button _imageButton;
        private Color _innerBorderColor;
        private bool _isHovered;
        private bool _isKeyDown;
        private bool _isMouseDown;
        private Color _outerBorderColor;
        private Color _pressedColor;

        public CButton()
        {
            SetStyle(
                ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw |
                ControlStyles.SupportsTransparentBackColor | ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.Opaque, false);

            InitializeStatus();
            InitializeAppearance();
        }

        private bool IsPressed => _isKeyDown || _isMouseDown && _isHovered;
        [DefaultValue(typeof(Color), "Gray"), Category("Appearance"), Description("The back color of the control.")]
        public new virtual Color BackColor
        {
            get => _backColor;
            set
            {
                _backColor = value;
                Invalidate();
            }
        }
        [DefaultValue(typeof(Color), "Black"), Category("Appearance"), Description("The outter border color of the control.")]
        public Color OuterBorderColor
        {
            get => _outerBorderColor;
            set
            {
                _outerBorderColor = value;
                Invalidate();
            }
        }
        [DefaultValue(typeof(Color), "Black"), Category("Appearance"), Description("The inner border color of the control.")]
        public Color InnerBorderColor
        {
            get => _innerBorderColor;
            set
            {
                _innerBorderColor = value;
                Invalidate();
            }
        }

        public int Depth { set; get; }

        public MouseButtonState MouseButtonState { set; get; }

        public ComponentVisualization Visualization { set; get; }
        [DefaultValue(1), Category("Appearance"), Description()]
        public int Radius
        {
            get => _radius;
            set
            {
                _radius = value;
                Invalidate();
            }
        }

        private void InitializeStatus()
        {
            _isKeyDown = false;
            _isMouseDown = false;
            _isHovered = false;
        }

        private void InitializeAppearance()
        {
            _backColor = SystemColors.Control;
            _foreColor = SystemColors.ButtonFace;
            _outerBorderColor = SystemColors.AppWorkspace;
            _innerBorderColor = SystemColors.ActiveBorder;
            _pressedColor = SystemColors.HotTrack;
            _radius = 1;
        }

        protected override void OnMouseDown(MouseEventArgs mevent)
        {
            if (!_isMouseDown && mevent.Button == MouseButtons.Left)
            {
                _isMouseDown = true;
                Invalidate();
            }

            base.OnMouseDown(mevent);
        }

        protected override void OnMouseMove(MouseEventArgs mevent)
        {
            if (mevent.Button != MouseButtons.None)
                if (!ClientRectangle.Contains(mevent.X, mevent.Y))
                {
                    if (_isHovered)
                    {
                        _isHovered = false;
                        Invalidate();
                    }
                }
                else if (!_isHovered)
                {
                    _isHovered = true;
                    Invalidate();
                }
            base.OnMouseMove(mevent);
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            _isHovered = true;
            Invalidate();
            base.OnMouseEnter(e);
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            _isHovered = false;
            base.OnMouseLeave(e);
        }

        protected override void OnMouseUp(MouseEventArgs mevent)
        {
            if (_isMouseDown)
            {
                _isMouseDown = false;
                Invalidate();
            }

            base.OnMouseUp(mevent);
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
            {
                _isKeyDown = true;
                Invalidate();
            }
            base.OnKeyDown(e);
        }

        protected override void OnKeyUp(KeyEventArgs e)
        {
            if (_isKeyDown && e.KeyCode == Keys.Space)
            {
                _isKeyDown = false;
                Invalidate();
            }
            base.OnKeyUp(e);
        }

        protected override void OnLeave(EventArgs e)
        {
            base.OnLeave(e);
            _isKeyDown = _isMouseDown = false;
            Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            //base.OnPaint(e);
            DrawButtonBackgroundFromBuffer(e.Graphics);
            DrawForegroundFromButton(e);
        }

        protected override void OnGotFocus(EventArgs e)
        {
            base.OnGotFocus(e);
            Invalidate();
        }

        protected override void OnLostFocus(EventArgs e)
        {
            base.OnLostFocus(e);
            Invalidate();
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            Invalidate();
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            Invalidate();
        }

        protected virtual void DrawButtonBackgroundFromBuffer(Graphics graphics)
        {
            var frame = CreateBackgroundFrame(IsPressed, _isHovered, Focused, Enabled);
            graphics.DrawImage(frame, Point.Empty);
        }

        protected virtual void DrawIconImage(Graphics graphics)
        {
        }

        protected virtual void DrawForeground(Graphics graphics)
        {
            var state = graphics.Save();

            graphics.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;
            var bounds = ClientRectangle;

            var brush = new SolidBrush(_foreColor);

            var format = new StringFormat
            {
                Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Center
            };

            graphics.DrawString(Text, Font, brush, bounds, format);
            format.Dispose();

            graphics.Restore(state);
        }


        private Image CreateBackgroundFrame(bool pressed, bool hovered, bool focused, bool enabled)
        {
            var rect = ClientRectangle;
            if (rect.Width <= 0)
                rect.Width = 1;

            if (rect.Height <= 0)
                rect.Height = 1;

            var img = new Bitmap(rect.Width, rect.Height);

            using (var g = Graphics.FromImage(img))
            {
                g.Clear(Color.Transparent);
                DrawButtonBackground(g, rect, pressed, hovered, focused, enabled);
            }
            return img;
        }

        private void DrawButtonBackground(Graphics g, Rectangle rectangle, bool pressed, bool hovered, bool focused, bool enabled, float glowOpacity = 0)
        {
            var sm = g.SmoothingMode;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            #region " white border "

            var rect = rectangle;
            rect.Width--;
            rect.Height--;
            using (var bw = GraphicsExtension.CreateRoundRectangle(rect, _radius))
            {
                using (var p = new Pen(OuterBorderColor))
                {
                    g.DrawPath(p, bw);
                }
            }

            #endregion


            #region " content "

            using (var path = GraphicsExtension.CreateRoundRectangle(rect, _radius))
            {
                g.SetClip(path, CombineMode.Intersect);
                var opacity = hovered ? 0xcc : 0x7f;
                using (LinearGradientBrush br
                    = new LinearGradientBrush(new PointF(0, rect.Height-3), new PointF(0, rect.Height+20), Color.FromArgb(opacity, _backColor), Color.FromArgb((int)(opacity * 0.3f), _backColor)))
                //using (Brush br = new SolidBrush(Color.FromArgb(opacity, _backColor)))
                {
                    g.FillPath(br, path);
                }
                g.ResetClip();
            }

            #endregion

            #region " pressed "

            if (pressed)
                using (var path = GraphicsExtension.CreateRoundRectangle(rect, _radius))
                using (Brush brush = new SolidBrush(Color.FromArgb(0x1C, _pressedColor)))
                {
                    g.FillPath(brush, path);
                }

            #endregion

            #region "Focused"

            if (focused)
            {
                var focusedBound = rect;
                focusedBound.X += 2;
                focusedBound.Y += 2;
                focusedBound.Width -= 4;
                focusedBound.Height -= 4;

                using (var path = GraphicsExtension.CreateRoundRectangle(focusedBound, _radius))
                using (var pen = new Pen(Color.FromArgb(50, 50, 250)))
                {
                    pen.DashCap = DashCap.Round;
                    pen.DashStyle = DashStyle.Dot;
                    pen.DashPattern = new[] { 1.0f, 1.0f };
                    g.DrawPath(pen, path);
                }
            }

            #endregion

            #region " black border "

            using (var path = GraphicsExtension.CreateRoundRectangle(rect, _radius))
            {
                using (var p = new Pen(InnerBorderColor))
                {
                    g.DrawPath(p, path);
                }
            }

            #endregion

            g.SmoothingMode = sm;
        }

        private void DrawForegroundFromButton(PaintEventArgs pevent)
        {
            if (_imageButton == null)
            {
                _imageButton = new Button { Parent = new TransparentControl() };
                _imageButton.SuspendLayout();
                _imageButton.BackColor = Color.Transparent;
                _imageButton.FlatAppearance.BorderSize = 0;
                _imageButton.FlatStyle = FlatStyle.Flat;
            }
            else
            {
                _imageButton.SuspendLayout();
            }
            _imageButton.AutoEllipsis = AutoEllipsis;
            if (Enabled)
                _imageButton.ForeColor = ForeColor;
            else
                _imageButton.ForeColor = Color.FromArgb((3 * ForeColor.R + _backColor.R) >> 2,
                    (3 * ForeColor.G + _backColor.G) >> 2,
                    (3 * ForeColor.B + _backColor.B) >> 2);
            _imageButton.Font = Font;
            _imageButton.RightToLeft = RightToLeft;
            _imageButton.Image = Image;
            if (Image != null && !Enabled)
            {
                var size = Image.Size;
                var newColorMatrix = new float[5][];
                newColorMatrix[0] = new[] { 0.2125f, 0.2125f, 0.2125f, 0f, 0f };
                newColorMatrix[1] = new[] { 0.2577f, 0.2577f, 0.2577f, 0f, 0f };
                newColorMatrix[2] = new[] { 0.0361f, 0.0361f, 0.0361f, 0f, 0f };
                var arr = new float[5];
                arr[3] = 1f;
                newColorMatrix[3] = arr;
                newColorMatrix[4] = new[] { 0.38f, 0.38f, 0.38f, 0f, 1f };
                var matrix = new ColorMatrix(newColorMatrix);
                var disabledImageAttr = new ImageAttributes();
                disabledImageAttr.ClearColorKey();
                disabledImageAttr.SetColorMatrix(matrix);
                _imageButton.Image = new Bitmap(Image.Width, Image.Height);
                using (var gr = Graphics.FromImage(_imageButton.Image))
                {
                    gr.DrawImage(Image, new Rectangle(0, 0, size.Width, size.Height), 0, 0, size.Width, size.Height,
                        GraphicsUnit.Pixel, disabledImageAttr);
                }
            }
            _imageButton.ImageAlign = ImageAlign;
            _imageButton.ImageIndex = ImageIndex;
            _imageButton.ImageKey = ImageKey;
            _imageButton.ImageList = ImageList;
            _imageButton.Padding = Padding;
            _imageButton.Size = Size;
            _imageButton.Text = Text;
            _imageButton.TextAlign = TextAlign;
            _imageButton.TextImageRelation = TextImageRelation;
            _imageButton.UseCompatibleTextRendering = UseCompatibleTextRendering;
            _imageButton.UseMnemonic = UseMnemonic;
            _imageButton.ResumeLayout();
            InvokePaint(_imageButton, pevent);
            if (_imageButton.Image != null && _imageButton.Image != Image)
            {
                _imageButton.Image.Dispose();
                _imageButton.Image = null;
            }
        }

        private class TransparentControl : Control
        {
            protected override void OnPaintBackground(PaintEventArgs pevent)
            {
            }

            protected override void OnPaint(PaintEventArgs e)
            {
            }
        }
    }
}