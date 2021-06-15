using DrawingLines.Calculator;
using DrawingLines.Calculator.Structures;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace DrawingLines
{
    public partial class DrawingLinesForm : Form
    {
        private bool _isLineStart;
        private readonly List<LineInfo> _lines;
        private readonly LineBuilder _lineBuilder;
        private readonly LineBuilder2 _lineBuilder2;

        public DrawingLinesForm()
        {
            _lineBuilder = new LineBuilder();
            _lineBuilder2 = new LineBuilder2();
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
                Pen pen = new(new SolidBrush(color));
                Point endPoint = new(e.X, e.Y);
                var points = _lineBuilder2.BuildLine(_lines[^1].Start, endPoint);
                graphics.DrawLines(pen, points.ToArray());
                //color = _lines[^1].Color;
                //Point endPoint = new(e.X, e.Y);
                //Pen pen = new(new SolidBrush(color));
                //var polyLine = _lineBuilder.Build(_lines[^1].Start, endPoint);
                //_lines[^1] = new LineInfo(_lines[^1].Color, _lines[^1].Start, polyLine.Segments[0].End);
                //graphics.DrawLine(pen, _lines[^1].Start, _lines[^1].End);
                //for (short i = 1; i < polyLine.Segments.Length; i++)
                //{
                //    var lineInfo = GetDrawableLineInfo(new LineInfo(color, polyLine.Segments[i].Start, polyLine.Segments[i].End));
                //    _lines.Add(lineInfo);
                //    graphics.DrawLine(pen, lineInfo.Start, lineInfo.End);
                //}
            }
            graphics.FillEllipse(new SolidBrush(color), e.X - 1, e.Y - 1, 3, 3);
            _isLineStart = !_isLineStart;
        }

        private LineInfo GetDrawableLineInfo(LineInfo lineInfo)
        {
            if (_lines.Any(l => l.Matches(lineInfo)))
            {
                return GetDrawableLineInfo(
                    new LineInfo(
                        lineInfo.Color,
                        new Point(lineInfo.Start.X - 1, lineInfo.Start.Y - 1),
                        new Point(lineInfo.End.X - 1, lineInfo.End.Y - 1)));
            }

            return lineInfo;
        }

        private void DrawingLinesForm_Load(object sender, EventArgs e)
        {
            AllocConsole();
        }

        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool AllocConsole();
    }
}
