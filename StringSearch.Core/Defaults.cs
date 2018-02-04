using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StringSearch.Core
{
    public static class Defaults
    {

        public static List<CellDirection> AllDirections = new List<CellDirection>()
        {
            CellDirection.Up,
            CellDirection.UpRight,
            CellDirection.Right,
            CellDirection.DownRight,
            CellDirection.Down,
            CellDirection.DownLeft,
            CellDirection.Left,
            CellDirection.UpLeft
        };

        public static string CapitalAlphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

        public static string Numbers = "0123456789";

    }
}
