using Minesweeper;

namespace Minesweeper_Test
{
    [TestClass]
    public class GameBoardTests
    {
        [TestMethod]
        public void CanInstantiateGameBoard()
        {
            GameBoard board = new GameBoard(10, 8);
            Assert.IsNotNull(board);
        }
    }
}