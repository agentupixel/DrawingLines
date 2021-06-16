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
            var points = _builder.BuildLine(start, end);

            // Then
            Assert.AreEqual(11, points.Count);
            Assert.AreEqual(new Point(1, 1), points[0]);
            Assert.AreEqual(new Point(2, 1), points[1]);
            Assert.AreEqual(new Point(3, 1), points[2]);
            Assert.AreEqual(new Point(4, 1), points[3]);
            Assert.AreEqual(new Point(5, 1), points[4]);
            Assert.AreEqual(new Point(6, 1), points[5]);
            Assert.AreEqual(new Point(7, 1), points[6]);
            Assert.AreEqual(new Point(7, 2), points[7]);
            Assert.AreEqual(new Point(7, 3), points[8]);
            Assert.AreEqual(new Point(7, 4), points[9]);
            Assert.AreEqual(new Point(7, 5), points[10]);
        }

        [Test]
        public void SimpleCrossingLines()
        {
            // Given
            
        }
    }
}
