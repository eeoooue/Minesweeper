using Minesweeper;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Media;

namespace WPF_Minesweeper
{
    public class ButtonBoard
    {
        public int Rows { get; private set; }
        public int Columns { get; private set; }

        public TileButton[,] _tileButtons;

        private GameBoard _board;

        private Game _mygame;

        public ButtonBoard(GameBoard gameBoard, Game myGame, Grid container)
        {
            Rows = gameBoard.Rows;
            Columns = gameBoard.Columns;

            _mygame = myGame;
            _board = gameBoard;
            _tileButtons = new TileButton[Rows, Columns];

            BuildContainer(container);
            AppendButtons(container);
        }

        void AppendButtons(Grid container)
        {
            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Columns; j++)
                {
                    TileButton button = new TileButton(this, i, j);

                    container.Children.Add(button);
                    Grid.SetRow(button, i);
                    Grid.SetColumn(button, j);

                    _tileButtons[i, j] = button;
                }
            }
        }

        void BuildContainer(Grid container)
        {
            container.Children.Clear();
            container.RowDefinitions.Clear();
            container.ColumnDefinitions.Clear();

            container.Height = 50 * Rows;
            container.Width = 50 * Columns;

            for (int i = 0; i < Rows; i++)
            {
                container.RowDefinitions.Add(new RowDefinition()); ;
            }

            for (int j = 0; j < Columns; j++)
            {
                container.ColumnDefinitions.Add(new ColumnDefinition());
            }
        }

        public void ClickTile(int row, int column)
        {
            if (ValidCoord(row, column))
            {
                _mygame.ClickTile(row, column);
                UpdateAffectedTiles();
                CheckGameEnd();
            }
        }

        private void CheckGameEnd()
        {
            if (_mygame.Victory)
            {
                ShowVictory();
            }
            else if (_mygame.GameOver)
            {
                ShowLoss();
            }
        }

        private void ShowVictory()
        {
            foreach (GameTile tile in _board.Tiles)
            {
                TileButton button = _tileButtons[tile.Row, tile.Column];
                button.ShowVictoryState(tile);
            }
        }

        private void ShowLoss()
        {
            foreach (GameTile tile in _board.Tiles)
            {
                TileButton button = _tileButtons[tile.Row, tile.Column];
                button.ShowLossState(tile);
            }
        }

        private void UpdateAffectedTiles()
        {
            while (_board.Affected.Count > 0)
            {
                GameTile tile = _board.Affected.Pop();
                TileButton button = _tileButtons[tile.Row, tile.Column];
                button.Update(tile);
            }
        }

        public void FlagTile(int row, int column)
        {
            if (ValidCoord(row, column))
            {
                _mygame.FlagTile(row, column);
                UpdateAffectedTiles();
            }
        }

        private bool ValidCoord(int i, int j)
        {
            return (0 <= i && i < Rows) && (0 <= j && j < Columns);
        }
    }
}
