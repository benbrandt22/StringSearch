using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StringSearch.Core
{
    public class GridCell
    {
        private readonly Dictionary<CellDirection, GridCell> nextCells;
        public GridCell() : this(null) {}

        public GridCell(char? value)
        {
            nextCells = new Dictionary<CellDirection, GridCell>();
            this.Value = value;
        }

        public char? Value { get; set; }
        public bool IsEmpty => (Value == null || Value == ' ');

        public void SetNextCell(CellDirection direction, GridCell cell)
        {
            if (this.nextCells.ContainsKey(direction)) {
                this.nextCells[direction] = cell;
            } else {
                this.nextCells.Add(direction,cell);
            }
        }
        
        public GridCell GetNextCell(CellDirection direction)
        {
            return (nextCells.ContainsKey(direction) ? nextCells[direction] : null);
        }

        public IEnumerable<GridCell> GetSequence(CellDirection direction, int totalCells)
        {
            if (totalCells == 0)
            {
                yield break;
            }

            yield return this;

            var nextCell = this.GetNextCell(direction);
            if (nextCell == null)
            {
                yield break;
            }
            else
            {
                foreach (var nc in nextCell.GetSequence(direction, totalCells-1))
                {
                    yield return nc;
                }
            }
        }

        /// <summary>
        /// indicates if this cell can be set to the desired value.
        /// </summary>
        public bool CanBeAssigned(char? desiredValue)
        {
            if (Value == null || Value == ' ') // empty cell
            {
                return true;
            }
            if (Value == desiredValue)
            {
                return true;
            }
            return false;
        }

        public bool CanBeAssigned(string desiredString, CellDirection direction)
        {
            var cellSequence = this.GetSequence(direction, desiredString.Length).ToList();
            if (cellSequence.Count < desiredString.Length)
            {
                return false;
            }

            bool assignable = desiredString.Select((c, i) => cellSequence[i].CanBeAssigned(c)).All(a => a);

            return assignable;
        }

        public bool Apply(string applyString, CellDirection direction)
        {
            bool canBeApplied = this.CanBeAssigned(applyString, direction);
            if (!canBeApplied) { return false; }

            var seq = this.GetSequence(direction, applyString.Length).ToList();

            for (int i = 0; i < applyString.Length; i++)
            {
                seq[i].Value = applyString[i];
            }

            return true;
        }
    }
}
