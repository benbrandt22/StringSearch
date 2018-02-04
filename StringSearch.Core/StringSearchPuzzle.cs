using System.Collections.Generic;

namespace StringSearch.Core
{
    public class StringSearchPuzzle
    {
        public StringSearchPuzzle()
        {
            Items = new List<IPuzzleEntry>();
        }

        public string Title { get; set; }
        public StringSearchGrid Grid { get; set; }
        public List<IPuzzleEntry> Items { get; set; }
    }
}