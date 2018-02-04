namespace StringSearch.Core
{
    public class GridCoordinate
    {
        public GridCoordinate(int row, int column)
        {
            Row = row;
            Column = column;
        }

        public int Row { get; }
        public int Column { get; }
    }
}