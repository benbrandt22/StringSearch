using FluentAssertions;
using StringSearch.Core;
using Xunit;

namespace StringSearch.Tests
{
    public class GridAssignableTests
    {

        [Theory]
        [InlineData("1 3\r\n   \r\n3 3", 0, 0, "123", CellDirection.Right, true)]
        [InlineData("1 3\r\n   \r\n3 3", 0, 0, "123", CellDirection.DownRight, true)]
        [InlineData("1 3\r\n   \r\n3 3", 0, 0, "123", CellDirection.Down, true)]
        [InlineData("1 3\r\n   \r\n3 3", 0, 0, "123", CellDirection.Up, false)]
        [InlineData("1 3\r\n   \r\n3 3", 0, 0, "111", CellDirection.Right, false)]

        [InlineData("   \r\nAAA\r\n   ", 0, 1, "BAD", CellDirection.Down, true)]
        [InlineData("   \r\nAAA\r\n   ", 0, 2, "BED", CellDirection.Down, false)]

        public void CanStringBeAssignedToGridTest(string populateText, int startRow, int startColumn,
            string desiredString, CellDirection direction, bool expectedCanBeAssigned)
        {
            var grid = new StringSearchGrid(populateText);
            var startCell = grid.GetCell(startRow, startColumn);

            var canBeAssignedResult = startCell.CanBeAssigned(desiredString, direction);

            canBeAssignedResult.Should().Be(expectedCanBeAssigned);
        }

    }
}
