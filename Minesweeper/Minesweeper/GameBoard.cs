using Minesweeper.GameTiles;

namespace Minesweeper
{
    public class GameBoard
    {
        public int Rows { get; private set; }

        public int Columns { get; private set; }

        public GameTile[,] Tiles { get; private set; }

        public bool GameOver { get; private set; }

        private Game _game;

        public GameBoard(Game game, int rows, int columns)
        {
            _game = game;
            Rows = rows;
            Columns = columns;
            Tiles = new GameTile[Rows, Columns];
            BuildTiles();
            GameOver = false;
        }

        public List<GameTile> GetAllCells()
        {
            List<GameTile> list = new List<GameTile>();
            foreach(GameTile tile in Tiles)
            {
                list.Add(tile);
            }
            return list;
        }

        public void EndGame()
        {
            _game.EndGame();
        }

        public GameTile GetTile(int i, int j)
        {
            return Tiles[i, j];
        }

        private void BuildTiles()
        {
            for(int i = 0; i < Rows; i++)
            {
                for(int j = 0; j < Columns; j++)
                {
                    Tiles[i, j] = new EmptyTile(this, i, j);
                }
            }
        }

        public void SeedMine(int row, int column)
        {
            Tiles[row, column] = new MineTile(this, row, column);
        }
    }
}