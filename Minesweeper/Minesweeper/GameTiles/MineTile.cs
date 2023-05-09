
namespace Minesweeper.GameTiles
{
    public class MineTile : GameTile
    {
        public MineTile(GameBoard parent, int i, int j) : base(parent, i, j) { }

        public override void Click()
        {
            if (Flagged || Clicked)
            {
                return;
            }

            Clicked = true;
            Activate();
            _board.Affected.Push(this);
        }

        protected override void Activate()
        {
            Text = "M";
        }
    }
}
