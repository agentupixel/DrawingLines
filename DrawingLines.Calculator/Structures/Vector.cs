using System.Drawing;

namespace DrawingLines.Calculator.Structures
{
    public struct Vector
    {
        public int X { get; }
        public int Y { get; }

        public Vector(Point start, Point end)
        {
            X = end.X - start.X;
            Y = end.Y - start.Y;
        }

        public int Multiply(Vector other)
        {
            return X * other.Y - Y * other.X;
        }
    }
}
