using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSW_Library
{
    public class MinesweeperGame
    {
        private MineField minefield;
        public GameBoard gameboard;
        public int m;
        public int n;
        private int movesMade = 0;

        public bool gameOver = false;

        private HashSet<Coordinate> uncovered = new HashSet<Coordinate>();

        public MinesweeperGame(int rows, int columns, int mines)
        {
            m = rows;
            n = columns;

            minefield = new MineField(m, n, mines);
            gameboard = new GameBoard(m, n); 
        }

        public string[,] GetGameboard()
        {
            return gameboard.board;
        }

        public void FlagHere(Coordinate point)
        {
            if (!gameOver)
            {
                string action = gameboard.IsFlag(point) ? " " : "F";
                gameboard.UpdateCell(point.i, point.j, action);
            }
        }

        public void SubmitMove(Coordinate point)
        {
            if(gameOver || gameboard.IsFlag(point) || uncovered.Contains(point))
            {
                return;
            }
            uncovered.Add(point);
            StepHere(point);
        }

        private void StepHere(Coordinate point)
        {
            if (movesMade++ == 0)
            {
                minefield.SeedMines(point);
            }

            if (minefield.IsMine(point))
            {
                gameOver = true;
                MarkAllMines();
            }
            else
            {
                int c = minefield.MinesNearby(point);
                gameboard.UpdateCell(point.i, point.j, c);

                if(c == 0)
                {
                    foreach(Coordinate autoMove in Logic.GetNeighbours(gameboard, point))
                    {
                        SubmitMove(autoMove);
                    }
                }

            }
        }
        private void MarkAllMines()
        {
            foreach (Coordinate point in minefield.GetAllMines())
            {
                gameboard.UpdateCell(point.i, point.j, "M");
            }
        }
    }
}
