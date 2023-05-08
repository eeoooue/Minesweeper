
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
        private bool _seeded = false;

        private List<GameTile> _affected = new List<GameTile>();

        public Game(int rows, int columns, int mineCount)
        {
            Board = new GameBoard(this, rows, columns);
            GameOver = false;
            _mineCount = mineCount;
        }

        public List<GameTile> ClickTile(int row, int col)
        {
            _affected = new List<GameTile>();

            if (!GameOver)
            {
                Explore(row, col);
            }

            return _affected;
        }

        public void EndGame()
        {
            GameOver = true;

            foreach(GameTile tile in Board.Tiles)
            {
                if (tile is MineTile)
                {
                    Explore(tile.Row, tile.Column);
                }
            }
        }


        private void Explore(int row, int col)
        {
            if (ValidCoordinates(row, col))
            {
                GameTile tile = Board.GetTile(row, col);

                if (tile.Clicked || tile.Flagged)
                {
                    return;
                }

                if (!_seeded)
                {
                    SeedMines(row, col);
                }

                tile.Click();
                _affected.Add(tile);

                if (tile is EmptyTile emptyTile)
                {
                    if (emptyTile.Counter == 0)
                    {
                        for (int i = row-1; i <=row+1; i++)
                        {
                            for(int j = col-1; j <=col+1; j++)
                            {
                                Explore(i, j);
                            }
                        }
                    }
                }
            }
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

            int placed = 0;

            while (placed < _mineCount)
            {
                int row = randomizer.Next(0, Board.Rows);
                int column = randomizer.Next(0, Board.Columns);
                GameTile tile = Board.GetTile(row, column);

                if (tile is EmptyTile && tile != origin)
                {
                    Board.SeedMine(row, column);
                    placed++;
                }
            }

            _seeded = true;
        }

        public bool ValidCoordinates(int i, int j)
        {
            if (0 <= i && i < Board.Rows)
            {
                if (0 <= j && j < Board.Columns)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
