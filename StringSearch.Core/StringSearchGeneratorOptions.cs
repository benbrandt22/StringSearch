using System.Collections.Generic;

namespace StringSearch.Core
{
    public class StringSearchGeneratorOptions
    {
        public StringSearchGeneratorOptions()
        {
            ItemsToPlace = new List<IPuzzleEntry>();
            // defaults:
            FillerCharacters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        }

        public int Width { get; set; }
        public int Height { get; set; }
        public string Title { get; set; }
        public List<IPuzzleEntry> ItemsToPlace { get; set; }
        public ICollection<CellDirection> Directions { get; set; }
        public string FillerCharacters { get; set; }
    }
}