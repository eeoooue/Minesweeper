using Minesweeper.GameTiles;

namespace Minesweeper
{
    public class GameBoard
    {
        public GameTile[,] Tiles { get; private set; }
        public int Rows { get { return Tiles.GetLength(0); } }
        public int Columns { get { return Tiles.GetLength(1); } }
        public Stack<GameTile> Affected { get { return _affectedTiles; } }

        private Stack<GameTile> _affectedTiles = new Stack<GameTile>();

        public List<MineTile> _mines = new();

        private int _clearables;

        public GameBoard(int rows, int columns)
        {
            Tiles = new GameTile[rows, columns];
            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Columns; j++)
                {
                    Tiles[i, j] = new EmptyTile(this, i, j);
                }
            }

            _clearables = rows * columns;
        }

        public bool FullyCleared()
        {
            return _clearables == 0;
        }

        public bool ContainsMines()
        {
            return _mines.Count > 0;
        }

        public void ClearTile()
        {
            _clearables--;
        }

        public void SeedMines(GameTile origin, int mineCount)
        {
            Random randomizer = new Random();

            while (_mines.Count < mineCount)
            {
                GameTile tile = GetRandomTile(randomizer);

                if (tile is EmptyTile && tile != origin)
                {
                    MineTile mine = SeedMine(tile);
                    _mines.Add(mine);
                }
            }
        }

        private GameTile GetRandomTile(Random randomizer)
        {
            int row = randomizer.Next(0, Rows);
            int column = randomizer.Next(0, Columns);
            GameTile tile = Tiles[row, column];

            return tile;
        }

        private MineTile SeedMine(GameTile tile)
        {
            MineTile mine = new MineTile(this, tile.Row, tile.Column);
            Tiles[tile.Row, tile.Column] = mine;
            _clearables--;

            return mine;
        }
    }
}