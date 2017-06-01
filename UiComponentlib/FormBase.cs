using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using static Rootech.UI.Component.Win32Utility;

namespace Rootech.UI.Component
{
    public partial class FormBase : Form, IControl
    {
        private enum ResizeDirection
        {
            BottomLeft,
            Left,
            Right,
            BottomRight,
            Bottom,
            None
        }


        private enum ButtonState
        {
            XOver,
            MaxOver,
            MinOver,
            XDown,
            MaxDown,
            MinDown,
            None
        }
        private ButtonState _buttonState = ButtonState.None;
        private ResizeDirection _resizeDir;
        // constants
        private static int FORM_PADDING = ComponentVisualization.PADDING;
        private static int TITLE_BAR_BUTTON_HEIGHT = ComponentVisualization.TITLE_BAR_HEIGHT;

        private const int BORDER_WIDTH = 7;
        private const int MONITOR_DEFAULTTONEAREST = 2;
        private const uint TPM_LEFTALIGN = 0x0000;
        private const uint TPM_RETURNCMD = 0x0100;

        private bool _aeroEnabled;
        private bool _maximized;
        private bool _headerMouseDown;

        private Color _titleBarBackGround       /**/ = Color.FromArgb(36, 41, 46);
        private Color _dividerColor             /**/ = Color.FromArgb(31, 0, 0, 0);
        private Color _buttonHighlightColor     /**/ = SystemColors.ControlLight;
        private Color _buttonPressedColor       /**/ = SystemColors.ControlDarkDark;
        private Color _buttonInnerColor         /**/ = Color.FromArgb(255, 150, 150, 150);

        private Rectangle _titleBarBounds;
        private Rectangle _minButtonBounds;
        private Rectangle _maxButtonBounds;
        private Rectangle _xButtonBounds;
        private Rectangle _iconBounds;
        private Image _iconImage;

        private Size _previousSize;
        private Point _previousLocation;

        private readonly Cursor[] _resizeCursors = { Cursors.SizeNESW, Cursors.SizeWE, Cursors.SizeNWSE, Cursors.SizeWE, Cursors.SizeNS };

        private const int HTBOTTOMLEFT              /**/ = 16;
        private const int HTBOTTOMRIGHT             /**/ = 17;
        private const int HTLEFT                    /**/ = 10;
        private const int HTRIGHT                   /**/ = 11;
        private const int HTBOTTOM                  /**/ = 15;
        private const int HTTOP                     /**/ = 12;
        private const int HTTOPLEFT                 /**/ = 13;
        private const int HTTOPRIGHT                /**/ = 14;
        private const int WMSZ_TOP                  /**/ = 3;
        private const int WMSZ_TOPLEFT              /**/ = 4;
        private const int WMSZ_TOPRIGHT             /**/ = 5;
        private const int WMSZ_LEFT                 /**/ = 1;
        private const int WMSZ_RIGHT                /**/ = 2;
        private const int WMSZ_BOTTOM               /**/ = 6;
        private const int WMSZ_BOTTOMLEFT           /**/ = 7;
        private const int WMSZ_BOTTOMRIGHT          /**/ = 8;

        private readonly Dictionary<int, int> _resizingLocationsToCmd = new Dictionary<int, int>
        {
            {HTTOP,         WMSZ_TOP},
            {HTTOPLEFT,     WMSZ_TOPLEFT},
            {HTTOPRIGHT,    WMSZ_TOPRIGHT},
            {HTLEFT,        WMSZ_LEFT},
            {HTRIGHT,       WMSZ_RIGHT},
            {HTBOTTOM,      WMSZ_BOTTOM},
            {HTBOTTOMLEFT,  WMSZ_BOTTOMLEFT},
            {HTBOTTOMRIGHT, WMSZ_BOTTOMRIGHT}
        };
        public int Depth { set; get; }

        public MouseButtonState MouseButtonState { set; get; }

        public ComponentVisualization Visualization { set; get; }
        protected override CreateParams CreateParams
        {
            get
            {
                var par = base.CreateParams;
                // WS_SYSMENU: Trigger the creation of the system menu
                // WS_MINIMIZEBOX: Allow minimizing from taskbar
                par.Style = par.Style | WS_MINIMIZEBOX | WS_SYSMENU; // Turn on the WS_MINIMIZEBOX style flag
                return par;
            }
        }

        #region "Fileds related to appearance"
        public bool Sizable { get; set; }

        [Category("Appearance"), DefaultValue(typeof(Color), "ActiveBorder")]
        public Color TitleBarBackColor
        {
            get { return _titleBarBackGround; }
            set { _titleBarBackGround = value; }
        }

        public Image IconImage
        {
            set
            {
                _iconImage = value;
            }
            get
            {
                return _iconImage;
            }
        }
        #endregion


        // constructor
        public FormBase()
        {
            Visualization = ComponentVisualization.Instance;

            this.FormBorderStyle = FormBorderStyle.None;
            DoubleBuffered = true;
            Sizable = true;
            SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw, true);

            // This enables the form to trigger the MouseMove event even when mouse is over another control
            //Application.AddMessageFilter(new MouseMessageFilter());
            //MouseMessageFilter.MouseMove += OnGlobalMouseMove;
            var margin = this.DefaultMargin;
        }

        #region "code related to override methods"
        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);

            if (DesignMode || IsDisposed) return;


            if (m.Msg == WM_LBUTTONDBLCLK && (_titleBarBounds.Contains(PointToClient(Cursor.Position))) &&
                !(_minButtonBounds.Contains(PointToClient(Cursor.Position)) || _maxButtonBounds.Contains(PointToClient(Cursor.Position)) || _xButtonBounds.Contains(PointToClient(Cursor.Position))))
            {
                MaximizeWindow(!_maximized);
            }
            else if (m.Msg == WM_MOUSEMOVE && _maximized &&
                (_titleBarBounds.Contains(PointToClient(Cursor.Position))) &&
                !(_minButtonBounds.Contains(PointToClient(Cursor.Position)) || _maxButtonBounds.Contains(PointToClient(Cursor.Position)) || _xButtonBounds.Contains(PointToClient(Cursor.Position))))
            {
                if (_headerMouseDown)
                {
                    _maximized = false;
                    _headerMouseDown = false;

                    var mousePoint = PointToClient(Cursor.Position);
                    if (mousePoint.X < Width / 2)
                        Location = mousePoint.X < _previousSize.Width / 2 ?
                            new Point(Cursor.Position.X - mousePoint.X, Cursor.Position.Y - mousePoint.Y) :
                            new Point(Cursor.Position.X - _previousSize.Width / 2, Cursor.Position.Y - mousePoint.Y);
                    else
                        Location = Width - mousePoint.X < _previousSize.Width / 2 ?
                            new Point(Cursor.Position.X - _previousSize.Width + Width - mousePoint.X, Cursor.Position.Y - mousePoint.Y) :
                            new Point(Cursor.Position.X - _previousSize.Width / 2, Cursor.Position.Y - mousePoint.Y);

                    Size = _previousSize;
                    ReleaseCapture();
                    SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
                }
            }
            else if (m.Msg == WM_LBUTTONDOWN &&
                (_titleBarBounds.Contains(PointToClient(Cursor.Position))) &&
                !(_minButtonBounds.Contains(PointToClient(Cursor.Position)) || _maxButtonBounds.Contains(PointToClient(Cursor.Position)) || _xButtonBounds.Contains(PointToClient(Cursor.Position))))
            {
                if (!_maximized)
                {
                    ReleaseCapture();
                    SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
                }
                else
                {
                    _headerMouseDown = true;
                }
            }
            else if (m.Msg == WM_RBUTTONDOWN)
            {
                Point cursorPos = PointToClient(Cursor.Position);

                if (_titleBarBounds.Contains(cursorPos) && !_minButtonBounds.Contains(cursorPos) &&
                    !_maxButtonBounds.Contains(cursorPos) && !_xButtonBounds.Contains(cursorPos))
                {
                    // Show default system menu when right clicking titlebar
                    var id = TrackPopupMenuEx(GetSystemMenu(Handle, false), TPM_LEFTALIGN | TPM_RETURNCMD, Cursor.Position.X, Cursor.Position.Y, Handle, IntPtr.Zero);

                    // Pass the command as a WM_SYSCOMMAND message
                    SendMessage(Handle, WM_SYSCOMMAND, id, 0);
                }
            }
            else if (m.Msg == WM_NCLBUTTONDOWN)
            {
                // This re-enables resizing by letting the application know when the
                // user is trying to resize a side. This is disabled by default when using WS_SYSMENU.
                if (!Sizable) return;

                byte bFlag = 0;

                // Get which side to resize from
                if (_resizingLocationsToCmd.ContainsKey((int)m.WParam))
                    bFlag = (byte)_resizingLocationsToCmd[(int)m.WParam];

                if (bFlag != 0)
                    SendMessage(Handle, WM_SYSCOMMAND, 0xF000 | bFlag, (int)m.LParam);
            }
            else if (m.Msg == WM_LBUTTONUP)
            {
                _headerMouseDown = false;
            }

        }
        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            if (DesignMode)
            {
                RecreateHandle();
            }
            _titleBarBounds = new Rectangle(0, 0, this.Width, ComponentVisualization.TITLE_BAR_HEIGHT);
            _minButtonBounds = new Rectangle((Width - FORM_PADDING / 2) - 3 * TITLE_BAR_BUTTON_HEIGHT, 0, TITLE_BAR_BUTTON_HEIGHT, ComponentVisualization.TITLE_BAR_HEIGHT);
            _maxButtonBounds = new Rectangle((Width - FORM_PADDING / 2) - 2 * TITLE_BAR_BUTTON_HEIGHT, 0, TITLE_BAR_BUTTON_HEIGHT, ComponentVisualization.TITLE_BAR_HEIGHT);
            _xButtonBounds = new Rectangle((Width - FORM_PADDING / 2) - TITLE_BAR_BUTTON_HEIGHT, 0, TITLE_BAR_BUTTON_HEIGHT, ComponentVisualization.TITLE_BAR_HEIGHT);
            _iconBounds = new Rectangle(3, 5, 30, 15);
            RedrawWindow(this.Handle, IntPtr.Zero, IntPtr.Zero, RedrawWindowFlag.Frame | RedrawWindowFlag.UpdateNow | RedrawWindowFlag.Invalidate);

            Application.AddMessageFilter(new MouseMessageFilter());
            MouseMessageFilter.MouseMove += OnGlobalMouseMove;
        }
        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (DesignMode)
                return;

            CheckButtonState(e);

            if (e.Button == MouseButtons.Left && !_maximized)
            {
                ResizeForm(_resizeDir);
            }
            base.OnMouseDown(e);
        }
        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            if (DesignMode) return;

            if (Sizable)
            {
                var isChildUnderMouse = GetChildAtPoint(e.Location) != null;
                if (e.Location.X < BORDER_WIDTH && e.Location.Y > Height - BORDER_WIDTH && !isChildUnderMouse && !_maximized)
                {
                    _resizeDir = ResizeDirection.BottomLeft;
                    Cursor = Cursors.SizeNESW;
                }
                else if (e.Location.X < BORDER_WIDTH && !isChildUnderMouse && !_maximized)
                {
                    _resizeDir = ResizeDirection.Left;
                    Cursor = Cursors.SizeWE;
                }
                else if (e.Location.X > Width - BORDER_WIDTH && e.Location.Y > Height - BORDER_WIDTH && !isChildUnderMouse && !_maximized)
                {
                    _resizeDir = ResizeDirection.BottomRight;
                    Cursor = Cursors.SizeNWSE;
                }
                else if (e.Location.X > Width - BORDER_WIDTH && !isChildUnderMouse && !_maximized)
                {
                    _resizeDir = ResizeDirection.Right;
                    Cursor = Cursors.SizeWE;
                }
                else if (e.Location.Y > Height - BORDER_WIDTH && !isChildUnderMouse && !_maximized)
                {
                    _resizeDir = ResizeDirection.Bottom;
                    Cursor = Cursors.SizeNS;
                }
                else
                {
                    _resizeDir = ResizeDirection.None;

                    //Only reset the cursor when needed, this prevents it from flickering when a child control changes the cursor to its own needs
                    if (_resizeCursors.Contains(Cursor))
                    {
                        Cursor = Cursors.Default;
                    }
                }
            }
            CheckButtonState(e);
        }
        protected override void OnMouseUp(MouseEventArgs e)
        {
            if (DesignMode) return;
            CheckButtonState(e, true);

            base.OnMouseUp(e);
            ReleaseCapture();
        }
        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);

            if (DesignMode) return;
            _buttonState = ButtonState.None;
            Invalidate();
        }
        protected void OnGlobalMouseMove(object sender, MouseEventArgs e)
        {
            if (IsDisposed) return;

            var clientCursorPos = PointToClient(e.Location);
            var newE = new MouseEventArgs(MouseButtons.None, 0, clientCursorPos.X, clientCursorPos.Y, 0);
            OnMouseMove(newE);
        }
        protected void OnGlobalMouseDown(object sender, MouseEventArgs e)
        {
            if (IsDisposed) return;
            var clientCursorPos = PointToClient(e.Location);
            var newE = new MouseEventArgs(MouseButtons.None, 0, clientCursorPos.X, clientCursorPos.Y, 0);
            SendMessage(Handle, WM_LBUTTONDOWN, 0,0);
        }
        protected override void OnControlAdded(ControlEventArgs e)
        {
            base.OnControlAdded(e);

            if (e.Control is IControl)
            {
                var ctrl = e.Control as IControl;
                ctrl.Visualization = Visualization;
            }
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            var g = e.Graphics;

            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
            g.Clear(Visualization.GetApplicationBackgroundColor());
            g.FillRectangle(Visualization.ColorScheme.DarkPrimaryBrush, _titleBarBounds);
            // draw border
            using (var borderPen = new Pen(Visualization.GetDividersColor(), 1))
            {
                g.DrawLine(borderPen, new Point(0, _titleBarBounds.Bottom), new Point(0, Height - 2));
                g.DrawLine(borderPen, new Point(Width - 1, _titleBarBounds.Bottom), new Point(Width - 1, Height - 2));
                g.DrawLine(borderPen, new Point(0, Height - 1), new Point(Width - 1, Height - 1));
            }
            if (Icon != null)
            {
                int margin = 3;
                int x = margin;
                int y = margin;
                int iconSz = ComponentVisualization.TITLE_BAR_HEIGHT - margin * 2;
                g.DrawImage(Icon.ToBitmap(), new Rectangle(x, y, iconSz, iconSz));
            }
            PaintTitle(g);
        }
        #endregion

        private void CheckAeroEnabled()
        {
            if (Environment.OSVersion.Version.Major >= 6)
            {
                int enabled = 0;
                int response = DwmIsCompositionEnabled(ref enabled);
                _aeroEnabled = (enabled == 1) ? true : false;
            }
            else
            {
                _aeroEnabled = false;
            }
        }

        #region "Code related to draw form child controls"
        protected void PaintTitle(Graphics graphics)
        {
            var isShowMinimumButton = MinimizeBox && ControlBox;
            var isShowMaximumButton = MaximizeBox && ControlBox;
            var hoveredBrush = Visualization.GetFlatButtonHoverBackgroundBrush();
            var pressedBrush = Visualization.GetFlatButtonPressedBackgroundBrush();
            using (var xButtonHoverBrush = new SolidBrush(Color.FromArgb(150, 255, 0, 0)))
            {
                if (isShowMinimumButton && _buttonState == ButtonState.MinOver)
                {
                    graphics.FillRectangle(hoveredBrush, isShowMaximumButton ? _minButtonBounds : _maxButtonBounds);
                }
                if (isShowMinimumButton && _buttonState == ButtonState.MinDown)
                {
                    graphics.FillRectangle(pressedBrush, isShowMaximumButton ? _minButtonBounds : _maxButtonBounds);
                }
                if (_buttonState == ButtonState.MaxOver && isShowMaximumButton)
                    graphics.FillRectangle(hoveredBrush, _maxButtonBounds);

                if (_buttonState == ButtonState.MaxDown && isShowMaximumButton)
                    graphics.FillRectangle(pressedBrush, _maxButtonBounds);

                if (_buttonState == ButtonState.XOver && ControlBox)
                    graphics.FillRectangle(xButtonHoverBrush, _xButtonBounds);

                if (_buttonState == ButtonState.XDown && ControlBox)
                    graphics.FillRectangle(Brushes.DarkRed, _xButtonBounds);

                using (var formButtonsPen = new Pen(_buttonInnerColor, 2))
                {
                    // Minimize button.
                    if (isShowMinimumButton)
                    {
                        int x = isShowMaximumButton ? _minButtonBounds.X : _maxButtonBounds.X;
                        int y = isShowMaximumButton ? _minButtonBounds.Y : _maxButtonBounds.Y;

                        graphics.DrawLine(
                            formButtonsPen,
                            x + (int)(_minButtonBounds.Width * 0.33),
                            y + (int)(_minButtonBounds.Height * 0.66),
                            x + (int)(_minButtonBounds.Width * 0.66),
                            y + (int)(_minButtonBounds.Height * 0.66)
                       );
                    }

                    // Maximize button
                    if (isShowMaximumButton)
                    {
                        graphics.DrawRectangle(
                            formButtonsPen,
                            _maxButtonBounds.X + (int)(_maxButtonBounds.Width * 0.33),
                            _maxButtonBounds.Y + (int)(_maxButtonBounds.Height * 0.36),
                            (int)(_maxButtonBounds.Width * 0.39),
                            (int)(_maxButtonBounds.Height * 0.31)
                       );
                    }

                    // Close button
                    if (ControlBox)
                    {
                        graphics.DrawLine(
                            formButtonsPen,
                            _xButtonBounds.X + (int)(_xButtonBounds.Width * 0.33),
                            _xButtonBounds.Y + (int)(_xButtonBounds.Height * 0.33),
                            _xButtonBounds.X + (int)(_xButtonBounds.Width * 0.66),
                            _xButtonBounds.Y + (int)(_xButtonBounds.Height * 0.66)
                       );

                        graphics.DrawLine(
                            formButtonsPen,
                            _xButtonBounds.X + (int)(_xButtonBounds.Width * 0.66),
                            _xButtonBounds.Y + (int)(_xButtonBounds.Height * 0.33),
                            _xButtonBounds.X + (int)(_xButtonBounds.Width * 0.33),
                            _xButtonBounds.Y + (int)(_xButtonBounds.Height * 0.66));
                    }
                }
            }
        }
        #endregion

        private void CheckButtonState(MouseEventArgs e, bool up = false)
        {
            if (DesignMode) return;

            var oldState = _buttonState;

            bool showMin = MinimizeBox && ControlBox;
            bool showMax = MaximizeBox && ControlBox;

            if (e.Button == MouseButtons.Left && !up)
            {
                if (showMin && !showMax && _maxButtonBounds.Contains(e.Location))
                    _buttonState = ButtonState.MinDown;
                else if (showMin && showMax && _minButtonBounds.Contains(e.Location))
                    _buttonState = ButtonState.MinDown;
                else if (showMax && _maxButtonBounds.Contains(e.Location))
                    _buttonState = ButtonState.MaxDown;
                else if (ControlBox && _xButtonBounds.Contains(e.Location))
                    _buttonState = ButtonState.XDown;
                else
                    _buttonState = ButtonState.None;
            }
            else
            {
                if (showMin && !showMax && _maxButtonBounds.Contains(e.Location))
                {
                    _buttonState = ButtonState.MinOver;

                    if (oldState == ButtonState.MinDown && up)
                        WindowState = FormWindowState.Minimized;
                }
                else if (showMin && showMax && _minButtonBounds.Contains(e.Location))
                {
                    _buttonState = ButtonState.MinOver;

                    if (oldState == ButtonState.MinDown && up)
                        WindowState = FormWindowState.Minimized;
                }
                else if (MaximizeBox && ControlBox && _maxButtonBounds.Contains(e.Location))
                {
                    _buttonState = ButtonState.MaxOver;

                    if (oldState == ButtonState.MaxDown && up)
                    {
                        MaximizeWindow(!_maximized);
                    }
                }
                else if (ControlBox && _xButtonBounds.Contains(e.Location))
                {
                    _buttonState = ButtonState.XOver;

                    if (oldState == ButtonState.XDown && up)
                        Close();
                }
                else _buttonState = ButtonState.None;
            }

            if (oldState != _buttonState) Invalidate();
        }
        private void MaximizeWindow(bool maximize)
        {
            if (!MaximizeBox || !ControlBox) return;

            _maximized = maximize;

            if (maximize)
            {
                _previousSize = Size;
                _previousLocation = Location;
                WindowState = FormWindowState.Maximized;
            }
            else
            {
                WindowState = FormWindowState.Normal;
                Size = _previousSize;
                Location = _previousLocation;
            }
        }
        private void ResizeForm(ResizeDirection direction)
        {
            if (DesignMode) return;
            var dir = -1;
            switch (direction)
            {
                case ResizeDirection.BottomLeft:
                    dir = HTBOTTOMLEFT;
                    break;
                case ResizeDirection.Left:
                    dir = HTLEFT;
                    break;
                case ResizeDirection.Right:
                    dir = HTRIGHT;
                    break;
                case ResizeDirection.BottomRight:
                    dir = HTBOTTOMRIGHT;
                    break;
                case ResizeDirection.Bottom:
                    dir = HTBOTTOM;
                    break;
            }

            ReleaseCapture();

            if (dir != -1)
            {
                SendMessage(Handle, WM_NCLBUTTONDOWN, dir, 0);
            }
        }
        private void ResizeIconImage(Image image)
        {
            var width = image.Width;
            var height = image.Height;

            float ratio = width < height ? (float)width / (float)height : (float)height / (float)width;
            _iconBounds = new Rectangle(0, 0, (int)(height * ratio), TITLE_BAR_BUTTON_HEIGHT - 3);
        }
    }

    public class MouseMessageFilter : IMessageFilter
    {
        private const int WM_MOUSEMOVE = 0x0200;

        public static event MouseEventHandler MouseMove;
        public static event MouseEventHandler MouseDown;

        public bool PreFilterMessage(ref Message m)
        {

            if (m.Msg == WM_MOUSEMOVE)
            {
                if (MouseMove != null)
                {
                    int x = Control.MousePosition.X, y = Control.MousePosition.Y;

                    MouseMove(null, new MouseEventArgs(MouseButtons.None, 0, x, y, 0));
                }
            }
            else if (m.Msg == WM_LBUTTONDOWN)
            {
                int x = Control.MousePosition.X, y = Control.MousePosition.Y;
                MouseDown?.Invoke(null, new MouseEventArgs(MouseButtons.None, 0, x, y, 0));
            }
            return false;
        }
    }
}
