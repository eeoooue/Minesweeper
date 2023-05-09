using Minesweeper;
using Minesweeper.Games;
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
        public Game myGame = new BeginnerGame();

        private HashSet<Key> _pressed = new HashSet<Key>();

        public int DifficultySetting { get; private set; }

        public MainWindow()
        {
            DifficultySetting = 0;
            Title = "Minesweeper";
            InitializeComponent();
            KeyDown += new KeyEventHandler(KeyDownListener);
            KeyUp += new KeyEventHandler(KeyUpListener);
            StartNewGame();
        }

        private Game GetGameInstance()
        {
            switch (DifficultySetting)
            {
                case 0:
                    return new BeginnerGame();
                case 1:
                    return new IntermediateGame();
                default:
                    return new ExpertGame();
            }
        }

        void StartNewGame()
        {
            myGame = GetGameInstance();
            new ButtonBoard(myGame.Board, myGame, Container);
        }
        
        #region keyboard commands

        private void KeyDownListener(object sender, KeyEventArgs e)
        {
            _pressed.Add(e.Key);

            switch (e.Key)
            {
                case Key.D:
                    UserShortcutRotateDifficulty();
                    return;

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

        void UserShortcutRotateDifficulty()
        {
            if ((_pressed.Contains(Key.LeftCtrl) || _pressed.Contains(Key.RightCtrl)) && _pressed.Contains(Key.D))
            {
                DifficultySetting = (DifficultySetting + 1) % 3;
                StartNewGame();
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
