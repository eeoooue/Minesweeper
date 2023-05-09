
using Minesweeper.GameTiles;
using System.Collections.Generic;
using System.Data.Common;

namespace Minesweeper
{
    public abstract class Game
    {
        public GameBoard Board { get; private set; }

        private int _mineCount;

        public bool GameOver { get; private set; }
        public bool Victory { get; private set; }

        private bool HasMines {  get { return _mines.Count > 0; } }

        private List<GameTile> _affected = new();

        private List<MineTile> _mines = new();

        public Game(int rows, int columns, int mineCount)
        {
            Board = new GameBoard(rows, columns);
            GameOver = false;
            Victory = false;
            _mineCount = mineCount;
        }

        public List<GameTile> ClickTile(int row, int col)
        {
            if (!GameOver)
            {
                if (!HasMines)
                {
                    SeedMines(row, col);
                }

                GameTile tile = Board.GetTile(row, col);
                tile.UserClick();
            }

            CheckGameState();

            return Board.GetAllCells();
        }

        public List<GameTile> FlagTile(int row, int col)
        {
            _affected = new List<GameTile>();

            if (!GameOver)
            {
                GameTile tile = Board.GetTile(row, col);
                tile.Flag();
                _affected.Add(tile);
            }

            return _affected;
        }

        private void SeedMines(int i, int j)
        {
            GameTile origin = Board.GetTile(i, j);
            Random randomizer = new Random();

            while (_mines.Count < _mineCount)
            {
                int row = randomizer.Next(0, Board.Rows);
                int column = randomizer.Next(0, Board.Columns);
                GameTile tile = Board.GetTile(row, column);

                if (tile is EmptyTile && tile != origin)
                {
                    MineTile mine = Board.SeedMine(row, column);
                    _mines.Add(mine);
                }
            }
        }

        private void CheckGameState()
        {
            foreach (MineTile mine in _mines)
            {
                if (mine.Clicked)
                {
                    LoseGame();
                    return;
                }
            }

            if (Board.Cleared)
            {
                WinGame();
                return;
            }
        }

        private void WinGame()
        {
            Victory = true;
            GameOver = true;
        }

        private void LoseGame()
        {
            foreach (MineTile tile in _mines)
            {
                tile.Click();
            }

            GameOver = true;
        }
    }
}
