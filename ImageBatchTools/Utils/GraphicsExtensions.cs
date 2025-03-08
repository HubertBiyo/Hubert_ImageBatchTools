using System.Drawing;
using System.Drawing.Drawing2D;

namespace ImageBatchTools.Utils
{
    public static class GraphicsExtensions
    {
        public static void AddRoundedRectangle(this GraphicsPath path, Rectangle bounds, int radius)
        {
            path.AddArc(bounds.X, bounds.Y, radius * 2, radius * 2, 180, 90);
            path.AddArc(bounds.Right - radius * 2, bounds.Y, radius * 2, radius * 2, 270, 90);
            path.AddArc(bounds.Right - radius * 2, bounds.Bottom - radius * 2, radius * 2, radius * 2, 0, 90);
            path.AddArc(bounds.X, bounds.Bottom - radius * 2, radius * 2, radius * 2, 90, 90);
            path.CloseFigure();
        }
    }
}