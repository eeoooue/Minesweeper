namespace Minesweeper
{
    public class GameBoard
    {
        public int Rows { get; private set; }
        public int Columns { get; private set; }

        public GameBoard(int rows, int columns)
        {
            Rows = rows;
            Columns = columns;
            BuildTiles();
        }

        

        private void BuildTiles()
        {



        }


    }
}