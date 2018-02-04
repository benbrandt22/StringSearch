using System.Linq;
using FluentAssertions;
using StringSearch.Core;
using Xunit;

namespace StringSearch.Tests
{
    public class GridCellTests
    {

        [Theory]
        [InlineData(CellDirection.Up)]
        [InlineData(CellDirection.UpRight)]
        [InlineData(CellDirection.Right)]
        [InlineData(CellDirection.DownRight)]
        [InlineData(CellDirection.Down)]
        [InlineData(CellDirection.DownLeft)]
        [InlineData(CellDirection.Left)]
        [InlineData(CellDirection.UpLeft)]
        public void SetGetNextCellTest(CellDirection direction)
        {
            var baseCell = new GridCell('A');
            var nextCell = new GridCell('B');
            baseCell.SetNextCell(direction, nextCell);

            var result = baseCell.GetNextCell(direction);
            result.Should().Be(nextCell);
            result.Value.Should().Be('B');
        }

        [Theory]
        [InlineData(CellDirection.Up)]
        [InlineData(CellDirection.UpRight)]
        [InlineData(CellDirection.Right)]
        [InlineData(CellDirection.DownRight)]
        [InlineData(CellDirection.Down)]
        [InlineData(CellDirection.DownLeft)]
        [InlineData(CellDirection.Left)]
        [InlineData(CellDirection.UpLeft)]
        public void NotSetNextCellReturnsNullTest(CellDirection direction)
        {
            var baseCell = new GridCell('A');
            // no next cells set

            var result = baseCell.GetNextCell(direction);
            result.Should().BeNull();
        }

        [Theory]
        [InlineData(CellDirection.Right, "abcd", 3, "abc")]
        [InlineData(CellDirection.Right, "abcd", 5, "abcd")]
        [InlineData(CellDirection.Right, "abcd", 1, "a")]

        [InlineData(CellDirection.UpRight, "abc", 3, "abc")]
        [InlineData(CellDirection.Down, "abc", 3, "abc")]
        [InlineData(CellDirection.Left, "abc", 3, "abc")]
        public void GetCellSequenceTest(CellDirection direction, string sourceCells, int requestedCells, string expectedCells)
        {
            // instantiate some grid cells
            var cells = sourceCells.Select(c => new GridCell(c)).ToList();
            for (int i = 1; i < sourceCells.Length; i++)
            {
                cells[i-1].SetNextCell(direction, cells[i]);
            }

            var sequence = cells[0].GetSequence(direction, requestedCells);

            var sequenceString = string.Join("", sequence.Select(c => c.Value));

            sequenceString.Should().Be(expectedCells);
        }

        [Theory]
        [InlineData(null, 'A', true)]
        [InlineData(' ', 'A', true)]
        [InlineData('B', 'B', true)]
        [InlineData('C', 'D', false)]
        [InlineData('E', ' ', false)]
        [InlineData('F', null, false)]
        public void CellCanBeAssignedTest(char? initialValue, char? desiredValue, bool expectedCanBeAssigned)
        {
            var cell = new GridCell(initialValue);
            bool result = cell.CanBeAssigned(desiredValue);

            result.Should().Be(expectedCanBeAssigned);
        }

        [Theory]
        [InlineData("test", CellDirection.Right, "test", true)]
        [InlineData("t s ", CellDirection.Right, "test", true)]
        [InlineData("123", CellDirection.Right, "1234", false)]
        [InlineData("   ", CellDirection.Right, "ABC", true)]
        [InlineData("1234", CellDirection.Right, "123", true)]
        public void CellSequenceCanBeAssignedTest(string sourceCells, CellDirection direction, string desiredWord, bool expectedCanBeAssigned)
        {
            // instantiate some grid cells
            var cells = sourceCells.Select(c => new GridCell(c)).ToList();
            for (int i = 1; i < sourceCells.Length; i++) { cells[i - 1].SetNextCell(direction, cells[i]); }

            var canWordBeAssigned = cells.First().CanBeAssigned(desiredWord, direction);

            canWordBeAssigned.Should().Be(expectedCanBeAssigned);
        }

        [Theory]
        [InlineData(null, true)]
        [InlineData(' ', true)]
        [InlineData('A', false)]
        [InlineData('1', false)]
        public void GridCellIsEmptyPropertyTest(char? constructorValue, bool expectedIsEmpty)
        {
            var cell = new GridCell(constructorValue);
            cell.IsEmpty.Should().Be(expectedIsEmpty);
        }

    }
}
