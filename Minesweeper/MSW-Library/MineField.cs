using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSW_Library
{
    public class MineField : ICoordinateGrid
    {
        private int[,] mines;
        private Random randomizer;

        public int m { get; set; }
        public int n { get; set; }
        public int mineCount;

        public MineField(int rows, int columns, int c)
        {
            m = rows;
            n = columns;

            mines = new int[m, n];
            randomizer = new Random();
            mineCount = c;
        }

        public void SeedMines(Coordinate startPt)
        {
            HashSet<string> placed = new HashSet<string>();
            mines = new int[m, n];

            while (placed.Count < mineCount)
            {
                Coordinate mine = GetRandomCoord();
                if (!Logic.CoordinatesMatch(startPt, mine))
                {
                    placed.Add($"{mine.i},{mine.j}");
                    mines[mine.i, mine.j] = 1;
                }
            }
        }

        public Coordinate[] GetAllMines()
        {
            Coordinate[] allMines = new Coordinate[mineCount];

            int p = 0;
            foreach(Coordinate point in Logic.GetAllCoordinates(this))
            {
                if(IsMine(point))
                {
                    allMines[p++] = point;
                }
            }

            return allMines;
        }

        public int MinesNearby(Coordinate start)
        {
            int count = 0;
            Coordinate[] neighbours = Logic.GetNeighbours(this, start);
            foreach(Coordinate point in neighbours)
            {
                if (IsMine(point))
                {
                    count++;
                }
            }
            return count;
        }

        public bool IsMine(Coordinate point)
        {
            return IsMine(point.i, point.j);
        }

        public bool IsMine(int i, int j)
        {
            return Logic.ValidCoordinate(this, i, j) && mines[i, j] == 1;
        }

        private Coordinate GetRandomCoord()
        {
            return new Coordinate()
            {
                i = randomizer.Next(m),
                j = randomizer.Next(n),
            };
        }
    }
}
