using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using StringSearch.Core;
using Xunit;

namespace StringSearch.Tests
{
    public class StringSearchGeneratorTests
    {

        [Theory]
        [InlineData(2, 2)]
        [InlineData(6, 4)]
        [InlineData(1, 8)]
        [InlineData(20, 25)]
        public void GeneratorOutputsRequestedSizeGrid(int width, int height)
        {
            var gen = new StringSearchGenerator();
            var options = new StringSearchGeneratorOptions()
            {
                Width = width,
                Height = height
            };
            var result = gen.Generate(options);
            result.Grid.Columns.Should().Be(width);
            result.Grid.Rows.Should().Be(height);
        }

        [Theory]
        [InlineData(0, 5)]
        [InlineData(10, 0)]
        [InlineData(-1, 7)]
        [InlineData(8, -2)]
        public void GeneratorThrowsExceptionForInvalidSize(int width, int height)
        {
            var gen = new StringSearchGenerator();
            var options = new StringSearchGeneratorOptions()
            {
                Width = width,
                Height = height
            };
            Action test = () =>
            {
                var result = gen.Generate(options);
            };
            test.ShouldThrow<ArgumentException>();
        }

        [Theory]
        [InlineData("title")]
        [InlineData("word search")]
        [InlineData("")]
        public void GeneratorOutputsTitle(string title)
        {
            var gen = new StringSearchGenerator();
            var options = new StringSearchGeneratorOptions()
            {
                Width = 5,
                Height = 5,
                Title = title
            };
            var result = gen.Generate(options);
            result.Title.Should().Be(title);
        }

        [Theory]
        [InlineData("WORD", "SEARCH")]
        [InlineData("HUNT", "FIND")]
        public void GeneratorPlacesWordsInGridAndItems(string word1, string word2)
        {
            var gen = new StringSearchGenerator();
            int size = (new[] {word1, word2}.Max(w => w.Length))*2;
            var options = new StringSearchGeneratorOptions()
            {
                Width = size,
                Height = size,
                Directions = new List<CellDirection>() { CellDirection.Right, CellDirection.Down }
            };
            options.ItemsToPlace.Add(new TestPuzzleEntry(word1.ToLower(), word1.ToUpper()));
            options.ItemsToPlace.Add(new TestPuzzleEntry(word2.ToLower(), word2.ToUpper()));

            var result = gen.Generate(options);

            result.Grid.Contains(word1.ToUpper()).Should().BeTrue();
            result.Grid.Contains(word2.ToUpper()).Should().BeTrue();

            result.Items.Count(e => e.DisplayValue == word1.ToLower()).Should().Be(1);
            result.Items.Count(e => e.DisplayValue == word2.ToLower()).Should().Be(1);
        }

        [Theory]
        [InlineData("TESTINGUP", CellDirection.Up)]
        [InlineData("TESTINGUPRIGHT", CellDirection.UpRight)]
        [InlineData("TESTINGRIGHT", CellDirection.Right)]
        [InlineData("TESTINGDOWNRIGHT", CellDirection.DownRight)]
        [InlineData("TESTINGDOWN", CellDirection.Down)]
        [InlineData("TESTINGDOWNLEFT", CellDirection.DownLeft)]
        [InlineData("TESTINGLEFT", CellDirection.Left)]
        [InlineData("TESTINGUPLEFT", CellDirection.UpLeft)]
        public void GeneratorPlacesWordsUsingDesiredDirections(string word, CellDirection direction)
        {
            var gen = new StringSearchGenerator();
            int size = (word.Length * 2);
            var options = new StringSearchGeneratorOptions()
            {
                Width = size,
                Height = size,
                Directions = new List<CellDirection>() { direction }
            };
            options.ItemsToPlace.Add(new TestPuzzleEntry(word, word));

            var puzzle = gen.Generate(options);

            var findResult = puzzle.Grid.Find(word).First();

            findResult.Direction.Should().Be(direction);
        }

        [Theory]
        [InlineData("TEST")]
        [InlineData("WORD")]
        public void GeneratorPopulatesAllCells(string word)
        {
            var gen = new StringSearchGenerator();
            int size = (word.Length * 2);
            var options = new StringSearchGeneratorOptions()
            {
                Width = size,
                Height = size,
                Directions = new List<CellDirection>() { CellDirection.Right }
            };
            options.ItemsToPlace.Add(new TestPuzzleEntry(word, word));

            var puzzle = gen.Generate(options);

            for (int r = 0; r < puzzle.Grid.Rows; r++)
            {
                for (int c = 0; c < puzzle.Grid.Columns; c++)
                {
                    var cell = puzzle.Grid.GetCell(r, c);
                    cell.IsEmpty.Should().BeFalse();
                }
            }
        }

    }

    public class TestPuzzleEntry : IPuzzleEntry
    {
        public TestPuzzleEntry(string displayValue, string hiddenValue)
        {
            DisplayValue = displayValue;
            HiddenValue = hiddenValue;
        }

        public string HiddenValue { get; }
        public string DisplayValue { get; }
    }
}
