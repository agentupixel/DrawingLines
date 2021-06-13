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
            return (Start.X - End.X) == 0 ? 0 : (Start.Y - End.Y) / (Start.X - End.X);
        }

        public double GetB()
        {
            return Start.Y - GetA() * Start.X;
        }
    }
}
