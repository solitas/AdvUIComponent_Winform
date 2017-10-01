using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Rootech.UI.Component
{
    public static class GraphicsExtension
    {
        public static GraphicsPath GetRoundPath(RectangleF r, float depth)
        {
            GraphicsPath graphPath = new GraphicsPath();

            graphPath.AddArc(r.X, r.Y, depth, depth, 180, 90);
            graphPath.AddArc(r.X + r.Width - depth, r.Y, depth, depth, 270, 90);
            graphPath.AddArc(r.X + r.Width - depth, r.Y + r.Height - depth, depth, depth, 0, 90);
            graphPath.AddArc(r.X, r.Y + r.Height - depth, depth, depth, 90, 90);
            graphPath.AddLine(r.X, r.Y + r.Height - depth, r.X, r.Y + depth / 2);

            return graphPath;
        }
        public static GraphicsPath GetTopRoundRectangle(Rectangle rectangle, int radius)
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
        public static void DrawRoundRect(this Graphics g, Pen pen, RectangleF bounds, float radius)
        {
            DrawRoundRect(g, pen, bounds.X, bounds.Y, bounds.Width, bounds.Height, radius);
        }
        public static void DrawRoundRect(this Graphics g, Pen pen, float x, float y, float width, float height, float depth)
        {
            var graphicsPath = GetRoundPath(new Rectangle((int)x, (int)y, (int)width, (int)height), depth);
            g.DrawPath(pen, graphicsPath);
        }

        public static void FillRoundRect(this Graphics g, Brush brush, RectangleF r, float depth)
        {
            var graphicsPath = GetRoundPath(r, depth);
            g.FillPath(brush, graphicsPath);
        }

        public static void DrawString(this Graphics g, string text, Font font, Brush brush, RectangleF rect, StringFormat format, bool allowNarrowSetWidth)
        {
            SizeF sz2 = g.MeasureString(text, font);
            float width = sz2.Width;

            // 글자 장평을 줄이는 방법
            if (width > rect.Width && allowNarrowSetWidth)
            {
                float mag = rect.Width / width;

                g.TranslateTransform(rect.Left, 0f);
                g.TranslateTransform(-rect.Width * mag * 0.5f, 0f);
                g.ScaleTransform(mag, 1f);
                g.TranslateTransform(rect.Width * 0.5f, 0f);
                g.TranslateTransform(-rect.Left, 0f);

                RectangleF rect2 = rect;
                rect2.Width = rect2.Width / mag;
                g.DrawString(text, font, brush, rect2, format);

                g.ResetTransform();
            }
            else
            {
                g.DrawString(text, font, brush, rect, format);
            }
        }

        public static void DrawStringWithGraphicsPath(this Graphics g, string text, Font font, Brush brush, RectangleF rect, StringFormat format, bool allowNarrowSetWidth = false)
        {
            var state = g.Save();

            SizeF sz2 = g.MeasureString(text, font);
            float width = sz2.Width;

            if (width > rect.Width && allowNarrowSetWidth)
            {
                float mag = rect.Width / width;

                g.TranslateTransform(rect.Left, 0f);
                g.TranslateTransform(-rect.Width * mag * 0.5f, 0f);
                g.ScaleTransform(mag, 1f);
                g.TranslateTransform(rect.Width * 0.5f, 0f);
                g.TranslateTransform(-rect.Left, 0f);

                rect.Width = rect.Width / mag;
            }

            float emSize = g.DpiY * font.Size / 72; // 다른의견 = font.Size + 4;
            GraphicsPath gp = new GraphicsPath();
            gp.AddString(text, font.FontFamily, (int)font.Style, emSize, rect, format);

            g.FillPath(brush, gp);
            g.Restore(state);
        }
        /// <summary>
        /// test
        /// <example>
        /// int pointsCount = (int)Math.Abs(sweepAngle * degreeFactor);
        /// int sign = Math.Sign(sweepAngle);
        /// PointF[] points = new PointF[pointsCount];
        /// for (int i = 0; i < pointsCount; ++i)
        /// {
        ///    var pointX = 
        ///       (float)(circlePoint.X  
        ///               + Math.Cos(startAngle + sign * (double)i / degreeFactor)  
        ///               * radius);
        ///    var pointY = 
        ///       (float)(circlePoint.Y 
        ///               + Math.Sin(startAngle + sign * (double)i / degreeFactor) 
        ///              * radius);
        ///    points[i] = new PointF(pointX, pointY);
        /// }
        /// </example>
        /// </summary>

        /// <param name="graphics"></param>
        /// <param name="angularPoint"></param>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <param name="radius"></param>
        public static void DrawRoundedCorner(this Graphics graphics, PointF angularPoint, PointF p1, PointF p2, float radius)
        {
            //Vector 1
            double dx1 = angularPoint.X - p1.X;
            double dy1 = angularPoint.Y - p1.Y;

            //Vector 2
            double dx2 = angularPoint.X - p2.X;
            double dy2 = angularPoint.Y - p2.Y;

            //Angle between vector 1 and vector 2 divided by 2
            double angle = (Math.Atan2(dy1, dx1) - Math.Atan2(dy2, dx2)) / 2;

            // The length of segment between angular point and the
            // points of intersection with the circle of a given radius
            double tan = Math.Abs(Math.Tan(angle));
            double segment = radius / tan;

            //Check the segment
            double length1 = GetLength(dx1, dy1);
            double length2 = GetLength(dx2, dy2);

            double length = Math.Min(length1, length2);

            if (segment > length)
            {
                segment = length;
                radius = (float)(length * tan);
            }

            // Points of intersection are calculated by the proportion between 
            // the coordinates of the vector, length of vector and the length of the segment.
            var p1Cross = GetProportionPoint(angularPoint, segment, length1, dx1, dy1);
            var p2Cross = GetProportionPoint(angularPoint, segment, length2, dx2, dy2);

            // Calculation of the coordinates of the circle 
            // center by the addition of angular vectors.
            double dx = angularPoint.X * 2 - p1Cross.X - p2Cross.X;
            double dy = angularPoint.Y * 2 - p1Cross.Y - p2Cross.Y;

            double L = GetLength(dx, dy);
            double d = GetLength(segment, radius);

            var circlePoint = GetProportionPoint(angularPoint, d, L, dx, dy);

            //StartAngle and EndAngle of arc
            var startAngle = Math.Atan2(p1Cross.Y - circlePoint.Y, p1Cross.X - circlePoint.X);
            var endAngle = Math.Atan2(p2Cross.Y - circlePoint.Y, p2Cross.X - circlePoint.X);

            //Sweep angle
            var sweepAngle = endAngle - startAngle;

            //Some additional checks
            if (sweepAngle < 0)
            {
                startAngle = endAngle;
                sweepAngle = -sweepAngle;
            }

            if (sweepAngle > Math.PI)
                sweepAngle = Math.PI - sweepAngle;

            //Draw result using graphics
            var pen = new Pen(Color.Black);

            graphics.Clear(Color.White);
            graphics.SmoothingMode = SmoothingMode.AntiAlias;

            graphics.DrawLine(pen, p1, p1Cross);
            graphics.DrawLine(pen, p2, p2Cross);

            var left = circlePoint.X - radius;
            var top = circlePoint.Y - radius;
            var diameter = 2 * radius;
            var degreeFactor = 180 / Math.PI;

            graphics.DrawArc(pen, left, top, diameter, diameter,
                             (float)(startAngle * degreeFactor),
                             (float)(sweepAngle * degreeFactor));
        }

        private static double GetLength(double dx, double dy)
        {
            return Math.Sqrt(dx * dx + dy * dy);
        }

        private static PointF GetProportionPoint(PointF point, double segment, double length, double dx, double dy)
        {
            double factor = segment / length;

            return new PointF((float)(point.X - dx * factor),
                              (float)(point.Y - dy * factor));
        }

        public static Font GetFont(Graphics g, string text, Font baseFont, Rectangle rect, float ratio = 1.0f)
        {
            float factor = 1.0f, factorX, factorY;
            var sz = g.MeasureString(text, baseFont);

            if (sz.Width > 0 && sz.Height > 0)
            {
                factorX = (float)(rect.Width) / (float)sz.Width;
                factorY = (float)(rect.Height) / (float)sz.Height;
                if (factorX > factorY)
                {
                    factor = factorY;
                }
                else
                {
                    factor = factorX;
                }
            }

            float s = baseFont.SizeInPoints * (factor) - 1;
            s *= ratio;

            Font f = new Font(baseFont.Name, s);

            return f;
        }
    }
}


