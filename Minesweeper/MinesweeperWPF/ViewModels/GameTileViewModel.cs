using Minesweeper;
using Minesweeper.GameTiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinesweeperWPF.ViewModels
{
    class GameTileViewModel : BaseViewModel
    {
        private GameTile Tile;

        public string Letter { get { return DetermineLetter(); } }

        public bool IsClicked { get { return Tile.Clicked; } }
        public bool IsFlagged { get { return Tile.Flagged; } }

        public string BackgroundColour { get { return DetermineBackgroundColour(); } }

        public GameTileViewModel(GameTile tile)
        {
            Tile = tile;
        }

        public void Flag()
        {
            Tile.Flag();
        }

        public void Click()
        {
            Tile.Click();
            Console.WriteLine("Clicked");
        }

        public string DetermineLetter()
        {
            if (IsFlagged)
            {
                return "F";
            }

            if (IsClicked && Tile is MineTile mine)
            {
                return "M";
            }

            if (IsClicked && Tile is EmptyTile info)
            {
                return info.Counter.ToString();
            }

            return " ";
        }

        public string DetermineBackgroundColour()
        {
            if (IsFlagged)
            {
                return "Orange";
            }
            if (IsClicked)
            {
                return "LightGray";
            }
            return "SteelBlue";
        }
    }
}
