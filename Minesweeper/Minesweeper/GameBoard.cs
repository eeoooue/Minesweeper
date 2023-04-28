using Minesweeper.GameTiles;

namespace Minesweeper
{
    public class GameBoard
    {
        public int Rows { get; private set; }

        public int Columns { get; private set; }

        public GameTile[,] Tiles { get; private set; }

        public bool GameOver { get; private set; }

        public GameBoard(int rows, int columns)
        {
            Rows = rows;
            Columns = columns;
            Tiles = new GameTile[Rows, Columns];
            BuildTiles();
            GameOver = false;
        }

        public void EndGame()
        {
            GameOver = true;
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