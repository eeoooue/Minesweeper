using Minesweeper;
using Minesweeper.GameTiles;

namespace Minesweeper_Test
{
    [TestClass]
    public class GameBoardTests
    {
        [TestMethod]
        public void CanInstantiateGameBoard()
        {
            GameBoard board = new GameBoard(1, 1);
            Assert.IsNotNull(board);
        }

        [TestMethod]
        public void BoardsAreFullOfTiles()
        {
            GameBoard board = new GameBoard(4, 5);

            int expectedNumberOfTiles = board.Rows * board.Columns;
            Assert.AreEqual(expectedNumberOfTiles, board.Tiles.Length);
        }

        [TestMethod]
        public void InitialBoardsHaveEmptyTilesOnly()
        {
            GameBoard board = new GameBoard(4, 4);

            foreach(GameTile tile in board.Tiles)
            {
                Assert.IsInstanceOfType(tile, typeof(EmptyTile));
            }
        }

        [TestMethod]
        public void CanSelectSpecificTiles()
        {
            GameBoard board = new GameBoard(4, 5);
            GameTile tile = board.GetTile(1, 1);
            Assert.IsNotNull(tile);
        }
    }
}