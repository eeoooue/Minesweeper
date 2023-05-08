
namespace Minesweeper
{
    public abstract class GameTile
    {
        public string Text { get; protected set; }
        public bool Clicked { get; protected set; }
        public bool Flagged { get; protected set; }

        protected readonly GameBoard _board;

        public int Row { get; private set; }
        public int Column { get; private set; }
        
        public GameTile(GameBoard parent, int i, int j)
        {
            _board = parent;
            Text = "";
            Row = i;
            Column = j;
        }

        protected abstract void Activate();


        public void UserClick()
        {

            if (Flagged)
            {
                return;
            }

            if (!Clicked)
            {
                Click();
            }
            else
            {
                Chord();
            }
        }




        public void Click()
        {
            if (Flagged)
            {
                return;
            }

            if (!Clicked)
            {
                Clicked = true;
                Activate();

                if (Text == "0")
                {
                    foreach(GameTile tile in GetNeighbours())
                    {
                        tile.Click();
                    }
                }
            }
        }

        private void Chord()
        {
            int cellValue = int.Parse(Text);

            int flagCount = CountFlaggedNeighbours();

            if (cellValue == flagCount)
            {
                List<GameTile> neighbours = GetNeighbours();

                foreach(GameTile tile in neighbours)
                {
                    tile.Click();
                }
            }
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

        private int CountFlaggedNeighbours()
        {
            int count = 0;
            foreach(GameTile tile in GetNeighbours())
            {
                if (tile.Flagged)
                {
                    count++;
                }
            }
            return count;
        }

        public bool Flag()
        {
            if (!Clicked && !Flagged)
            {
                Flagged = true;
            }
            else
            {
                Flagged = false;
            }

            return Flagged;
        }
    }
}
