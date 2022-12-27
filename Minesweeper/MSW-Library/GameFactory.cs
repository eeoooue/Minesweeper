using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSW_Library
{

    public enum MinesweeperDifficulty
    {
        Beginner,
        Intermediate,
        Expert,
    }

    public class GameFactory
    {
        public static MinesweeperGame CreateGame(MinesweeperDifficulty difficulty)
        {
            switch (difficulty)
            {
                case MinesweeperDifficulty.Beginner:
                    return new MinesweeperGame(9, 9, 10);

                case MinesweeperDifficulty.Intermediate:
                    return new MinesweeperGame(16, 16, 40);

                case MinesweeperDifficulty.Expert:
                    return new MinesweeperGame(16, 30, 99);

                default:
                    return new MinesweeperGame(8, 8, 10);
            }
        }
    }
}
