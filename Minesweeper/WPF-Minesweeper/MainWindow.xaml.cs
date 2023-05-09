using Minesweeper;
using Minesweeper.Games;
using Minesweeper.GameTiles;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace WPF_Minesweeper
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public int m = 0;
        public int n = 0;

        public Game myGame = new BeginnerGame();

        private HashSet<Key> _pressed = new HashSet<Key>();

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
            m = myGame.Board.Rows;
            n = myGame.Board.Columns;
            ButtonBoard board = new ButtonBoard(myGame.Board, myGame, Container);
        }
        
        #region keyboard commands

        private void KeyDownListener(object sender, KeyEventArgs e)
        {
            _pressed.Add(e.Key);

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
            if (_pressed.Contains(e.Key))
            {
                _pressed.Remove(e.Key);
            }
        }

        void UserShortcutNewGame()
        {
            if ((_pressed.Contains(Key.LeftCtrl) || _pressed.Contains(Key.RightCtrl)) && _pressed.Contains(Key.N))
            {
                StartNewGame();
            }
        }

        void UserShortcutQuitGame()
        {
            Application.Current.Shutdown();
        }

        #endregion
    }
}
