using DrawingLines.Calculator;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace DrawingLines
{
    public partial class DrawingLinesForm : Form
    {
        private bool _isLineStart;
        private readonly List<LineInfo> _lines;
        private readonly LineBuilder _lineBuilder;

        public DrawingLinesForm()
        {
            _lineBuilder = new LineBuilder();
            _lines = new();
            _isLineStart = true;
            InitializeComponent();
        }

        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            var graphics = CreateGraphics();
            LineInfo line;
            Color color;
            if(_isLineStart)
            {
                Random random = new();
                color = Color.FromArgb(random.Next(0, 255), random.Next(0, 255), random.Next(0, 255));
                line = new LineInfo(color, new Point(e.X, e.Y), default); 
                _lines.Add(line);
            }
            else
            {
                color = _lines[^1].Color;
                Point endPoint = new(e.X, e.Y);
                var polyLine = _lineBuilder.Build(_lines[^1].X, endPoint);
                _lines[^1] = new LineInfo(_lines[^1].Color, _lines[^1].X, polyLine.Segments[0].End);
                graphics.DrawLine(new Pen(new SolidBrush(color)), _lines[^1].X, _lines[^1].Y);
                for (short i = 1; i < polyLine.Segments.Length; i++)
                {
                    _lines.Add(new LineInfo(color, polyLine.Segments[i].Start, polyLine.Segments[i].End));
                    graphics.DrawLine(new Pen(new SolidBrush(color)), _lines[^1].X, _lines[^1].Y);
                }
            }
            graphics.FillEllipse(new SolidBrush(color), e.X - 1, e.Y - 1, 3, 3);
            _isLineStart = !_isLineStart;
        }
    }
}
