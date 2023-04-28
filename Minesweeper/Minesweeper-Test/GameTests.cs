using Minesweeper;
using Minesweeper.Games;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minesweeper_Test
{
    [TestClass]
    public class GameTests
    {
   
        [TestMethod]
        public void CanMakeBeginnerGame()
        {
            Game game = new BeginnerGame();
            Assert.IsNotNull(game);
        }

        [TestMethod]
        public void CanMakeIntermediateGame()
        {
            Game game = new IntermediateGame();
            Assert.IsNotNull(game);
        }

        [TestMethod]
        public void CanMakeExpertGame()
        {
            Game game = new ExpertGame();
            Assert.IsNotNull(game);
        }

        [TestMethod]
        public void GameHasBoard()
        {
            Game game = new BeginnerGame();
            GameBoard board = game.Board;
            Assert.IsNotNull(board);
        }

        [TestMethod]
        public void CanClickTile()
        {
            Game game = new BeginnerGame();
            int row = 0;
            int column = 0;
            game.ClickTile(row, column);

            GameTile tile = game.Board.GetTile(row, column);
            Assert.IsTrue(tile.Clicked);
        }
    }
}
