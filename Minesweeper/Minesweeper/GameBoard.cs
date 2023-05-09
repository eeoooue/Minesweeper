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

        public List<GameTile> GetAllCells()
        {
            List<GameTile> list = new List<GameTile>();
            foreach(GameTile tile in Tiles)
            {
                list.Add(tile);
            }
            return list;
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

        public MineTile SeedMine(int row, int column)
        {
            MineTile mine = new MineTile(this, row, column);
            Tiles[row, column] = mine;
            Clearables--;

            return mine;
        }
    }
}