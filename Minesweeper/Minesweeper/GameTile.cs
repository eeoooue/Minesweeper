
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

        public abstract void Click();


        public void UserClick()
        {
            if (Flagged)
            {
                return;
            }

            Click();
        }

        public bool Flag()
        {
            if (Clicked)
            {
                return false;
            }

            Flagged = !Flagged;
            _board.Affected.Push(this);

            return Flagged;
        }
    }
}
