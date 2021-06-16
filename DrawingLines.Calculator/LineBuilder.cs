using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading;

namespace DrawingLines.Calculator
{
    public class LineBuilder
    {
        private readonly HashSet<Point> _takenPoints = new();
        private readonly Point _leftTopCorner = new(0, 0);
        private Point _rightBottomCorner;

        public LineBuilder(Point corner)
        {
            _rightBottomCorner = corner;
        }

        public void SetSize(Point corner)
        {
            _rightBottomCorner = corner;
        }

        public List<Point> BuildLine(Point start, Point end)
        {
            var points = new List<Point>();
            points.Add(start);
            var lastPoint = start;
            byte counter = 0;
            while (!points.Contains(end))
            {
                lastPoint = GetNextPoint(lastPoint, end, points, start);
                if (start.Equals(lastPoint))
                {
                    if (counter++ > 4)
                    {
                        throw new Exception("No path available");
                    }
                    continue;
                }
                points.Add(lastPoint);
            }
            var minimumRequiredPoints = new List<Point>();
            minimumRequiredPoints.Add(points[^1]);
            while (!minimumRequiredPoints.Contains(start))
            {
                var firstNeighbour = points.First(p => IsNeighbour(p, minimumRequiredPoints.Last()));
                minimumRequiredPoints.Add(firstNeighbour);
                _takenPoints.Add(firstNeighbour);
            }
            return minimumRequiredPoints;
        }

        private static bool IsNeighbour(Point point1, Point point2)
        {
            return (point1.X - point2.X == 0 && Math.Abs(point1.Y - point2.Y) == 1)
                || (point1.Y - point2.Y == 0 && Math.Abs(point1.X - point2.X) == 1);
        }

        private bool IsOnScreen(Point point)
        {
            return point.X > _leftTopCorner.X && point.Y > _leftTopCorner.Y
                && point.X < _rightBottomCorner.X && point.Y < _rightBottomCorner.Y;
        }

        private bool TryMoving(List<Point> currentPoints, out Point nextPoint, Point point)
        {
            if (!_takenPoints.Contains(point) && !currentPoints.Contains(point) && IsOnScreen(point))
            {
                nextPoint = point;
                return true;
            }
            nextPoint = default;
            return false;
        }

        private bool TryMovingLeft(Point lastPoint, List<Point> currentPoints, out Point nextPoint)
        {
            var point = new Point(lastPoint.X - 1, lastPoint.Y);
            return TryMoving(currentPoints, out nextPoint, point);
        }

        private bool TryMovingUp(Point lastPoint, List<Point> currentPoints, out Point nextPoint)
        {
            var point = new Point(lastPoint.X, lastPoint.Y - 1);
            return TryMoving(currentPoints, out nextPoint, point);
        }

        private bool TryMovingDown(Point lastPoint, List<Point> currentPoints, out Point nextPoint)
        {
            var point = new Point(lastPoint.X, lastPoint.Y + 1);
            return TryMoving(currentPoints, out nextPoint, point);
        }

        private bool TryMovingRight(Point lastPoint, List<Point> currentPoints, out Point nextPoint)
        {
            var point = new Point(lastPoint.X + 1, lastPoint.Y);
            return TryMoving(currentPoints, out nextPoint, point);
        }

        private Point GetNextPoint(Point lastPoint, Point nextPoint, List<Point> currentPoints, Point startingPoint)
        {
            Point next;
            if(lastPoint.Y > nextPoint.Y)
            {
                if(TryMovingUp(lastPoint, currentPoints, out next))
                {
                    return next;
                }
                else
                {
                    if (lastPoint.X > nextPoint.X)
                    {
                        if(TryMovingLeft(lastPoint, currentPoints, out next))
                        {
                            return next;
                        }
                        else
                        {
                            if(TryMovingRight(lastPoint, currentPoints, out next))
                            {
                                return next;
                            }
                            else
                            {
                                if(TryMovingDown(lastPoint, currentPoints, out next))
                                {
                                    return next;
                                }
                                else
                                {
                                    return startingPoint;
                                }    
                            }
                        }
                    }
                    else
                    {
                        if (TryMovingRight(lastPoint, currentPoints, out next))
                        {
                            return next;
                        }
                        else
                        {
                            if (TryMovingLeft(lastPoint, currentPoints, out next))
                            {
                                return next;
                            }
                            else
                            {
                                if (TryMovingDown(lastPoint, currentPoints, out next))
                                {
                                    return next;
                                }
                                else
                                {
                                    return startingPoint;
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                if (lastPoint.Y == nextPoint.Y)
                {
                    if (lastPoint.X > nextPoint.X)
                    {
                        if (TryMovingLeft(lastPoint, currentPoints, out next))
                        {
                            return next;
                        }
                        else
                        {
                            if (TryMovingUp(lastPoint, currentPoints, out next))
                            {
                                return next;
                            }
                            else
                            {
                                if (TryMovingDown(lastPoint, currentPoints, out next))
                                {
                                    return next;
                                }
                                else
                                {
                                    if (TryMovingRight(lastPoint, currentPoints, out next))
                                    {
                                        return next;
                                    }
                                    else
                                    {
                                        return startingPoint;
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        if (TryMovingRight(lastPoint, currentPoints, out next))
                        {
                            return next;
                        }
                        else
                        {
                            if (TryMovingUp(lastPoint, currentPoints, out next))
                            {
                                return next;
                            }
                            else
                            {
                                if (TryMovingDown(lastPoint, currentPoints, out next))
                                {
                                    return next;
                                }
                                else
                                {
                                    if (TryMovingLeft(lastPoint, currentPoints, out next))
                                    {
                                        return next;
                                    }
                                    else
                                    {
                                        return startingPoint;
                                    }
                                }
                            }
                        }
                    }
                }
                else
                {
                    if (TryMovingDown(lastPoint, currentPoints, out next))
                    {
                        return next;
                    }
                    else
                    {
                        if (lastPoint.X > nextPoint.X)
                        {
                            if (TryMovingLeft(lastPoint, currentPoints, out next))
                            {
                                return next;
                            }
                            else
                            {
                                if (TryMovingRight(lastPoint, currentPoints, out next))
                                {
                                    return next;
                                }
                                else
                                {
                                    if (TryMovingUp(lastPoint, currentPoints, out next))
                                    {
                                        return next;
                                    }
                                    else
                                    {
                                        return startingPoint;
                                    }
                                }
                            }
                        }
                        else
                        {
                            if (TryMovingRight(lastPoint, currentPoints, out next))
                            {
                                return next;
                            }
                            else
                            {
                                if (TryMovingLeft(lastPoint, currentPoints, out next))
                                {
                                    return next;
                                }
                                else
                                {
                                    if (TryMovingUp(lastPoint, currentPoints, out next))
                                    {
                                        return next;
                                    }
                                    else
                                    {
                                        return startingPoint;
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}
