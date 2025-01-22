using Minesweeper;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinesweeperWPF.ViewModels
{
    class GameBoardViewModel : BaseViewModel
    {
        public ObservableCollection<GameTileViewModel> Tiles { get; set; }

        public int Rows { get { return GameBoard.Rows;  } }
        public int Columns { get { return GameBoard.Columns;  } }

        private GameBoard GameBoard;

        public GameBoardViewModel(GameBoard gameboard)
        {
            GameBoard = gameboard;
            Tiles = new ObservableCollection<GameTileViewModel>();

            foreach(GameTile tile in gameboard.Tiles)
            {
                GameTileViewModel model = new GameTileViewModel(tile);
                Tiles.Add(model);
            }
        }
    }
}
