using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSW_Library
{

    public class GameBoard : ICoordinateGrid
    {
        public string[,] board;
        public int m { get; set; }
        public int n { get; set; }


        public GameBoard(int rows, int columns)
        {
            m = rows;
            n = columns;

            board = new string[m, n];
            ClearGrid();
        }
        private void ClearGrid()
        {
            for(int i=0; i<m; i++)
            {
                for(int j=0; j<n; j++)
                {
                    board[i, j] = " ";
                }
            }
        }

        public bool IsFlag(Coordinate point)
        {
            return IsFlag(point.i, point.j);
        }

        private bool IsFlag(int i, int j)
        {
            return Logic.ValidCoordinate(this, i, j) && board[i, j] == "F";
        }

        public void UpdateCell(int i, int j, string value)
        {
            board[i, j] = value;
        }

        public void UpdateCell(int i, int j, int value)
        {
            UpdateCell(i, j, value.ToString());
        }
    }
}
