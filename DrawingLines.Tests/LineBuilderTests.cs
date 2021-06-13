using DrawingLines.Calculator;
using DrawingLines.Calculator.Structures;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawingLines.Tests
{
    [TestFixture]
    public class LineBuilderTests
    {
        private LineBuilder _builder;

        [SetUp]
        public void SetUp()
        {
            _builder = new LineBuilder();
        }

        [Test]
        public void SingleLineTest()
        {
            // Given
            Point start = new(1, 1);
            Point end = new Point(7, 5);

            // When
            _builder.Build(start, end);

            // Then
            Assert.AreEqual(1, _builder.Lines.Count());
            var line = _builder.Lines.First();
            Assert.AreEqual(1, line.Segments.Length);
            Assert.AreEqual(0, line.Id);
            Assert.AreEqual(start, line.Segments[0].Start);
            Assert.AreEqual(end, line.Segments[0].End);
        }

        [Test]
        public void SomeNotCrossingLines()
        {
            // Given
            Tuple<Point, Point>[] lines = new Tuple<Point, Point>[]
            {
                new Tuple<Point, Point>(new Point(1, 1), new Point(10, 0)),
                new Tuple<Point, Point>(new Point(6, 4), new Point(3, 2)),
                new Tuple<Point, Point>(new Point(4, 5), new Point(2, 2)),
                new Tuple<Point, Point>(new Point(5, 6), new Point(8, 8)),
                new Tuple<Point, Point>(new Point(6, 4), new Point(3, 2))
            };

            // When
            foreach(var line in lines)
            {
                _builder.Build(line.Item1, line.Item2);
            }

            // Then
            Assert.AreEqual(5, lines.Length);
            for (byte i = 0; i < lines.Length; i++)
            {
                var line = _builder.Lines.ElementAt(i);
                Assert.AreEqual(1, line.Segments.Length);
                Assert.AreEqual(i, line.Id);
                Assert.AreEqual(lines[i].Item1, line.Segments[0].Start);
                Assert.AreEqual(lines[i].Item2, line.Segments[0].End);
            }
        }

        [Test]
        public void SimpleCrossingLines()
        {
            // Given
            Point a = new(2, 2);
            Point b = new(10, 7);
            Point c = new(3, 8);
            Point d = new(8, 1);

            // When
            _builder.Build(a, b);
            _builder.Build(c, d);

            // Then
            Assert.AreEqual(2, _builder.Lines.Count());
            var line1 = _builder.Lines.First();
            Assert.AreEqual(1, line1.Segments.Length);
            Assert.AreEqual(0, line1.Id);
            Assert.AreEqual(a, line1.Segments[0].Start);
            Assert.AreEqual(b, line1.Segments[0].End);
            var line2 = _builder.Lines.ElementAt(1);
            Assert.AreEqual(2, line2.Segments.Length);
            Assert.AreEqual(1, line2.Id);
            Assert.AreEqual(c, line2.Segments[0].Start);
            Assert.AreEqual(a, line2.Segments[0].End);
            Assert.AreEqual(a, line2.Segments[1].Start);
            Assert.AreEqual(d, line2.Segments[1].End);
        }
    }
}
