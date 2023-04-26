using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MSW_Library;
using MSW_Library.MinesweeperGames;

namespace MSW_WPFApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public int m = 0;
        public int n = 0;

        public MinesweeperGame myGame = new BeginnerGame();

        Button[] buttons;

        private HashSet<Key> pressed = new HashSet<Key>();

        public MainWindow()
        {
            Title = "Minesweeper";
            InitializeComponent();
            KeyDown += new KeyEventHandler(KeyDownListener);
            KeyUp += new KeyEventHandler(KeyUpListener);
            StartNewGame();
        }

        void StartNewGame()
        {
            myGame = new BeginnerGame();
            m = myGame.m;
            n = myGame.n;
            buttons = new Button[m * n];
            BuildContainer();
            InsertButtons();

            UpdateGrid();
        }

        void BuildContainer()
        {
            Container.Children.Clear();
            Container.RowDefinitions.Clear();
            Container.ColumnDefinitions.Clear();

            Container.Height = 50 * m;
            Container.Width = 50 * n;

            for(int i=0; i<m; i++)
            {
                Container.RowDefinitions.Add(new RowDefinition()); ;
            }

            for(int j=0; j<n; j++)
            {
                Container.ColumnDefinitions.Add(new ColumnDefinition());
            }
        }

        void InsertButtons()
        {
            int p = 0;
            for(int i=0; i<m; i++)
            {
                for(int j=0; j<n; j++)
                {
                    Button button = new Button()
                    {
                        Height = 42,
                        Width = 42,
                        Content = "",
                        Background = Brushes.SteelBlue,
                        FontSize = 28,
                    };

                    Container.Children.Add(button);
                    Grid.SetRow(button, i);
                    Grid.SetColumn(button, j);

                    button.Click += Button_Click;
                    button.PreviewMouseRightButtonDown += Button_RightClick;
                    buttons[p++] = button;
                }
            }
        }

        void UpdateGrid()
        {
            for(int i=0; i<m; i++)
            {
                for(int j=0; j<n; j++)
                {
                    UpdateButton(i, j);
                }
            }
        }

        void UpdateButton(int i, int j)
        {
            int p = (i * n) + j;
            Button button = buttons[p];
            button.Content = ReadGameBoard(i, j);
            button.Background = Brushes.LightGray;

            SolidColorBrush[] minecountColours = new SolidColorBrush[]{
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

            switch (button.Content)
            {
                case " ":
                    button.Background = Brushes.SteelBlue;
                    return;
                case "F":
                    button.Foreground = Brushes.White;
                    button.Background = Brushes.Orange;
                    return;
                case "M":
                    button.Foreground = Brushes.Maroon;
                    button.Background = Brushes.Red;
                    return;
                default:
                    int k = int.Parse(button.Content.ToString());
                    button.Foreground = minecountColours[k];
                    return;
            }
        }

        string ReadGameBoard(int i, int j)
        {
            return myGame.BoardState[i, j];
        }

        private void ButtonClicked(int i, int j)
        {
            Coordinate move = new Coordinate()
            {
                i = i,
                j = j,
            };
            myGame.SubmitMove(move);
            UpdateGrid();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            int p = 0;
            for(int i=0; i<m; i++)
            {
                for(int j=0; j<n; j++)
                {
                    if(sender == buttons[p++])
                    {
                        ButtonClicked(i, j);
                    }
                }
            }
        }

        private void ToggleFlag(int i, int j)
        {
            Coordinate move = new Coordinate()
            {
                i = i,
                j = j,
            };

            myGame.FlagHere(move);
            UpdateButton(i, j);
        }

        private void Button_RightClick(object sender, RoutedEventArgs e)
        {
            int p = 0;
            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (sender == buttons[p++])
                    {
                        ToggleFlag(i, j);
                    }
                }
            }
        }
        private void KeyDownListener(object sender, KeyEventArgs e)
        {
            pressed.Add(e.Key);

            switch (e.Key)
            {
                case Key.N:
                    UserShortcutNewGame();
                    return;

                case Key.Escape:
                    UserShortcutQuitGame();
                    return;

                default:
                    return;
            }
        }

        private void KeyUpListener(object sender, KeyEventArgs e)
        {
            if (pressed.Contains(e.Key))
            {
                pressed.Remove(e.Key);
            }
        }

        void UserShortcutNewGame()
        {
            if ((pressed.Contains(Key.LeftCtrl) || pressed.Contains(Key.RightCtrl)) && pressed.Contains(Key.N))
            {
                StartNewGame();
            }
        }

        void UserShortcutQuitGame()
        {
            Application.Current.Shutdown();
        }
    }
}
