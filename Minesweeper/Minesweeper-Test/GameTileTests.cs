using Minesweeper;
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
            GameBoard board = new GameBoard(1, 1);
            GameTile tile = new EmptyTile(board, 0, 0);
            Assert.IsNotNull(tile);
        }

        [TestMethod]
        public void ArentMinesByDefault()
        {
            GameBoard board = new GameBoard(1, 1);
            GameTile tile = new EmptyTile(board, 0, 0);
            Assert.IsNotInstanceOfType(tile, typeof(MineTile));
        }

        [TestMethod]
        public void CanMakeMine()
        {
            GameBoard board = new GameBoard(1, 1);
            GameTile tile = new MineTile(board, 0, 0);
            Assert.IsInstanceOfType(tile, typeof(MineTile));
        }

        [TestMethod]
        public void CanClickTile()
        {
            GameBoard board = new GameBoard(1, 1);
            GameTile tile = new EmptyTile(board, 0, 0);
            Assert.IsTrue(tile.Click());
        }

        [TestMethod]
        public void CantClickTwice()
        {
            GameBoard board = new GameBoard(1, 1);
            GameTile tile = new EmptyTile(board, 0, 0);
            Assert.IsTrue(tile.Click());
            Assert.IsFalse(tile.Click());
        }

        [TestMethod]
        public void ClickingTileUpdatesText()
        {
            GameBoard board = new GameBoard(1, 1);
            GameTile tile = new EmptyTile(board, 0, 0);

            string before = tile.Text;
            tile.Click();
            string after = tile.Text;
            Assert.AreNotEqual(before, after);
        }

        [TestMethod]
        public void TileCountsAdjacentMinesA()
        {
            GameBoard board = new GameBoard(4, 5);

            board.Tiles[0, 1] = new MineTile(board, 0, 1);
            board.Tiles[1, 0] = new MineTile(board, 1, 0);
            board.Tiles[1, 1] = new MineTile(board, 1, 1);

            GameTile tile = board.Tiles[0, 0];
            tile.Click();

            string expected = "3";
            Assert.AreEqual(expected, tile.Text);
        }

        [TestMethod]
        public void TileCountsAdjacentMinesB()
        {
            GameBoard board = new GameBoard(4, 5);

            board.Tiles[0, 1] = new MineTile(board, 0, 1);

            GameTile tile = board.Tiles[0, 0];
            tile.Click();

            string expected = "1";
            Assert.AreEqual(expected, tile.Text);
        }

        [TestMethod]
        public void ClickingAMineEndsGame()
        {
            GameBoard board = new GameBoard(1, 1);
            board.Tiles[0, 0] = new MineTile(board, 0, 0);

            GameTile tile = board.Tiles[0, 0];
            tile.Click();

            Assert.IsTrue(board.GameOver);
        }

        [TestMethod]
        public void CanFlagTile()
        {
            GameBoard board = new GameBoard(1, 1);
            GameTile tile = new EmptyTile(board, 0, 0);
            tile.Flag();
        }

        [TestMethod]
        public void CantClickFlaggedTile()
        {
            GameBoard board = new GameBoard(1, 1);
            GameTile tile = new EmptyTile(board, 0, 0);
            tile.Flag();
            Assert.IsFalse(tile.Click());

        }

        [TestMethod]
        public void CantFlagClickedTile()
        {
            GameBoard board = new GameBoard(1, 1);
            GameTile tile = new EmptyTile(board, 0, 0);
            tile.Click();
            tile.Flag();
            Assert.IsFalse(tile.Flagged);
        }


    }
}
