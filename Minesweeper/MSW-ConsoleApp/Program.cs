
using MSW_Library;
using MSW_Library.MinesweeperGames;

namespace MSW_ConsoleApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            MinesweeperGame myGame = new BeginnerGame();

            while (!myGame.gameOver)
            {
                ShowGrid(myGame.GetGameboard());

                Coordinate move = GetPlayerMove();
                while (!Logic.ValidCoordinate(myGame.m, myGame.n, move))
                {
                    Console.WriteLine($"({move.i}, {move.j}) isn't a valid coordinate, please guess again using 'i j'");
                    move = GetPlayerMove();
                }

                myGame.SubmitMove(move);
                Console.Clear();
            }

            if (myGame.gameOver)
            {
                Console.WriteLine("GAME OVER");
            }
            ShowGrid(myGame.GetGameboard());
        }

        static void ShowGrid(string[,] board)
        {
            int m = board.GetLength(0);
            int n = board.GetLength(1);

            for (int i = 0; i < m; i++)
            {
                Console.Write("|");
                for (int j = 0; j < n; j++)
                {
                    Console.Write(board[i, j]);
                }
                Console.WriteLine("|");
            }
        }

        static Coordinate GetPlayerMove()
        {
            try
            {
                string[] arr = Console.ReadLine().Split(' ');
                Coordinate move = new Coordinate()
                {
                    i = int.Parse(arr[0]),
                    j = int.Parse(arr[1]),
                };
                return move;
            }
            catch
            {
                Console.WriteLine("Couldn't interpret your move, please enter in the form 'i j'");
                return GetPlayerMove();
            }
        }
    }
}