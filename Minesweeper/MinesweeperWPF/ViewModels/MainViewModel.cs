using Minesweeper;
using Minesweeper.Games;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinesweeperWPF.ViewModels
{
    class MainViewModel
    {
        public GameBoardViewModel GameBoard { get; set; }

        private Game _minesweeperGame;

        public MainViewModel()
        {
            _minesweeperGame = new BeginnerGame();
            GameBoard = new GameBoardViewModel(_minesweeperGame.Board);
        }

        public void NewGame()
        {
            if (_minesweeperGame is BeginnerGame)
            {
                _minesweeperGame = new BeginnerGame();
            }
            else if (_minesweeperGame is IntermediateGame)
            {
                _minesweeperGame = new IntermediateGame();
            }
            else
            {
                _minesweeperGame = new ExpertGame();
            }
        }

        public void RotateDifficulty()
        {
            if (_minesweeperGame is BeginnerGame)
            {
                _minesweeperGame = new IntermediateGame();
            }
            else if (_minesweeperGame is IntermediateGame)
            {
                _minesweeperGame = new ExpertGame();
            }
            else
            {
                _minesweeperGame = new BeginnerGame();
            }
            NewGame();
        }

    }
}
