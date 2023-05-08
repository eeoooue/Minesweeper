using Minesweeper;
using Minesweeper.Games;
using Minesweeper.GameTiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minesweeper_Test
{
    [TestClass]
    public class GameTileTests
    {
        [TestMethod]
        public void CanInstantiateTile()
        {
            Game game = new BeginnerGame();
            GameTile tile = new EmptyTile(game.Board, 0, 0);
            Assert.IsNotNull(tile);
        }

        [TestMethod]
        public void ArentMinesByDefault()
        {
            Game game = new BeginnerGame();
            GameTile tile = new EmptyTile(game.Board, 0, 0);
            Assert.IsNotInstanceOfType(tile, typeof(MineTile));
        }

        [TestMethod]
        public void CanMakeMine()
        {
            Game game = new BeginnerGame();
            GameTile tile = new MineTile(game.Board, 0, 0);
            Assert.IsInstanceOfType(tile, typeof(MineTile));
        }

        [TestMethod]
        public void CanClickTile()
        {
            Game game = new BeginnerGame();
            GameTile tile = new EmptyTile(game.Board, 0, 0);
            Assert.IsTrue(tile.Click());
        }

        [TestMethod]
        public void CantClickTwice()
        {
            Game game = new BeginnerGame();
            GameTile tile = new EmptyTile(game.Board, 0, 0);
            Assert.IsTrue(tile.Click());
            Assert.IsFalse(tile.Click());
        }

        [TestMethod]
        public void ClickingTileUpdatesText()
        {
            Game game = new BeginnerGame();
            GameTile tile = new EmptyTile(game.Board, 0, 0);

            string before = tile.Text;
            tile.Click();
            string after = tile.Text;
            Assert.AreNotEqual(before, after);
        }

        [TestMethod]
        public void TileCountsAdjacentMinesA()
        {
            throw new NotImplementedException();
        }

        [TestMethod]
        public void TileCountsAdjacentMinesB()
        {
            throw new NotImplementedException();
        }

        [TestMethod]
        public void ClickingAMineEndsGame()
        {
            Game game = new BeginnerGame();
            game.Board.Tiles[0, 0] = new MineTile(game.Board, 0, 0);

            game.ClickTile(0, 0);
            Assert.IsTrue(game.GameOver);
        }

        [TestMethod]
        public void CanFlagTile()
        {
            Game game = new BeginnerGame();
            game.FlagTile(0, 0);
        }

        [TestMethod]
        public void CantClickFlaggedTile()
        {
            throw new NotImplementedException();
        }

        [TestMethod]
        public void CantFlagClickedTile()
        {
            throw new NotImplementedException();
        }


    }
}
