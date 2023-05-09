using Minesweeper.GameTiles;

namespace Minesweeper
{
    public class GameBoard
    {
        public GameTile[,] Tiles { get; private set; }
        public int Rows { get; private set; }
        public int Columns { get; private set; }
        public bool Cleared { get { return _clearables == 0; } }
        public bool HasMines { get { return _mines.Count > 0; } }

        public Stack<GameTile> Affected { get { return _affectedTiles; } }

        private Stack<GameTile> _affectedTiles = new Stack<GameTile>();

        private Random _randomizer = new Random();

        public List<MineTile> _mines = new();

        private int _clearables;

        public GameBoard(int rows, int columns)
        {
            Rows = rows;
            Columns = columns;
            Tiles = BuildTiles();
            _clearables = Rows * Columns;
        }

        public void ClearTile()
        {
            _clearables--;
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