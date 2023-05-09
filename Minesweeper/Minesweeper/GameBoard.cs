using Minesweeper.GameTiles;

namespace Minesweeper
{
    public class GameBoard
    {
        public int Rows { get; private set; }

        public int Columns { get; private set; }

        public GameTile[,] Tiles { get; private set; }

        public bool Cleared { get { return Clearables == 0; } }

        protected int Clearables { get; private set; }

        private Stack<GameTile> _affectedTiles = new Stack<GameTile>();

        private Random _randomizer = new Random();

        public bool HasMines { get { return _mines.Count > 0; } }

        public List<MineTile> _mines = new();

        public Stack<GameTile> Affected { get { return _affectedTiles; } }

        public GameBoard(int rows, int columns)
        {
            Rows = rows;
            Columns = columns;
            Tiles = BuildTiles();
            Clearables = Rows * Columns;
        }

        public void ClearTile()
        {
            Clearables--;
        }

        public GameTile GetTile(int i, int j)
        {
            return Tiles[i, j];
        }

        private GameTile[,] BuildTiles()
        {
            GameTile[,] tiles = new GameTile[Rows, Columns];
            for (int i = 0; i < Rows; i++)
            {
                for(int j = 0; j < Columns; j++)
                {
                    tiles[i, j] = new EmptyTile(this, i, j);
                }
            }

            return tiles;
        }


        public void SeedMines(GameTile origin, int mineCount)
        {
            while (_mines.Count < mineCount)
            {
                GameTile tile = GetRandomTile();

                if (tile is EmptyTile && tile != origin)
                {
                    MineTile mine = SeedMine(tile);
                    _mines.Add(mine);
                }
            }
        }

        private GameTile GetRandomTile()
        {
            int row = _randomizer.Next(0, Rows);
            int column = _randomizer.Next(0, Columns);
            GameTile tile = GetTile(row, column);

            return tile;
        }

        private MineTile SeedMine(GameTile tile)
        {
            MineTile mine = new MineTile(this, tile.Row, tile.Column);
            Tiles[tile.Row, tile.Column] = mine;
            Clearables--;

            return mine;
        }
    }
}