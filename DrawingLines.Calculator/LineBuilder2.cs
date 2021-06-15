using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawingLines.Calculator
{
    public class LineBuilder2
    {
        private readonly HashSet<Point> _takenPoints = new();

        public List<Point> BuildLine(Point start, Point end)
        {
            var begin = start.Y <= end.Y ? start : end;
            var finish = start.Y <= end.Y ? end : start;
            var points = new List<Point>();
            points.Add(begin);
            var lastPoint = begin;
            while (!points.Contains(finish))
            {
                lastPoint = GetNextPoint(lastPoint, finish);
                points.Add(lastPoint);
            }
            foreach(var point in points)
            {
                _takenPoints.Add(point);
            }
            return points;
        }

        private Point GetNextPoint(Point lastPoint, Point nextPoint)
        {
            if(lastPoint.Y < nextPoint.Y)
            {
                var pointAbove = new Point(lastPoint.X, lastPoint.Y + 1);
                if (!_takenPoints.Contains(pointAbove))
                {
                    return pointAbove;
                }
                else
                {
                    var pointRight = new Point(lastPoint.X + 1, lastPoint.Y);
                    if (!_takenPoints.Contains(pointRight))
                    {
                        return pointRight;
                    }
                    else
                    {
                        var pointLeft = new Point(lastPoint.X - 1, lastPoint.Y);
                        if (!_takenPoints.Contains(pointLeft))
                        {
                            return pointLeft;
                        }
                        else
                        {
                            var pointBelow = new Point(lastPoint.X, lastPoint.Y - 1);
                            if (!_takenPoints.Contains(pointBelow))
                            {
                                return pointBelow;
                            }
                            else
                            {
                                throw new Exception("No path available");
                            }
                        }
                    }
                }
            }
            else
            {
                if(lastPoint.Y == nextPoint.Y)
                {
                    if (lastPoint.X < nextPoint.X)
                    {
                        var pointRight = new Point(lastPoint.X + 1, lastPoint.Y);
                        if (!_takenPoints.Contains(pointRight))
                        {
                            return pointRight;
                        }
                        else
                        {
                            var pointAbove = new Point(lastPoint.X, lastPoint.Y + 1);
                            if (!_takenPoints.Contains(pointAbove))
                            {
                                return pointAbove;
                            }
                            else
                            {
                                var pointLeft = new Point(lastPoint.X - 1, lastPoint.Y);
                                if (!_takenPoints.Contains(pointLeft))
                                {
                                    return pointLeft;
                                }
                                else
                                {
                                    var pointBelow = new Point(lastPoint.X, lastPoint.Y - 1);
                                    if (!_takenPoints.Contains(pointBelow))
                                    {
                                        return pointBelow;
                                    }
                                    else
                                    {
                                        throw new Exception("No path available");
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        var pointLeft = new Point(lastPoint.X - 1, lastPoint.Y);
                        if (!_takenPoints.Contains(pointLeft))
                        {
                            return pointLeft;
                        }
                        else
                        {
                            var pointAbove = new Point(lastPoint.X, lastPoint.Y + 1);
                            if (!_takenPoints.Contains(pointAbove))
                            {
                                return pointAbove;
                            }
                            else
                            {
                                var pointRight = new Point(lastPoint.X + 1, lastPoint.Y);
                                if (!_takenPoints.Contains(pointRight))
                                {
                                    return pointRight;
                                }
                                else
                                {
                                    var pointBelow = new Point(lastPoint.X, lastPoint.Y - 1);
                                    if (!_takenPoints.Contains(pointBelow))
                                    {
                                        return pointBelow;
                                    }
                                    else
                                    {
                                        throw new Exception("No path available");
                                    }
                                }
                            }
                        }
                    }
                }
                else
                {
                    var pointBelow = new Point(lastPoint.X, lastPoint.Y - 1);
                    if (!_takenPoints.Contains(pointBelow))
                    {
                        return pointBelow;
                    }
                    else
                    {
                        var pointAbove = new Point(lastPoint.X, lastPoint.Y + 1);
                        if (!_takenPoints.Contains(pointAbove))
                        {
                            return pointAbove;
                        }
                        else
                        {
                            var pointRight = new Point(lastPoint.X + 1, lastPoint.Y);
                            if (!_takenPoints.Contains(pointRight))
                            {
                                return pointRight;
                            }
                            else
                            {
                                var pointLeft = new Point(lastPoint.X - 1, lastPoint.Y);
                                if (!_takenPoints.Contains(pointLeft))
                                {
                                    return pointLeft;
                                }
                                else
                                {
                                    throw new Exception("No path available");
                                }
                            }
                        }
                    }
                }
            }
        }

        private enum Direction
        {
            Top,
            Left,
            Right,
            Bottom
        }
    }
}
