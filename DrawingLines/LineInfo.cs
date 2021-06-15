using System.Drawing;

namespace DrawingLines
{
    public struct LineInfo
    {
        public LineInfo(Color color, Point start, Point end)
        {
            Color = color;
            Start = start;
            End = end;
        }

        public Color Color { get; }
        public Point Start { get; }
        public Point End { get; }

        public bool Matches(LineInfo other)
        {
            return Start.Equals(other.Start) && End.Equals(other.End)
                || End.Equals(other.Start) && Start.Equals(other.End);
        }
    }
}
