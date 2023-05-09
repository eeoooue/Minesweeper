
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
            _board.ClearTile();

            if (Counter == 0)
            {
                foreach (GameTile tile in GetNeighbours())
                {
                    if (!tile.Clicked)
                    {
                        tile.Click();
                    }
                }
            }
        }

        public override void Click()
        {
            if (Flagged)
            {
                return;
            }

            if (Clicked)
            {
                Chord();
                return;
            }

            Clicked = true;
            Activate();

            _board.Affected.Push(this);
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
                        if (0 <= j && j < _board.Columns)
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

        private void Chord()
        {
            if (Counter == CountFlaggedNeighbours())
            {
                List<GameTile> neighbours = GetNeighbours();

                foreach (GameTile tile in neighbours)
                {
                    if (!tile.Clicked)
                    {
                        tile.Click();
                    }
                }
            }
        }

        private int CountFlaggedNeighbours()
        {
            int count = 0;
            foreach (GameTile tile in GetNeighbours())
            {
                if (tile.Flagged)
                {
                    count++;
                }
            }
            return count;
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
