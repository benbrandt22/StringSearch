using FluentAssertions;
using StringSearch.Core;
using Xunit;

namespace StringSearch.Tests
{
    public class GridCellApplyStringTests
    {

        [Theory]
        [InlineData("   \r\n   \r\n   ", "WOW", 1, 0, CellDirection.Right, true)]
        [InlineData("   \r\n   \r\n   ", "WOW", 1, 1, CellDirection.Right, false)]
        [InlineData("   \r\nDEF\r\n   ", "YES", 0, 1, CellDirection.Down, true)]
        [InlineData("   \r\nDEF\r\n   ", "NO", 0, 1, CellDirection.Down, false)]
        public void ApplyStringToGridTest(string populateText, string applyString, int startRow, int startColumn, CellDirection direction, bool expectedApplied)
        {
            var grid = new StringSearchGrid(populateText);
            var gridCell = grid.GetCell(startRow, startColumn);
            bool applyResult = gridCell.Apply(applyString, direction);

            applyResult.Should().Be(expectedApplied);
            grid.Contains(applyString).Should().Be(expectedApplied);
        }

    }
}
