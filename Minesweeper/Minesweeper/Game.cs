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
                GameTile tile = Board.GetTile(row, col);

                if (!Board.HasMines)
                {
                    Board.SeedMines(tile, _mineCount);
                }
                
                tile.UserClick();
            }

            CheckGameState();

            return GetAffected();
        }

        private List<GameTile> GetAffected()
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
                GameTile tile = Board.GetTile(row, col);
                tile.Flag();
            }

            return GetAffected();
        }

        private void CheckGameState()
        {
            foreach (MineTile mine in Board._mines)
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
            foreach (MineTile tile in Board._mines)
            {
                tile.Click();
            }
            GameOver = true;
        }
    }
}
