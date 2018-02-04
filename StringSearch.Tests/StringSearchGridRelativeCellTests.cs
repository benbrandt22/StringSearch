using FluentAssertions;
using StringSearch.Core;
using Xunit;

namespace StringSearch.Tests
{
    public class StringSearchGridRelativeCellTests
    {
        private const string ThreeByThreeTestGrid = ("123\r\n456\r\n789");
        // shown below in multi-line comment, to help visualize...
        //  1 2 3
        //  4 5 6
        //  7 8 9

        [Theory]
        [InlineData(ThreeByThreeTestGrid, 0, 0, CellDirection.Up, null)]
        [InlineData(ThreeByThreeTestGrid, 1, 1, CellDirection.Up, '2')]
        [InlineData(ThreeByThreeTestGrid, 2, 2, CellDirection.Up, '6')]

        [InlineData(ThreeByThreeTestGrid, 0, 0, CellDirection.UpRight, null)]
        [InlineData(ThreeByThreeTestGrid, 1, 1, CellDirection.UpRight, '3')]
        [InlineData(ThreeByThreeTestGrid, 2, 2, CellDirection.UpRight, null)]

        [InlineData(ThreeByThreeTestGrid, 0, 0, CellDirection.Right, '2')]
        [InlineData(ThreeByThreeTestGrid, 1, 1, CellDirection.Right, '6')]
        [InlineData(ThreeByThreeTestGrid, 2, 2, CellDirection.Right, null)]

        [InlineData(ThreeByThreeTestGrid, 0, 0, CellDirection.DownRight, '5')]
        [InlineData(ThreeByThreeTestGrid, 1, 1, CellDirection.DownRight, '9')]
        [InlineData(ThreeByThreeTestGrid, 2, 2, CellDirection.DownRight, null)]

        [InlineData(ThreeByThreeTestGrid, 0, 0, CellDirection.Down, '4')]
        [InlineData(ThreeByThreeTestGrid, 1, 1, CellDirection.Down, '8')]
        [InlineData(ThreeByThreeTestGrid, 2, 2, CellDirection.Down, null)]

        [InlineData(ThreeByThreeTestGrid, 0, 0, CellDirection.DownLeft, null)]
        [InlineData(ThreeByThreeTestGrid, 1, 1, CellDirection.DownLeft, '7')]
        [InlineData(ThreeByThreeTestGrid, 2, 2, CellDirection.DownLeft, null)]

        [InlineData(ThreeByThreeTestGrid, 0, 0, CellDirection.Left, null)]
        [InlineData(ThreeByThreeTestGrid, 1, 1, CellDirection.Left, '4')]
        [InlineData(ThreeByThreeTestGrid, 2, 2, CellDirection.Left, '8')]

        [InlineData(ThreeByThreeTestGrid, 0, 0, CellDirection.UpLeft, null)]
        [InlineData(ThreeByThreeTestGrid, 1, 1, CellDirection.UpLeft, '1')]
        [InlineData(ThreeByThreeTestGrid, 2, 2, CellDirection.UpLeft, '5')]

        public void DirectionTest(string populateText, int getRow, int getCol, CellDirection direction, char? expectedValue)
        {
            var grid = new StringSearchGrid(populateText);
            var cell = grid.GetCell(getRow, getCol);
            var resultCell = cell.GetNextCell(direction);

            if (expectedValue == null)
            {
                resultCell.Should().BeNull();
            }
            else
            {
                resultCell.Value.Should().Be(expectedValue);
            }
        }

    }
}
