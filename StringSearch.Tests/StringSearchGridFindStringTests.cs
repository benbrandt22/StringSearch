using System.Linq;
using FluentAssertions;
using StringSearch.Core;
using Xunit;

namespace StringSearch.Tests
{
    public class StringSearchGridFindStringTests
    {
        private const string ThreeByThreeTestGrid = ("123\r\n456\r\n789");
        // shown below in multi-line comment, to help visualize...
        //  1 2 3
        //  4 5 6
        //  7 8 9

        [Theory]
        [InlineData(ThreeByThreeTestGrid, "123")]
        [InlineData(ThreeByThreeTestGrid, "147")]
        [InlineData(ThreeByThreeTestGrid, "987")]
        public void FindOneResult_CheckValueTest(string populateText, string searchFor)
        {
            var grid = new StringSearchGrid(populateText);
            var results = grid.Find(searchFor);
            
            results.First().Value.Should().Be(searchFor);
        }

        [Theory]
        [InlineData(ThreeByThreeTestGrid, "123", 0, 0)]
        [InlineData(ThreeByThreeTestGrid, "147", 0, 0)]
        [InlineData(ThreeByThreeTestGrid, "987", 2, 2)]
        [InlineData(ThreeByThreeTestGrid, "78", 2, 0)]
        [InlineData(ThreeByThreeTestGrid, "65", 1, 2)]
        public void FindOneResult_CheckStartCoordTest(string populateText, string searchFor, int expectedRow, int expectedColumn)
        {
            var grid = new StringSearchGrid(populateText);
            var results = grid.Find(searchFor);

            var coord = results.First().Start;
            coord.Row.Should().Be(expectedRow);
            coord.Column.Should().Be(expectedColumn);
        }
        
        [Theory]
        [InlineData(ThreeByThreeTestGrid, "123", CellDirection.Right)]
        [InlineData(ThreeByThreeTestGrid, "147", CellDirection.Down)]
        [InlineData(ThreeByThreeTestGrid, "987", CellDirection.Left)]
        [InlineData(ThreeByThreeTestGrid, "78", CellDirection.Right)]
        [InlineData(ThreeByThreeTestGrid, "65", CellDirection.Left)]
        [InlineData(ThreeByThreeTestGrid, "159", CellDirection.DownRight)]
        public void FindOneResult_CheckDirectionTest(string populateText, string searchFor, CellDirection expectedDirection)
        {
            var grid = new StringSearchGrid(populateText);
            var results = grid.Find(searchFor);

            results.First().Direction.Should().Be(expectedDirection);
        }

        [Theory]
        [InlineData(ThreeByThreeTestGrid, "123", true)]
        [InlineData(ThreeByThreeTestGrid, "147", true)]
        [InlineData(ThreeByThreeTestGrid, "987", true)]
        [InlineData(ThreeByThreeTestGrid, "78", true)]
        [InlineData(ThreeByThreeTestGrid, "65", true)]
        [InlineData(ThreeByThreeTestGrid, "159", true)]
        [InlineData(ThreeByThreeTestGrid, "00", false)]
        [InlineData(ThreeByThreeTestGrid, "222", false)]
        [InlineData(ThreeByThreeTestGrid, "abc", false)]
        [InlineData(ThreeByThreeTestGrid, "999", false)]
        public void GridContainsTest(string populateText, string searchFor, bool expectedContains)
        {
            var grid = new StringSearchGrid(populateText);
            bool result = grid.Contains(searchFor);

            result.Should().Be(expectedContains);
        }

    }
}
