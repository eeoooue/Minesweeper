
namespace Minesweeper.GameTiles
{
    public class MineTile : GameTile
    {
        public MineTile(GameBoard parent, int i, int j) : base(parent, i, j) { }

        protected override void Activate()
        {
            Text = "M";

            if (!_board.GameOver)
            {
                _board.LoseGame();
            }
        }
    }
}
