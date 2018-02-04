using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using StringSearch.Core;
using Xunit;

namespace StringSearch.Tests
{
    public class StringSearchGridTests
    {
        [Theory]
        [InlineData(3, 3)]
        [InlineData(1, 5)]
        [InlineData(10, 5)]
        [InlineData(20, 25)]
        public void WidthHeightConstructor_RowColumnTest(int columns, int rows)
        {
            var grid = new StringSearchGrid(columns, rows);
            grid.Rows.Should().Be(rows);
            grid.Columns.Should().Be(columns);
        }

        [Theory]
        [InlineData("ABC\r\nDEF\r\nGHI", 3, 3)]
        [InlineData("AAAA\r\nBCDE", 4, 2)]
        public void Populate_RowColumnTest(string populateText, int expectedColumns, int expectedRows)
        {
            var grid = new StringSearchGrid(populateText);
            grid.Rows.Should().Be(expectedRows);
            grid.Columns.Should().Be(expectedColumns);
        }

        [Theory]
        [InlineData("ABC\r\nDEF\r\nGHI", 0, 0, 'A')]
        [InlineData("ABC\r\nDEF\r\nGHI", 1, 2, 'F')]
        [InlineData("ABC\r\nDEF\r\nGHI", 2, 2, 'I')]
        public void Populate_GetValueTest(string populateText, int getRow, int getCol, char expectedValue)
        {
            var grid = new StringSearchGrid(populateText);
            var value = grid.GetValue(getRow,getCol);
            value.Should().Be(expectedValue);
        }

        [Theory]
        [InlineData(1, 1)]
        [InlineData(10, 5)]
        public void SizeConstructor_AllValuesNull(int width, int height)
        {
            var grid = new StringSearchGrid(width, height);
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    grid.GetValue(y, x).Should().BeNull();
                }
            }
        }

        [Theory]
        [InlineData("ABC\r\nDEF\r\nGHI", 0, 0, 'A')]
        [InlineData("ABC\r\nDEF\r\nGHI", 1, 2, 'F')]
        [InlineData("ABC\r\nDEF\r\nGHI", 2, 2, 'I')]
        public void Populate_GetCellTest(string populateText, int getRow, int getCol, char expectedValue)
        {
            var grid = new StringSearchGrid(populateText);
            var cell = grid.GetCell(getRow, getCol);
            cell.Should().NotBeNull();
            cell.Value.Should().Be(expectedValue);
        }

        [Theory]
        [InlineData("ABC\r\nDEF\r\nGHI", -1, 0)]
        [InlineData("ABC\r\nDEF\r\nGHI", 0, -1)]
        [InlineData("ABC\r\nDEF\r\nGHI", 3, 1)]
        [InlineData("ABC\r\nDEF\r\nGHI", 1, 3)]
        public void Populate_GetCellForOutOfRangeReturnsNull(string populateText, int getRow, int getCol)
        {
            var grid = new StringSearchGrid(populateText);
            var cell = grid.GetCell(getRow, getCol);
            cell.Should().BeNull();
        }

        [Fact]
        public void GetRandomCellTest()
        {
            var grid = new StringSearchGrid("ABC\r\nDEF\r\nGHI");

            int testCount = 100;
            var resultCells = new List<GridCell>();
            for (int i = 0; i < testCount; i++)
            {
                resultCells.Add(grid.GetRandomCell());
            }

            resultCells.Count.Should().Be(testCount);

            resultCells.All(c => c != null).Should().Be(true, "it should always return a cell");

            resultCells.Select(c => c.Value)
                .Distinct().Count()
                .Should().BeGreaterThan(1, "it should return a variety of different cells");
        }

        [Theory]
        [InlineData("ABC\r\nDEF\r\nGHI")]
        [InlineData("AAAA\r\nBCDE")]
        [InlineData("111\r\n222\r\n333")]
        public void GridToStringTest(string populateText)
        {
            var grid = new StringSearchGrid(populateText);
            grid.ToString().Should().Be(populateText);
        }
    }
}
