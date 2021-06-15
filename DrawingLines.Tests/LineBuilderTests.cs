using DrawingLines.Calculator;
using NUnit.Framework;
using System;
using System.Drawing;

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
            Point end = new(7, 5);

            // When
            var line = _builder.Build(start, end);
            
            // Then
            Assert.AreEqual(1, line.Segments.Length);
            Assert.AreEqual(0, line.Id);
            Assert.AreEqual(start, line.Segments[0].Start);
            Assert.AreEqual(end, line.Segments[0].End);
        }

        [Test]
        public void SomeNotCrossingLines()
        {
            // Given
            var lines = new Tuple<Point, Point>[]
            {
                new Tuple<Point, Point>(new Point(1, 1), new Point(10, 0)),
                new Tuple<Point, Point>(new Point(6, 4), new Point(3, 2)),
                new Tuple<Point, Point>(new Point(4, 5), new Point(2, 2)),
                new Tuple<Point, Point>(new Point(5, 6), new Point(8, 8)),
                new Tuple<Point, Point>(new Point(6, 4), new Point(3, 2))
            };

            // When | Then
            for (byte i = 0; i < lines.Length; i++)
            {
                var line = _builder.Build(lines[i].Item1, lines[i].Item2);
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

            // When | Then
            var line1 = _builder.Build(a, b);
            Assert.AreEqual(1, line1.Segments.Length);
            Assert.AreEqual(0, line1.Id);
            Assert.AreEqual(a, line1.Segments[0].Start);
            Assert.AreEqual(b, line1.Segments[0].End);
            var line2 = _builder.Build(c, d);
            Assert.AreEqual(2, line2.Segments.Length);
            Assert.AreEqual(1, line2.Id);
            Assert.AreEqual(c, line2.Segments[0].Start);
            Assert.AreEqual(a, line2.Segments[0].End);
            Assert.AreEqual(a, line2.Segments[1].Start);
            Assert.AreEqual(d, line2.Segments[1].End);
        }

        [Test]
        public void SimpleCrossingLines1()
        {
            // Given
            Point a = new(2, 2);
            Point b = new(2, 8);
            Point c = new(2, 0);
            Point d = new(2, 2);

            // When | Then
            var line1 = _builder.Build(a, b);
            Assert.AreEqual(1, line1.Segments.Length);
            Assert.AreEqual(0, line1.Id);
            Assert.AreEqual(a, line1.Segments[0].Start);
            Assert.AreEqual(b, line1.Segments[0].End);
            var line2 = _builder.Build(c, d);
            Assert.AreEqual(2, line2.Segments.Length);
            Assert.AreEqual(1, line2.Id);
            Assert.AreEqual(c, line2.Segments[0].Start);
            Assert.AreEqual(a, line2.Segments[0].End);
            Assert.AreEqual(a, line2.Segments[1].Start);
            Assert.AreEqual(d, line2.Segments[1].End);
        }
    }
}
