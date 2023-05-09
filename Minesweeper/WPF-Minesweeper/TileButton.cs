using Minesweeper;
using Minesweeper.GameTiles;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace WPF_Minesweeper
{
    public class TileButton : Button
    {
        public int Row { get; private set; }
        public int Column { get; private set; }

        private ButtonBoard _board;

        private readonly SolidColorBrush[] minecountColours = new SolidColorBrush[]
        {
            Brushes.LightGray,
            Brushes.Blue,
            Brushes.SeaGreen,
            Brushes.OrangeRed,
            Brushes.Navy,
            Brushes.Maroon,
            Brushes.CadetBlue,
            Brushes.Black,
            Brushes.SlateGray,
        };

        public TileButton(ButtonBoard board, int i, int j)
        {
            Height = 42;
            Width = 42;
            Content = "";
            Background = Brushes.SteelBlue;
            FontSize = 28;

            _board = board;
            Row = i;
            Column = j;

            ShowCovered();
        }

        protected override void OnClick()
        {
            _board.ClickTile(Row, Column);
        }

        protected override void OnPreviewMouseRightButtonDown(MouseButtonEventArgs e)
        {
            _board.FlagTile(Row, Column);
        }

        public void Update(GameTile tile)
        {
            if (tile.Flagged)
            {
                ShowFlag();
            }
            else if (!tile.Clicked)
            {
                ShowCovered();
            }
            else if (tile is MineTile)
            {
                ShowMine();
            }
            else if (tile is EmptyTile emptyTile)
            {
                int count = emptyTile.Counter;
                ShowCounter(count);
            }
        }

        private void ShowCovered()
        {
            Foreground = Brushes.White;
            Background = Brushes.SteelBlue;
            Content = " ";
        }

        private void ShowFlag()
        {
            Foreground = Brushes.White;
            Background = Brushes.Orange;
            Content = "F";
        }

        private void ShowMine()
        {
            Foreground = Brushes.Maroon;
            Background = Brushes.Red;
            Content = "M";
        }

        private void ShowCounter(int counter)
        {
            Background = Brushes.LightGray;
            Foreground = minecountColours[counter];
            Content = counter.ToString();
        }

        public void ShowVictoryState(GameTile tile)
        {
            if (tile is EmptyTile emptyTile)
            {
                int count = emptyTile.Counter;
                ShowCounter(count);
                Foreground = Brushes.DarkGray;
            }
            else
            {
                ShowMine();
                Foreground = Brushes.White;
                Background = Brushes.DarkGreen;
            }
        }

        public void ShowLossState(GameTile tile)
        {
            if (tile is MineTile)
            {
                ShowMine();
            }
            else if (tile.Clicked)
            {
                Foreground = Brushes.DarkGray;
            }
            else if (tile.Flagged)
            {
                Background = Brushes.Black;
            }
        }
    }
}
