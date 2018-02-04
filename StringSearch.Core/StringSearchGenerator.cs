using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;
using StringSearch.Shared;

namespace StringSearch.Core
{
    public class StringSearchGenerator
    {
        public StringSearchPuzzle Generate(StringSearchGeneratorOptions options)
        {
            Validate(options);

            var puzzle = new StringSearchPuzzle()
            {
                Title = options.Title,
                Grid = new StringSearchGrid(options.Width, options.Height)
            };

            // place words
            int placementTriesPerWord = 100;
            
            foreach (var item in options.ItemsToPlace.OrderBy(a => Guid.NewGuid()).ToList())
            {
                for (int i = 0; i < placementTriesPerWord; i++)
                {
                    bool applySuccessful = puzzle.Grid.GetRandomCell().Apply(item.HiddenValue, options.Directions.GetRandom());
                    if (applySuccessful)
                    {
                        puzzle.Items.Add(item);
                        break;
                    }
                }
            }

            // fill in the empty cells
            for (int r = 0; r < puzzle.Grid.Rows; r++)
            {
                for (int c = 0; c < puzzle.Grid.Columns; c++)
                {
                    var cell = puzzle.Grid.GetCell(r, c);
                    if (cell.IsEmpty)
                    {
                        cell.Value = options.FillerCharacters.GetRandom();
                    }
                }
            }

            return puzzle;
        }

        public StringSearchPuzzle GenerateGetMostEntries(StringSearchGeneratorOptions options, int tries)
        {
            var puzzles = new List<StringSearchPuzzle>();
            for (int i = 0; i < tries; i++)
            {
                puzzles.Add(Generate(options));
            }

            var mostEntriesPuzzle = puzzles.OrderByDescending(p => p.Items.Count).First();

            return mostEntriesPuzzle;
        }

        private void Validate(StringSearchGeneratorOptions options)
        {
            if (options.Width <= 0) { throw new ArgumentException("Width must be at least 1", nameof(options.Width)); }
            if (options.Height <= 0) { throw new ArgumentException("Height must be at least 1", nameof(options.Height)); }
        }
    }
}
