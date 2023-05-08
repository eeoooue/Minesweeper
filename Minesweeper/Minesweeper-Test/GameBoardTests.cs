using Minesweeper;
using Minesweeper.Games;
using Minesweeper.GameTiles;

namespace Minesweeper_Test
{
    [TestClass]
    public class GameBoardTests
    {
        [TestMethod]
        public void CanInstantiateGameBoard()
        {
            Game game = new BeginnerGame();
            Assert.IsNotNull(game.Board);
        }

        [TestMethod]
        public void BoardsAreFullOfTiles()
        {
            Game game = new BeginnerGame();

            int expectedNumberOfTiles = game.Board.Rows * game.Board.Columns;
            Assert.AreEqual(expectedNumberOfTiles, game.Board.Tiles.Length);
        }

        [TestMethod]
        public void InitialBoardsHaveEmptyTilesOnly()
        {
            Game game = new BeginnerGame();

            foreach(GameTile tile in game.Board.Tiles)
            {
                Assert.IsInstanceOfType(tile, typeof(EmptyTile));
            }
        }

        [TestMethod]
        public void CanSelectSpecificTiles()
        {
            Game game = new BeginnerGame();

            GameTile tile = game.Board.GetTile(0, 0);
            Assert.IsNotNull(tile);
        }
    }
}