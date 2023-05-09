using Minesweeper.GameTiles;

namespace Minesweeper
{
    public abstract class Game
    {
        public GameBoard Board { get; private set; }

        private int _mineCount;

        public bool GameOver { get; private set; }
        public bool Victory { get; private set; }

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
                GameTile tile = Board.Tiles[row, col];
                if (!Board.HasMines)
                {
                    Board.SeedMines(tile, _mineCount);
                }
                tile.UserClick();
            }
            GameOver = CheckGameOver();

            return GetAffectedTiles();
        }

        private List<GameTile> GetAffectedTiles()
        {
            List<GameTile> affected = new List<GameTile>();
            while (Board.Affected.Count > 0)
            {
                GameTile tile = Board.Affected.Pop();
                affected.Add(tile);
            }

            return affected;
        }

        public List<GameTile> FlagTile(int row, int col)
        {
            if (!GameOver)
            {
                GameTile tile = Board.Tiles[row, col];
                tile.Flag();
            }

            return GetAffectedTiles();
        }

        private bool CheckGameOver()
        {
            foreach (MineTile mine in Board._mines)
            {
                if (mine.Clicked)
                {
                    LoseGame();
                    return true;
                }
            }

            if (Board.Cleared)
            {
                WinGame();
                return true;
            }

            return false;
        }

        private void WinGame()
        {
            Victory = true;
        }

        private void LoseGame()
        {
            foreach (MineTile tile in Board._mines)
            {
                tile.Click();
            }
        }
    }
}
