using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace StringSearch.Core
{
    public class StringSearchGrid
    {
        private GridCell[,] grid;
        private readonly Random random = new Random();

        public StringSearchGrid(int width, int height)
        {
            InitializeGrid(width, height);
        }
        
        public StringSearchGrid(string populateText)
        {
            string[] lines = populateText.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
            var maxLineWidth = lines.Max(l => l.Length);

            InitializeGrid(maxLineWidth, lines.Length);

            int y = 0;
            foreach (var line in lines)
            {
                for (int x = 0; x < maxLineWidth; x++)
                {
                    grid[y, x].Value = line[x];
                }
                y++;
            }
        }

        public int Rows => (grid.GetUpperBound(0) + 1);
        public int Columns => (grid.GetUpperBound(1) + 1);

        private void InitializeGrid(int width, int height)
        {
            this.grid = new GridCell[height, width];
            // populate the grid with new cells
            for (int r = 0; r < height; r++)
            {
                for (int c = 0; c < width; c++)
                {
                    grid[r,c] = new GridCell();
                }
            }
            // Now link up adjoining cells appropriately
            for (int r = 0; r < height; r++)
            {
                for (int c = 0; c < width; c++)
                {
                    var thisCell = grid[r, c];
                    thisCell.SetNextCell(CellDirection.Up, GetCell(r - 1, c));
                    thisCell.SetNextCell(CellDirection.UpRight, GetCell(r - 1, c + 1));
                    thisCell.SetNextCell(CellDirection.Right, GetCell(r, c + 1));
                    thisCell.SetNextCell(CellDirection.DownRight, GetCell(r + 1, c + 1));
                    thisCell.SetNextCell(CellDirection.Down, GetCell(r + 1, c));
                    thisCell.SetNextCell(CellDirection.DownLeft, GetCell(r + 1, c - 1));
                    thisCell.SetNextCell(CellDirection.Left, GetCell(r, c - 1));
                    thisCell.SetNextCell(CellDirection.UpLeft, GetCell(r - 1, c - 1));
                }
            }
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            int row = 0;
            foreach (var t in AllCells())
            {
                if (t.Item1.Row != row) {
                    // new row, add line break
                    sb.Append("\r\n");
                }
                sb.Append(t.Item2.Value ?? ' ');
                row = t.Item1.Row;
            }
            return sb.ToString();
        }

        public char? GetValue(int row, int col)
        {
            return GetCell(row,col).Value;
        }

        public GridCell GetCell(int row, int col)
        {
            if (row < 0 || row >= Rows || col < 0 || col >= Columns)
            {
                return null;
            }
            return grid[row, col];
        }

        public GridCell GetRandomCell()
        {
            int row = random.Next(0, Rows);
            int col = random.Next(0, Columns);
            return GetCell(row, col);
        }

        private IEnumerable<Tuple<GridCoordinate, GridCell>> AllCells()
        {
            for (int r = 0; r < Rows; r++)
            {
                for (int c = 0; c < Columns; c++)
                {
                    yield return Tuple.Create(new GridCoordinate(r,c), grid[r, c]);
                }
            }
        }

        public IEnumerable<FindResult> Find(string searchFor)
        {
            var directions = new[]
            {
                CellDirection.Up,
                CellDirection.UpRight,
                CellDirection.Right,
                CellDirection.DownRight,
                CellDirection.Down,
                CellDirection.DownLeft,
                CellDirection.Left,
                CellDirection.UpLeft
            };

            foreach (var t in AllCells())
            {
                var cell = t.Item2;
                var coord = t.Item1;
                foreach (var d in directions)
                {
                    var seqString = string.Join("", cell.GetSequence(d, searchFor.Length).Select(sc => sc.Value ?? ' '));
                    if (searchFor == seqString)
                    {
                        yield return new FindResult() {
                            Value = seqString,
                            Start = coord,
                            Direction = d
                        };
                    }
                }
            }
        }

        public bool Contains(string searchFor)
        {
            return this.Find(searchFor).Any();
        }
        
    }
}
