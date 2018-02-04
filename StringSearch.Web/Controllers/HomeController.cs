using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using StringSearch.Core;
using StringSearch.Math;
using StringSearch.Shared;

namespace StringSearch.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var gen = new StringSearchGenerator();
            var options = new StringSearchGeneratorOptions()
            {
                Width = 15,
                Height = 15,
                Directions = new List<CellDirection>() { CellDirection.Right, CellDirection.Down},
                Title = "Math Search",
                FillerCharacters = Defaults.Numbers,
                ItemsToPlace = RandomMathProblemsForPuzzle().Take(20).ToList()
            };

            var puzzle = gen.Generate(options);

            return View(puzzle);
        }

        private IEnumerable<IPuzzleEntry> RandomMathProblemsForPuzzle()
        {
            while (true)
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
                    yield return new MathProblemPuzzleEntry(prob);
                }
            }
        }

        public ActionResult New()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Words(string words, string title)
        {
            var wordList = words
                .Split(new[] {'\r', '\n'}, StringSplitOptions.RemoveEmptyEntries)
                .Select(l => l.Trim())
                .Where(l => !string.IsNullOrWhiteSpace(l))
                .ToList();

            var wordItems = wordList
                .Select(w => new CapitalWordEntry(w))
                .Cast<IPuzzleEntry>()
                .ToList();

            var gen = new StringSearchGenerator();
            var options = new StringSearchGeneratorOptions()
            {
                Width = 35,
                Height = 20,
                Directions = new List<CellDirection>() { CellDirection.Right, CellDirection.Down, CellDirection.DownRight },
                Title = title,
                FillerCharacters = Defaults.CapitalAlphabet,
                ItemsToPlace = wordItems
            };

            var puzzle = gen.GenerateGetMostEntries(options, 1000);

            return View("Index", puzzle);
        }

    }

    public class CapitalWordEntry : IPuzzleEntry
    {
        public CapitalWordEntry(string w)
        {
            DisplayValue = w.Trim().ToUpper();
            HiddenValue = RemoveNonAlpha(w).ToUpper();
        }

        private static string RemoveNonAlpha(string s)
        {
            Regex rgx = new Regex("[^a-zA-Z]");
            return rgx.Replace(s, "");
        }

        public string HiddenValue { get; }
        public string DisplayValue { get; }
    }
}