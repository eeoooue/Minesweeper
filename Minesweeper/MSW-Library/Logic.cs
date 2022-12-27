using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSW_Library
{

    public struct Coordinate
    {
        public int i;
        public int j;
    }

    public class Logic
    {

        public static bool ValidCoordinate(int m, int n, int i, int j)
        {
            return (0 <= i && i < m) && (0 <= j && j < n);
        }

        public static bool ValidCoordinate(int m, int n, Coordinate point)
        {
            return ValidCoordinate(m, n, point.i, point.j);
        }

        public static bool ValidCoordinate(ICoordinateGrid grid, int i, int j)
        {
            return ValidCoordinate(grid.m, grid.n, i, j);
        }
       
        public static Coordinate[] CropArray(Coordinate[] original, int size)
        {
            Coordinate[] cropped = new Coordinate[size];

            for(int i=0; i<size; i++)
            {
                cropped[i] = original[i];
            }
            return cropped;
        }
        public static Coordinate[] GetNeighbours(int m, int n, int a, int b)
        {
            Coordinate[] coords = new Coordinate[9];

            int p = 0;
            for(int i = a-1; i<=a+1; i++)
            {
                for(int j = b-1; j <= b+1; j++)
                {
                    if (ValidCoordinate(m, n, i, j))
                    {
                        coords[p++] = new Coordinate()
                        {
                            i = i,
                            j = j,
                        };
                    }
                }
            }
            return CropArray(coords, p);
        }

        public static Coordinate[] GetNeighbours(ICoordinateGrid grid, Coordinate point)
        {
            return GetNeighbours(grid.m, grid.n, point.i, point.j);
        }

        public static bool CoordinatesMatch(Coordinate a, Coordinate b)
        {
            return (a.i == b.i) && (a.j == b.j);
        }

        public static Coordinate[] GetAllCoordinates(ICoordinateGrid grid)
        {
            Coordinate[] coords = new Coordinate[grid.m * grid.n];

            int p = 0;
            for(int i=0; i<grid.m; i++)
            {
                for(int j=0; j<grid.n; j++)
                {
                    coords[p++] = new Coordinate()
                    {
                        i = i,
                        j = j,
                    };
                }
            }

            return coords;
        }
    }
}
