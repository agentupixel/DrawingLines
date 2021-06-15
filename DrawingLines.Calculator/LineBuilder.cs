using DrawingLines.Calculator.Structures;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace DrawingLines.Calculator
{
    public class LineBuilder
    {
        private readonly IList<Polyline> _existingLines;
        private int _identifier = 0;
        public IEnumerable<Polyline> Lines => _existingLines;

        public LineBuilder()
        {
            _existingLines = new List<Polyline>();
        }

        private IEnumerable<Line> BuildSegments(Point start, Point end, Point? previousPoint = null)
        {
            Line newLine = new(start, end);
            var crossingLines = GetCrossingLines(start, end, previousPoint);
            if (!crossingLines.Any())
            {
                Console.WriteLine($"Start: {newLine.Start}, end: {newLine.End}");
                return new Line[] { newLine };
            }
            else
            {
                var nearestCrossingLine = GetNearestCrossingLine(newLine, crossingLines);
                if(start.Equals(nearestCrossingLine.Start)
                    || nearestCrossingLine.Matches(new Line(start, nearestCrossingLine.Start)))
                {
                    Point midpoint = new(
                        (int)Math.Round((nearestCrossingLine.Start.X + nearestCrossingLine.End.X) / 2D),
                        (int)Math.Round((nearestCrossingLine.Start.Y + nearestCrossingLine.End.Y) / 2D));
                //add pixel, check if crosses, if true remove pixel and build segments
                }
                return BuildSegments(start, nearestCrossingLine.Start)
                    .Concat(BuildSegments(nearestCrossingLine.Start, end, start));
            }
        }

        public Polyline Build(Point start, Point end)
        {
            var segments = BuildSegments(start, end).ToArray();
            var newPolyline = new Polyline(_identifier++, segments);
            _existingLines.Add(newPolyline);
            return newPolyline;
        }

        private static Line GetNearestCrossingLine(Line newLine, Line[] crossingLines)
        {
            double a1 = newLine.GetA();
            double b1 = newLine.GetB();
            var crossingDistances = new double[crossingLines.Length];
            for (short i = 0; i < crossingLines.Length; i++)
            {
                var line = crossingLines[i];
                var a2 = line.GetA();
                var b2 = line.GetB();
                int x = (int)Math.Round((b2 - b1) / (a1 - a2));
                int y = (int)Math.Round(a1 * x + b1);
                crossingDistances[i] = CalculateDistance(newLine.Start, new Point(x, y));
            }

            return crossingLines[Array.IndexOf(crossingDistances, crossingDistances.Min())];
        }

        private static double CalculateDistance(Point a, Point b)
        {
            return Math.Sqrt(Math.Pow(a.X - b.X, 2) + Math.Pow(a.Y - b.Y, 2));
        }

        private Line[] GetCrossingLines(Point start, Point end, Point? previousPoint = null)
        {
            Vector newLineVector = new(start, end);
            var lines = new List<Line>();
            foreach(var polyline in _existingLines)
            {
                foreach(var segment in polyline.Segments)
                {
                    Vector newStartOldStart = new(start, segment.Start);
                    Vector newStartOldEnd = new(start, segment.End);
                    Vector existingVector = segment.GetVector();
                    int v1 = newLineVector.Multiply(newStartOldStart);
                    int v2 = newLineVector.Multiply(newStartOldEnd);
                    int v3 = existingVector.Multiply(newStartOldStart);
                    int v4 = existingVector.Multiply(new Vector(end, segment.Start));
                    if(((v1 > 0 && v2 < 0 || v1 < 0 && v2 > 0)
                        && (v3 > 0 && v4 < 0 || v3 < 0 && v4 > 0)))
                    {
                        lines.Add(segment);
                        
                    }
                    if (v1 == 0 || v2 == 0 || v3 == 0 || v4 == 0)
                    {
                        var a = segment.GetA();
                        var b = segment.GetB();
                        if (start.Y == a * start.X + b)
                        {
                            if (start.Equals(segment.Start) || start.Equals(segment.End))
                            {
                                lines.Add(segment);
                            }
                        }
                        else
                        {
                            if (end.Y == a * end.X + b)
                            {
                                if (end.Equals(segment.Start) || end.Equals(segment.End))
                                {
                                    lines.Add(segment);
                                }
                            }
                            else
                            {
                                if (!previousPoint.HasValue)
                                {
                                    continue;
                                }
                                var crossingLines = GetCrossingLines(previousPoint.Value, end);
                                var crossedSegments = polyline.Segments.Count(s => crossingLines.Contains(s));
                                if (crossedSegments > 0 && crossedSegments < polyline.Segments.Length)
                                {
                                    lines.Add(segment);
                                }
                            }
                        }
                    }
                }
            }

            return lines.ToArray();
        }
    }
}
