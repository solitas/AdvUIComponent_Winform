using System;
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Rootech.UI.Component
{
    public class CCTabControl : TabControl, IControl
    {
        public int Depth { set; get; }

        public MouseButtonState MouseButtonState { set; get; }

        public ComponentVisualization Visualization { set; get; }
        private Color _backColor;
        [Browsable(true)]
        public Color BackgroundColor
        {
            set
            {
                if (_backColor != value)
                {
                    _backColor = value;
                    Refresh();
                }
            }
            get
            {
                return _backColor;
            }
        }

        public CCTabControl()
        {
            Visualization = ComponentVisualization.Instance;
            Font = Visualization.ROBOTO_MEDIUM_10;

            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            SetStyle(ControlStyles.Opaque, true);
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);

            SizeMode = TabSizeMode.Normal;
            Padding = new Point(0, 0);
            BackgroundColor = Color.FromArgb(64, 64, 64);
            DrawMode = TabDrawMode.OwnerDrawFixed;

            CalcTabItemSize();
        }
        protected override void OnControlAdded(ControlEventArgs e)
        {
            base.OnControlAdded(e);
            Console.WriteLine("Control Added");
            e.Control.TextChanged += (obj, args) => CalcTabItemSize();

        }

        public int TabItemStripHeight
        {
            get
            {
                int margin = 5;
                return ItemSize.Height + margin;
            }
        }

        public override Rectangle DisplayRectangle
        {
            get
            {
                int margin = 0;
                int height = TabItemStripHeight + margin;
                return new Rectangle(margin, height, Width - margin * 2 - 2, Height - height - 2);
            }
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            const int leftMargin = 0;
            // Display Rectangle
            Rectangle backRect = new Rectangle(leftMargin, TabItemStripHeight - 3, Width - 3, Height - TabItemStripHeight + 2);
            g.FillRectangle(Brushes.White, backRect);

            // Tab Rectangle
            for (int i = 0; i < TabPages.Count; i++)
            {
                bool selected = (SelectedIndex == i);

                Rectangle rect = GetTabRect(i);

                Console.WriteLine("OnPaint [{0}] - Bounds({1})", i, rect);

                if (rect == Rectangle.Empty)
                    continue;
                if (rect.Width > 0 && rect.Height > 0)
                {
                    DrawTabItem(g, TabPages[i].Text, rect, selected);
                }
            }
        }
        protected override void OnSelectedIndexChanged(EventArgs e)
        {
            base.OnSelectedIndexChanged(e);
        }

        protected virtual void DrawTabItem(Graphics g, string text, Rectangle rect, bool selected)
        {
            Color backColor = selected ? Visualization.ColorScheme.LightPrimaryColor : Visualization.GetApplicationBackgroundColor();
            Color foreColor = selected ? Visualization.GetApplicationBackgroundColor() : Visualization.GetSecondaryTextColor();
            using (Pen pen = new Pen(Color.Orange, 1))
            using (Brush backBrush = new SolidBrush(backColor))
            using (SolidBrush foreBrush = new SolidBrush(foreColor))
            {
                g.FillRectangle(backBrush, rect);
                g.DrawString(text, Font, foreBrush, rect);
            }
        }
        private ArrayList _tabLength = new ArrayList();
        new public virtual Rectangle GetTabRect(int index)
        {
            if (index < 0)
            {
                return Rectangle.Empty;
            }

            var g = CreateGraphics();
            var size = GetMeasureSize(g, Font, Controls[index].Text);
            var margin = 0.5f ;
            float startX = 0.0f;

            startX = index == 0 ? 0 : margin;

            for (int i = 0; i < index; i++)
            {
                if (i != 0)
                {
                    startX += margin;
                }
                startX += Convert.ToSingle(_tabLength[i]);
            }

            Rectangle rect = new Rectangle(Convert.ToInt32(startX), 0, Convert.ToInt32(_tabLength[index]), 24);
            return rect;
        }
        private void CalcTabItemSize()
        {
            _tabLength.Clear();

            var g = CreateGraphics();
            for (int i = 0; i < Controls.OfType<TabPage>().Count(); i++)
            {
                var text = Controls[i].Text;
                if (string.IsNullOrEmpty(text)) continue;

                var size = GetMeasureSize(g, Font, text);
                if (!size.IsEmpty)
                {
                    _tabLength.Add(size.Width + 8.0f * 2.0f); //     |<--->text<--->|
                }
            }

            foreach (var l in _tabLength)
            {
                Console.WriteLine("l = {0}", l);
            }
            Console.WriteLine();
        }

        private SizeF GetMeasureSize(Graphics g, Font f, string Text)
        {
            var size = g.MeasureString(Text, f);
            return size;
        }
    }
}
