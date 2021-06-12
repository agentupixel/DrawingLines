namespace DrawingLines.Calculator.Structures
{
    public struct Polyline
    {
        public Polyline(int id, params Line[] segments)
        {
            Id = id;
            Segments = segments;
        }

        public int Id { get; }
        public Line[] Segments { get; }
    }
}
