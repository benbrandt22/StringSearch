using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StringSearch.Core;
using StringSearch.Math;
using StringSearch.Shared;

namespace StringSearch.Sandbox
{
    class Program
    {
        static void Main(string[] args)
        {
            var gen = new StringSearchGenerator();
            var options = new StringSearchGeneratorOptions()
            {
                Width = 10,
                Height = 10,
                Directions = new List<CellDirection>(){CellDirection.Right, CellDirection.Down},
                Title = "Test Math Search",
                FillerCharacters = Defaults.Numbers,
            };

            for (int i = 0; i < 100; i++)
            {
                var probs = new List<IMathOperation>() {
                    MultiplicationOperation.NewRandom(25, 100),
                    AdditionOperation.NewRandom(100, 100000),
                    SubtractionOperation.NewRandomNonNegative(1, 100),
                    DivisionOperation.NewRandomIntegerResult(10, 100)
                };

                var prob = probs.GetRandom();

                if (prob.ValueDisplay.Length > 1)
                {
                    options.ItemsToPlace.Add(new MathProblemPuzzleEntry(prob));
                }

            }
            
            var puzzle = gen.Generate(options);

            Console.WriteLine(puzzle.Title);
            Console.WriteLine();
            Console.WriteLine(puzzle.Grid.ToString());
            Console.WriteLine();
            puzzle.Items.ForEach(i => Console.WriteLine(i.DisplayValue));
            Console.WriteLine();

            Console.ReadLine();

        }
    }

    internal class CapitalizedWord : IPuzzleEntry
    {
        public CapitalizedWord(string displayWord)
        {
            this.DisplayValue = displayWord;
        }

        public string HiddenValue => DisplayValue.ToUpper();
        public string DisplayValue { get; }
    }
}
