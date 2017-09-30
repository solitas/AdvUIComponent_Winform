using System.Drawing;
using System.Drawing.Drawing2D;

namespace Rootech.UI.Component
{
    public static class GraphicsExtension
    {

        public static void DrawRoundRectangle(this Graphics g, Color borderColor, Rectangle rect, int radius, float size = 1.0f)
        {
            using (var pen = new Pen(borderColor, size))
            {
                var path = CreateRoundRectangle(rect, radius);
                g.DrawPath(pen, path);
            }
        }

        public static void FillRoundRectangle(this Graphics g, Color backColor, Rectangle rect, int radius)
        {
            using (var brush = new SolidBrush(backColor))
            {
                var path = CreateRoundRectangle(rect, radius);
                g.FillPath(brush, path);
            }
        }
        public static GraphicsPath CreateTopRoundRectangle(Rectangle rectangle, int radius)
        {
            GraphicsPath path = new GraphicsPath();
            int l = rectangle.Left;
            int t = rectangle.Top;
            int w = rectangle.Width;
            int h = rectangle.Height;
            int d = radius << 1;
            path.AddArc(l, t, d, d, 180, 90); // topleft
            path.AddLine(l + radius, t, l + w - radius, t); // top
            path.AddArc(l + w - d, t, d, d, 270, 90); // topright
            path.AddLine(l + w, t + radius, l + w, t + h); // right
            path.AddLine(l + w, t + h, l, t + h); // bottom
            path.AddLine(l, t + h, l, t + radius); // left
            path.CloseFigure();
            return path;
        }

        public static GraphicsPath CreateBottomRadialPath(Rectangle rectangle)
        {
            GraphicsPath path = new GraphicsPath();
            RectangleF rect = rectangle;
            rect.X -= rect.Width * .35f;
            rect.Y -= rect.Height * .15f;
            rect.Width *= 1.7f;
            rect.Height *= 2.3f;
            path.AddEllipse(rect);
            path.CloseFigure();
            return path;
        }
        public static GraphicsPath CreateRoundRectangle(Rectangle rectangle, int radius)
        {
            var path = new GraphicsPath();
            var l = rectangle.Left;
            var t = rectangle.Top;
            var w = rectangle.Width;
            var h = rectangle.Height;
            var d = radius << 1;

            path.AddArc(l, t, d, d, 180, 90); // topleft
            path.AddLine(l + radius, t, l + w - radius, t); // top
            path.AddArc(l + w - d, t, d, d, 270, 90); // topright
            path.AddLine(l + w, t + radius, l + w, t + h - radius); // right
            path.AddArc(l + w - d, t + h - d, d, d, 0, 90); // bottomright
            path.AddLine(l + w - radius, t + h, l + radius, t + h); // bottom
            path.AddArc(l, t + h - d, d, d, 90, 90); // bottomleft
            path.AddLine(l, t + h - radius, l, t + radius); // left
            path.CloseFigure();

            return path;
        }
    }
}