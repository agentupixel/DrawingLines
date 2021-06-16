using DrawingLines.Calculator;
using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace DrawingLines
{
    public partial class DrawingLinesForm : Form
    {
        private readonly LineBuilder _lineBuilder;
        private Point? _lineStart;
        private Color _color;

        public DrawingLinesForm()
        {
            InitializeComponent();
            _lineBuilder = new LineBuilder(new Point(Size.Width, Size.Height));
        }

        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            var graphics = CreateGraphics();
            if (!_lineStart.HasValue)
            {
                _lineStart = new Point(e.X, e.Y);
                Random random = new();
                _color = Color.FromArgb(random.Next(0, 255), random.Next(0, 255), random.Next(0, 255));
                graphics.FillEllipse(new SolidBrush(_color), e.X - 1, e.Y - 1, 3, 3);
                return;
            }
            if (_lineStart.Equals(new Point(e.X, e.Y)))
            {
                return;
            }
            Pen pen = new(new SolidBrush(_color));
            Point endPoint = new(e.X, e.Y);
            try
            {
                var points = _lineBuilder.BuildLine(_lineStart.Value, endPoint);
                graphics.DrawLines(pen, points.ToArray());
                graphics.FillEllipse(new SolidBrush(_color), e.X - 1, e.Y - 1, 3, 3);
                _lineStart = null;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        private void DrawingLinesForm_Load(object sender, EventArgs e)
        {
            AllocConsole();
        }

        private void DrawingLinesForm_Resize(object sender, EventArgs e)
        {
            _lineBuilder.SetSize(new Point(Size.Width, Size.Height));
        }

        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool AllocConsole();
    }
}
