namespace StringSearch.Core
{
    public class FindResult
    {
        public string Value { get; set; }
        public GridCoordinate Start { get; set; }
        public CellDirection Direction { get; set; }
    }
}