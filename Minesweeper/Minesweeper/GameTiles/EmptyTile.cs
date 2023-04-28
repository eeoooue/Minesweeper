
namespace Minesweeper.GameTiles
{
    public class EmptyTile : GameTile
    {
        public int Counter { get; private set; }

        public EmptyTile(GameBoard parent, int i, int j) : base(parent, i, j) { }

        protected override void Activate()
        {
            Counter = CountNeighbourMines();
            Text = Counter.ToString();
        }

        private List<GameTile> GetNeighbours()
        {
            List<GameTile> neighbours = new List<GameTile>();

            for (int i = Row - 1; i <= Row + 1; i++)
            {
                for (int j = Column - 1; j <= Column + 1; j++)
                {
                    if (0 <= i && i < _board.Rows)
                    {
                        if(0 <= j && j < _board.Columns)
                        {
                            GameTile neighbour = _board.GetTile(i, j);
                            if (neighbour != this)
                            {
                                neighbours.Add(neighbour);
                            }
                        }
                    }
                }
            }

            return neighbours;
        }

        private int CountNeighbourMines()
        {
            int count = 0;
            foreach (GameTile neighbour in GetNeighbours())
            {
                if (neighbour is MineTile)
                {
                    count++;
                }
            }

            return count;
        }
    }
}
