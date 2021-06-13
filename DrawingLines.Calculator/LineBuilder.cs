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
                return new Line[] { newLine };
            }
            else
            {
                var nearestCrossingLine = GetNearestCrossingLine(newLine, crossingLines);
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
                double x = (b2 - b1) / (a1 - a2);
                double y = a1 * x + b1;
                crossingDistances[i] = Math.Sqrt(Math.Pow(x - newLine.Start.X, 2) + Math.Pow(y - newLine.Start.Y, 2));           
            }

            return crossingLines[Array.IndexOf(crossingDistances, crossingDistances.Min())];
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
                        var a = segment.GetA();
                        var b = segment.GetB();
                        if (start.Y == a * start.X + b)
                        {
                            if ((!start.Equals(segment.Start) || !segment.Equals(polyline.Segments[0]))
                                || !start.Equals(segment.End) || !segment.Equals(polyline.Segments[^1]))
                            {
                                lines.Add(segment);
                            }
                        }
                        else
                        {
                            if (end.Y == a * end.X + b)
                            {
                                if ((!end.Equals(segment.Start) || !segment.Equals(polyline.Segments[0]))
                                    || !end.Equals(segment.End) || !segment.Equals(polyline.Segments[^1]))
                                {
                                    lines.Add(segment);
                                }
                            }
                            else
                            {
                                lines.Add(segment);
                            }
                        }
                    }
                    if (v1 == 0 || v2 == 0 || v3 == 0 || v4 == 0)
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

            return lines.ToArray();
        }
    }
}
