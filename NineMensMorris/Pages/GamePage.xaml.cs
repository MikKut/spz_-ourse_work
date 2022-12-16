using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace NineMensMorris.Pages
{
    /// <summary>
    /// Логика взаимодействия для GamePage.xaml
    /// </summary>
    public partial class GamePage : Page
    {
        //TODO: increse letters when mouse(trigger, styles)
        private Windows.MainWindow _content;
        string warningText = "No warnings";
        private static string 
            _setChipsString = "Set your chips", _startHuntingString = "Hunting on oponent chips",
            _turnOfTheFirstPlayerString = "Player 1 is moving", _turnOfTheSecondPlayerString = "Player 2 is moving";

        private bool warningExists = false, chipIsTakenToSet = false, inProcessOfTakingChip = false;
        
        Models.ButtonPosition prevButton;
        public GamePage(Windows.MainWindow content, bool fromContinue)
        {
            InitializeComponent();
            _content = content;
            if (fromContinue)
            { 
                SetStartPosition();
            }
            Models.GameState.TurnIsChanged += ChangeTextTurn;
            Models.GameState.PeriodIsChanged += ChangePeriodOfTheGame;
            Models.GameState.TwoChipsIsLeftEvent += Win;
            GameLogic.MovingLogic.ResetWarningEvent += ResetWarnings;
        
        }
        private void Win(object sender, Models.GameStateEventArgs.TwoChipsIsLeftArgs args)
        {
            if(args.itsTurn == Models.Turn.Player1)
            {
                MessageBox.Show("Player 2 won!");
            }
            else
            {
                MessageBox.Show("Player 1 won!");
            }
            _content.mainFrame.GoBack();
        }
        private void ChangeTextTurn(object sender, EventArgs args)
        {
            TurnText.Text = $"{Models.GameState.TurnOfThePlayer.ToString()} is moving";
        }
        private void ChangePeriodOfTheGame(object sender, EventArgs args)
        {
            StageOfTheGameText.Text = $"{Models.GameState.GamePeriod}!";
        }
        private bool ResetWarnings(bool warningExists)
        {
            if (warningExists)
            {
                WarningText.Text = "No warnings";
                return false;
            }
            return true;
        }
        private void SetStartPosition()
        {
            #region SetBelongingOfTheChips
            SetBelongingOfTheChip(Models.GameState.ButtonStates.GetValueOrDefault(Models.ButtonPosition.a1), Button_a1);
            SetBelongingOfTheChip(Models.GameState.ButtonStates.GetValueOrDefault(Models.ButtonPosition.a4), Button_a4);
            SetBelongingOfTheChip(Models.GameState.ButtonStates.GetValueOrDefault(Models.ButtonPosition.a7), Button_a7);

            SetBelongingOfTheChip(Models.GameState.ButtonStates.GetValueOrDefault(Models.ButtonPosition.b2), Button_b2);
            SetBelongingOfTheChip(Models.GameState.ButtonStates.GetValueOrDefault(Models.ButtonPosition.b4), Button_b4);
            SetBelongingOfTheChip(Models.GameState.ButtonStates.GetValueOrDefault(Models.ButtonPosition.b6), Button_b6);

            SetBelongingOfTheChip(Models.GameState.ButtonStates.GetValueOrDefault(Models.ButtonPosition.c3), Button_c3);
            SetBelongingOfTheChip(Models.GameState.ButtonStates.GetValueOrDefault(Models.ButtonPosition.c4), Button_c4);
            SetBelongingOfTheChip(Models.GameState.ButtonStates.GetValueOrDefault(Models.ButtonPosition.c5), Button_c5);

            SetBelongingOfTheChip(Models.GameState.ButtonStates.GetValueOrDefault(Models.ButtonPosition.d1), Button_d1);
            SetBelongingOfTheChip(Models.GameState.ButtonStates.GetValueOrDefault(Models.ButtonPosition.d2), Button_d2);
            SetBelongingOfTheChip(Models.GameState.ButtonStates.GetValueOrDefault(Models.ButtonPosition.d3), Button_d3);
            SetBelongingOfTheChip(Models.GameState.ButtonStates.GetValueOrDefault(Models.ButtonPosition.d5), Button_d5); 
            SetBelongingOfTheChip(Models.GameState.ButtonStates.GetValueOrDefault(Models.ButtonPosition.d6), Button_d6);
            SetBelongingOfTheChip(Models.GameState.ButtonStates.GetValueOrDefault(Models.ButtonPosition.d7), Button_d7);

            SetBelongingOfTheChip(Models.GameState.ButtonStates.GetValueOrDefault(Models.ButtonPosition.e3), Button_e3);
            SetBelongingOfTheChip(Models.GameState.ButtonStates.GetValueOrDefault(Models.ButtonPosition.e4), Button_e4);
            SetBelongingOfTheChip(Models.GameState.ButtonStates.GetValueOrDefault(Models.ButtonPosition.e5), Button_e5);

            SetBelongingOfTheChip(Models.GameState.ButtonStates.GetValueOrDefault(Models.ButtonPosition.f2), Button_f2);
            SetBelongingOfTheChip(Models.GameState.ButtonStates.GetValueOrDefault(Models.ButtonPosition.f4), Button_f4);
            SetBelongingOfTheChip(Models.GameState.ButtonStates.GetValueOrDefault(Models.ButtonPosition.f6), Button_f6);

            SetBelongingOfTheChip(Models.GameState.ButtonStates.GetValueOrDefault(Models.ButtonPosition.g1), Button_g1);
            SetBelongingOfTheChip(Models.GameState.ButtonStates.GetValueOrDefault(Models.ButtonPosition.g4), Button_g4);
            SetBelongingOfTheChip(Models.GameState.ButtonStates.GetValueOrDefault(Models.ButtonPosition.g7), Button_g7);
            #endregion
            SetGameState(Models.GameState.GamePeriod);
            SetPlayerTurn(Models.GameState.TurnOfThePlayer);
        }

        private void SetGameState(Models.PeriodOfTheGame period)
        {
            switch (period)
            {
                case Models.PeriodOfTheGame.HuntingTime:
                    StageOfTheGameText.Text = _startHuntingString;
                    break;
                case Models.PeriodOfTheGame.SetUp:
                    StageOfTheGameText.Text = _setChipsString;
                    break;
                default:
                    throw new NotImplementedException($"There is no other periods of the game. The state \"{period}\" is not declared");

            }
        }
        private void SetPlayerTurn(Models.Turn turn )
        {
            switch (turn)
            {
                case Models.Turn.Player1:
                    TurnText.Text = _turnOfTheFirstPlayerString;
                    break;
                case Models.Turn.Player2:
                    TurnText.Text = _turnOfTheSecondPlayerString;
                    break;
                default:
                    throw new NotImplementedException($"There is no other players in the game. The player \"{turn}\" is not declared");

            }
        }
        private void SetBelongingOfTheChip(Models.ChipState chip, Button button)
        {
            switch (chip)
            {
                case Models.ChipState.Player1:
                    button.Background = GameLogic.ChipColors.player1Color;
                    break;
                case Models.ChipState.Player2:
                    button.Background = GameLogic.ChipColors.player2Color;
                    break;
                case Models.ChipState.None:
                    button.Background = GameLogic.ChipColors.defaultColor;
                    break;
                default:
                    throw new NotImplementedException($"There is no other options for belonging chips. The state \"{chip}\" is not declared");

            }
        }
        private void Button_a4_Click(object sender, RoutedEventArgs e)
        {
            var button = (Button)sender;
            GameLogic.MovingLogic.TotalMove(Models.ButtonPosition.a4, ref prevButton, ref button, ref chipIsTakenToSet,
                ref warningExists, ref inProcessOfTakingChip,
                ref warningText);
            WarningText.Text = warningText;
        }
        private void Button_d5_Click(object sender, RoutedEventArgs e)
        {
            var button = (Button)sender;
            GameLogic.MovingLogic.TotalMove(Models.ButtonPosition.d5, ref prevButton, ref button, ref chipIsTakenToSet, ref warningExists, ref inProcessOfTakingChip,
                ref warningText);
            WarningText.Text = warningText;
        }

        private void Button_b2_Click(object sender, RoutedEventArgs e)
        {
            var button = (Button)sender;
            GameLogic.MovingLogic.TotalMove(Models.ButtonPosition.b2, ref prevButton, ref button, ref chipIsTakenToSet,
                ref warningExists, ref inProcessOfTakingChip,
                ref warningText);
            WarningText.Text = warningText;
        }
        private void Button_a1_Click(object sender, RoutedEventArgs e)
        {
            var button = (Button)sender;
            GameLogic.MovingLogic.TotalMove(Models.ButtonPosition.a1, ref prevButton, ref button, ref chipIsTakenToSet, ref warningExists, ref inProcessOfTakingChip,
            ref warningText);
            WarningText.Text = warningText;
        } 

        private void Button_a7_Click(object sender, RoutedEventArgs e)
        {
            var button = (Button)sender;
            GameLogic.MovingLogic.TotalMove(Models.ButtonPosition.a7, ref prevButton, ref button, ref chipIsTakenToSet, ref warningExists, ref inProcessOfTakingChip,
            ref warningText);
            WarningText.Text = warningText;
        }

        private void Button_d7_Click(object sender, RoutedEventArgs e)
        {
            var button = (Button)sender;
            GameLogic.MovingLogic.TotalMove(Models.ButtonPosition.d7, ref prevButton, ref button, ref chipIsTakenToSet, ref warningExists, ref inProcessOfTakingChip,
            ref warningText);
            WarningText.Text = warningText;
        }

        private void Button_g7_Click(object sender, RoutedEventArgs e)
        {
            var button = (Button)sender;
            GameLogic.MovingLogic.TotalMove(Models.ButtonPosition.g7, ref prevButton, ref button, ref chipIsTakenToSet, ref warningExists, ref inProcessOfTakingChip,
            ref warningText);
            WarningText.Text = warningText;
        }

        private void Button_g4_Click(object sender, RoutedEventArgs e)
        {
            var button = (Button)sender;
            GameLogic.MovingLogic.TotalMove(Models.ButtonPosition.g4, ref prevButton, ref button, ref chipIsTakenToSet, ref warningExists, ref inProcessOfTakingChip,
            ref warningText);
            WarningText.Text = warningText;
        }

        private void Button_g1_Click(object sender, RoutedEventArgs e)
        {
            var button = (Button)sender;
            GameLogic.MovingLogic.TotalMove(Models.ButtonPosition.g1, ref prevButton, ref button, ref chipIsTakenToSet, ref warningExists, ref inProcessOfTakingChip,
            ref warningText);
            WarningText.Text = warningText;
        }

        private void Button_d2_Click(object sender, RoutedEventArgs e)
        {
            var button = (Button)sender;
            GameLogic.MovingLogic.TotalMove(Models.ButtonPosition.d2, ref prevButton, ref button, ref chipIsTakenToSet, ref warningExists, ref inProcessOfTakingChip,
            ref warningText);
            WarningText.Text = warningText;
        }

        private void Button_f4_Click(object sender, RoutedEventArgs e)
        {
            var button = (Button)sender;
            GameLogic.MovingLogic.TotalMove(Models.ButtonPosition.f4, ref prevButton, ref button, ref chipIsTakenToSet, ref warningExists, ref inProcessOfTakingChip,
                ref warningText);
            WarningText.Text = warningText;
        }

        private void Button_e4_Click(object sender, RoutedEventArgs e)
        {
            var button = (Button)sender;
            GameLogic.MovingLogic.TotalMove(Models.ButtonPosition.e4, ref prevButton, ref button, ref chipIsTakenToSet, ref warningExists, ref inProcessOfTakingChip,
                ref warningText);
            WarningText.Text = warningText;
        }

        private void Button_c4_Click(object sender, RoutedEventArgs e)
        {
            var button = (Button)sender;
            GameLogic.MovingLogic.TotalMove(Models.ButtonPosition.c4, ref prevButton, ref button, ref chipIsTakenToSet, ref warningExists, ref inProcessOfTakingChip,
                ref warningText);
            WarningText.Text = warningText;
        }

        private void Button_b4_Click(object sender, RoutedEventArgs e)
        {
            var button = (Button)sender;
            GameLogic.MovingLogic.TotalMove(Models.ButtonPosition.b4, ref prevButton, ref button, ref chipIsTakenToSet, ref warningExists, ref inProcessOfTakingChip,
                ref warningText);
            WarningText.Text = warningText;
        }

        private void Button_c3_Click(object sender, RoutedEventArgs e)
        {
            var button = (Button)sender;
            GameLogic.MovingLogic.TotalMove(Models.ButtonPosition.c3, ref prevButton, ref button, ref chipIsTakenToSet, ref warningExists, ref inProcessOfTakingChip,
                ref warningText);
            WarningText.Text = warningText;
        }

        private void Button_d3_Click(object sender, RoutedEventArgs e)
        {
            var button = (Button)sender;
            GameLogic.MovingLogic.TotalMove(Models.ButtonPosition.d3, ref prevButton, ref button, ref chipIsTakenToSet, ref warningExists, ref inProcessOfTakingChip,
                ref warningText);
            WarningText.Text = warningText;
        }

        private void Button_e3_Click(object sender, RoutedEventArgs e)
        {
            var button = (Button)sender;
            GameLogic.MovingLogic.TotalMove(Models.ButtonPosition.e3, ref prevButton, ref button, ref chipIsTakenToSet, ref warningExists, ref inProcessOfTakingChip,
                ref warningText);
            WarningText.Text = warningText;
        }

        private void Button_c5_Click(object sender, RoutedEventArgs e)
        {
            var button = (Button)sender;
            GameLogic.MovingLogic.TotalMove(Models.ButtonPosition.c5, ref prevButton, ref button, ref chipIsTakenToSet, ref warningExists, ref inProcessOfTakingChip,
                ref warningText);
            WarningText.Text = warningText;
        }

        private void Button_e5_Click(object sender, RoutedEventArgs e)
        {
            var button = (Button)sender;
            GameLogic.MovingLogic.TotalMove(Models.ButtonPosition.e5, ref prevButton, ref button, ref chipIsTakenToSet, ref warningExists, ref inProcessOfTakingChip,
                ref warningText);
            WarningText.Text = warningText;
        }

        private void Button_b6_Click(object sender, RoutedEventArgs e)
        {
            var button = (Button)sender;
            GameLogic.MovingLogic.TotalMove(Models.ButtonPosition.b6, ref prevButton, ref button, ref chipIsTakenToSet, ref warningExists, ref inProcessOfTakingChip,
                ref warningText);
            WarningText.Text = warningText;
        }

        private void Button_f2_Click(object sender, RoutedEventArgs e)
        {
            var button = (Button)sender;
            GameLogic.MovingLogic.TotalMove(Models.ButtonPosition.f2, ref prevButton, ref button, ref chipIsTakenToSet, ref warningExists, ref inProcessOfTakingChip,
                ref warningText);
            WarningText.Text = warningText;
        }

        private void Button_d6_Click(object sender, RoutedEventArgs e)
        {
            var button = (Button)sender;
            GameLogic.MovingLogic.TotalMove(Models.ButtonPosition.d6, ref prevButton, ref button, ref chipIsTakenToSet, ref warningExists, ref inProcessOfTakingChip,
                ref warningText);
            WarningText.Text = warningText;
        }

        private void Button_f6_Click(object sender, RoutedEventArgs e)
        {
            var button = (Button)sender;
            GameLogic.MovingLogic.TotalMove(Models.ButtonPosition.f6, ref prevButton, ref button, ref chipIsTakenToSet, ref warningExists, ref inProcessOfTakingChip,
                ref warningText);
            WarningText.Text = warningText;
        }

        private void Button_d1_Click(object sender, RoutedEventArgs e)
        {
            var button = (Button)sender;
            GameLogic.MovingLogic.TotalMove(Models.ButtonPosition.d1, ref prevButton, ref button, ref chipIsTakenToSet, ref warningExists, ref inProcessOfTakingChip,
                ref warningText);
            WarningText.Text = warningText;
        }
        private void Back_To_Main_Menu_Button_Click(object sender, RoutedEventArgs e)
        {
            Models.GameState.ResetGameState();
            _content.mainFrame.GoBack();
        }

        private void Save_Button_Click(object sender, RoutedEventArgs e)
        {
            Windows.SaveWindow sw = new(_content);
            sw.Topmost = true;
            sw.Show();
        }

    }
}
