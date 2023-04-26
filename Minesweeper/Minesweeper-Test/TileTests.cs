using Minesweeper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minesweeper_Test
{
    [TestClass]
    public class TileTests
    {
        [TestMethod]
        public void CanInstantiateTile()
        {
            GameTile tile = new GameTile();
            Assert.IsNotNull(tile);
        }

        [TestMethod]
        public void ArentMinesByDefault()
        {
            GameTile tile = new GameTile();

            Assert.IsFalse(tile.IsMine);
        }

        [TestMethod]
        public void CanMakeMine()
        {
            GameTile tile = new GameTile();
            tile.IsMine = true;

            Assert.IsTrue(tile.IsMine);
        }
    }
}
