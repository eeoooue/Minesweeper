
namespace Minesweeper
{
    public abstract class GameTile
    {
        public bool Clicked { get; protected set; }
        public bool Flagged { get; protected set; }
        public int Row { get; private set; }
        public int Column { get; private set; }

        protected readonly GameBoard _board;

        public GameTile(GameBoard parent, int i, int j)
        {
            _board = parent;
            Row = i;
            Column = j;
        }

        public abstract void Click();

        protected void NotifyParent()
        {
            _board.Affected.Push(this);
        }

        public void UserClick()
        {
            if (!Flagged)
            {
                Click();
            }
        }

        public bool Flag()
        {
            if (!Clicked)
            {
                Flagged = !Flagged;
                NotifyParent();
            }
            return Flagged;
        }
    }
}
