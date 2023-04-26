

namespace MSW_Library
{
    public class MineField : ICoordinateGrid
    {
        private int[,] _mines;
        private Random _randomizer;

        HashSet<string> _placed = new HashSet<string>();

        public int m { get; set; }
        public int n { get; set; }
        public int mineCount;

        public MineField(int rows, int columns, int c)
        {
            m = rows;
            n = columns;

            _mines = new int[m, n];
            _randomizer = new Random();
            mineCount = c;
        }

        public void SeedMines(Coordinate startPt)
        {
            _mines = new int[m, n];

            while (_placed.Count < mineCount)
            {
                Coordinate destination = GetRandomCoord();
                if (!Logic.CoordinatesMatch(startPt, destination))
                {
                    PlaceMine(destination);
                }
            }
        }

        private void PlaceMine(Coordinate destination)
        {
            _placed.Add($"{destination.i},{destination.j}");
            _mines[destination.i, destination.j] = 1;
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
            if (Logic.ValidCoordinate(this, i, j))
            {
                return _mines[i, j] == 1;
            }
            return false;
        }

        private Coordinate GetRandomCoord()
        {
            return new Coordinate()
            {
                i = _randomizer.Next(m),
                j = _randomizer.Next(n),
            };
        }
    }
}
