using System;
using System.Drawing;

namespace DrawingLines.Calculator.Structures
{
    public struct Line
    {
        public Line(Point start, Point end)
        {
            Start = start;
            End = end;
        }

        public Point Start { get; }
        public Point End { get; }

        public Vector GetVector()
        {
            return new Vector(Start, End);
        }

        public double GetA()
        {
            return (Start.X - End.X) == 0 ? 0 : (double)(Start.Y - End.Y) / (Start.X - End.X);
        }

        public double GetB()
        {
            return Start.Y - GetA() * Start.X;
        }

        public bool Matches(Line other)
        {
            return Start.Equals(other.Start) && End.Equals(other.End)
                || End.Equals(other.Start) && Start.Equals(other.End);
        }
    }
}
