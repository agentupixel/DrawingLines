using System.Drawing;

namespace DrawingLines
{
    public struct LineInfo
    {
        public LineInfo(Color color, Point x, Point y)
        {
            Color = color;
            X = x;
            Y = y;
        }

        public Color Color { get; }
        public Point X { get; }
        public Point Y { get; }
    }
}
