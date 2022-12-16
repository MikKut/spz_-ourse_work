using System;
using System.Collections.Generic;

namespace NineMensMorris.GameLogic
{
    public static class MovingLogic
    {
        private static event Predicate<bool> _resetWarningEvent;
        public static event Predicate<bool> ResetWarningEvent
        {
            add
            {
                if(_resetWarningEvent == null)
                {
                    _resetWarningEvent += value;
                }
            }
            remove
            {
                _resetWarningEvent -= value;
            }
        }
        public static void TotalMove(in Models.ButtonPosition currentButtonPosition, ref Models.ButtonPosition prevButtonPosition,
          ref System.Windows.Controls.Button currentButton, ref bool chipIsTakenToSet, ref bool warningExists,
          ref bool inProcessOfTakingAnotherChip, ref string warningText)
        {
            if (Models.GameState.GamePeriod != Models.PeriodOfTheGame.SetUp)
            {
                //to move from one position to another you should since take the chip and then set it
                if (!chipIsTakenToSet)
                {
                    if (!TakeChipCorrectly(currentButtonPosition, ref currentButton, ref warningExists, ref warningText, false)) return;
                    chipIsTakenToSet = true;
                    prevButtonPosition = currentButtonPosition;
                    return;
                }
                else
                {

                    if (!inProcessOfTakingAnotherChip)
                    {
                        if (!CheckIfThreeChipsIsLeft())
                        {
                            if (!CheckIfInNeighbourPositionCorrectly(currentButtonPosition, prevButtonPosition, ref warningExists, ref warningText)) return;
                        }
                        if (!SetChipCorrectly(currentButtonPosition, ref currentButton, ref warningExists, ref warningText)) return;
                        inProcessOfTakingAnotherChip = CheckIfCanTakeOponentsChip(Models.GameState.TurnOfThePlayer, currentButtonPosition);
                        if (inProcessOfTakingAnotherChip)
                        {
                            return;
                        }
                    }
                    else
                    {
                        if (!TakeChipCorrectly(currentButtonPosition, ref currentButton, ref warningExists, ref warningText, true))
                        {
                            warningText = $"Cannot take the chip by the adress {currentButtonPosition}: {prevButtonPosition} forms a mill, try again";
                            warningExists = true;
                            return;
                        }
                        inProcessOfTakingAnotherChip = false;
                    }
                    chipIsTakenToSet = false;
                    EndTurnSuccesfully(currentButtonPosition, ref prevButtonPosition, ref warningExists);
                }
            }
            else
            {
                GameLogic.MovingLogic.MakeOneIterationOfSettingUp(currentButtonPosition, currentButton, ref inProcessOfTakingAnotherChip, ref warningText, ref warningExists);
                if (!warningExists)
                {
                    if (!inProcessOfTakingAnotherChip)
                    {
                        Models.GameState.GiveTheTurnToOponent();
                        warningText = "No warnings";
                    }
                    else
                    {
                        warningText = $"Take a chip of {Models.GameState.GetOponentsTurn()}";
                    }
                }
            }
        }

        private static bool TakeTheChip(System.Windows.Controls.Button button,
             in Models.ButtonPosition buttonPosition)
        {
            if (Models.GameState.GamePeriod != Models.PeriodOfTheGame.HuntingTime)
            {
                if (ChipBelongsToAPlayer(buttonPosition))
                {
                    return false;
                }
            }
            else
            {
                if (Models.GameState.QuantityOfChipsOfPlayer1 == 3 || Models.GameState.QuantityOfChipsOfPlayer2 == 3)
                {
                    if (!ChipBelongsToAPlayer(buttonPosition))
                    {
                        return false;
                    }
                }
                else
                {
                    if (!ChipBelongsToAPlayer(buttonPosition) || !HasFreeNeigbourCells(buttonPosition))
                    {
                        return false;
                    }
                }
            }
            button.Background = ChipColors.defaultColor;
            Models.GameState.ButtonStates[buttonPosition] = Models.ChipState.None;
            return true;
        }
        private static bool HasFreeNeigbourCells(in Models.ButtonPosition currentButtonPosition)
        {
            var col = CollectionOfLines.GetListOfNeighbours(currentButtonPosition);
            foreach(var ar in col)
            {
                foreach(var element in ar)
                {
                    if(Models.GameState.ButtonStates[element] == Models.ChipState.None)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        private static bool CheckIfThreeChipsIsLeft()
        {
            if(Models.GameState.TurnOfThePlayer == Models.Turn.Player1)
            {
                if(Models.GameState.QuantityOfChipsOfPlayer1 == 3)
                {
                    return true;
                }
            }
            else
            {
                if (Models.GameState.QuantityOfChipsOfPlayer2 == 3)
                {
                    return true;
                }
            }
            return false;
        }
        private static void ChangeChipsQuantity(bool increment, in Models.Turn playerToChange)
        {
            if(increment)
            {
                if (Models.Turn.Player1 == playerToChange)
                {
                    Models.GameState.IncrementQuantityOfChipsOfPlayer1();
                }
                else
                {
                    Models.GameState.IncrementQuantityOfChipsOfPlayer2();
                }
            }
            else
            {
                if (Models.Turn.Player1 == playerToChange)
                {
                    Models.GameState.DecrementQuantityOfChipsOfPlayer1();
                }
                else
                {
                    Models.GameState.DecrementQuantityOfChipsOfPlayer2();
                }
            }
        }
      
        private static void EndTurnSuccesfully(in Models.ButtonPosition buttonPosition, ref Models.ButtonPosition prevButton, ref bool warningExists)
        {
            Models.GameState.GiveTheTurnToOponent(); 
            warningExists = _resetWarningEvent.Invoke(warningExists);

        }
        private static bool CheckIfInNeighbourPositionCorrectly(in Models.ButtonPosition currentButtonPosition, in Models.ButtonPosition prevButtonPosition,  ref bool warningExists, ref string warningText)
        {
            if (!GameLogic.MovingLogic.ChipIsLocatedInNeighbourPosition(prevButtonPosition, CollectionOfLines.GetListOfNeighbours(currentButtonPosition)))
            {
                warningText = $"Cannot set the chip by the adress {currentButtonPosition}. Reason: only neighbour to {prevButtonPosition} is allowed, try again";
                warningExists = true;
                return false;
            }
            warningExists = false;
            return true;
        }

        private static bool SetChipCorrectly(in Models.ButtonPosition currentButtonPosition, ref System.Windows.Controls.Button currentButton, ref bool warningExists, ref string warningText)
        {
            if (!GameLogic.MovingLogic.SetChipOnThePosition(currentButton, currentButtonPosition, Models.GameState.TurnOfThePlayer))
            {
                warningText = $"Cannot set the chip by the adress {currentButtonPosition}, try again";
                warningExists = true;
                return false;
            }
            if (Models.GameState.GamePeriod == Models.PeriodOfTheGame.SetUp)
            {
                ChangeChipsQuantity(true, Models.GameState.TurnOfThePlayer);
            }
            warningExists = false;
            return true;
        }

        private static bool TakeChipCorrectly(in Models.ButtonPosition currentButtonPosition, ref System.Windows.Controls.Button currentButton, ref bool warningExists, ref string warningText, bool oponent)
        {
            if (!oponent)
            {
                if (!ChipBelongsToThePlayer(currentButtonPosition, Models.GameState.TurnOfThePlayer) || !GameLogic.MovingLogic.TakeTheChip(currentButton, currentButtonPosition))
                {
                    warningText = $"Cannot take the chip by the adress {currentButtonPosition}, try again";
                    warningExists = true;
                    return false;
                }
            }
            else
            {
                if (ChipBelongsToThePlayer(currentButtonPosition, Models.GameState.TurnOfThePlayer) || !GameLogic.MovingLogic.TakeOponentsChip(currentButtonPosition, Models.GameState.TurnOfThePlayer, currentButton))
                {
                    warningText = $"Cannot take the chip by the adress {currentButtonPosition}, try again";
                    warningExists = true;
                    return false;
                }
                ChangeChipsQuantity(false, Models.GameState.GetOponentsTurn());
            }
            _resetWarningEvent.Invoke(true);
            warningExists = false;
            return true;
        }
        private static bool ChipBelongsToAPlayer(in Models.ButtonPosition buttonPosition)
        {
            if (Models.GameState.ButtonStates[buttonPosition] == Models.ChipState.Player1
               || Models.GameState.ButtonStates[buttonPosition] == Models.ChipState.Player2)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        private static bool ChipBelongsToThePlayer(in Models.ButtonPosition buttonPosition, in Models.Turn playerToCheck)
        {
            if ((byte)Models.GameState.ButtonStates[buttonPosition] == (byte)playerToCheck)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        private static void MakeOneIterationOfSettingUp(in Models.ButtonPosition buttonPosition, System.Windows.Controls.Button button, ref bool inProcessOfTakingChip, ref string warningText, ref bool warningExists)
        {
            if (inProcessOfTakingChip )
            {
                if (TakeChipCorrectly(buttonPosition, ref button, ref warningExists, ref warningText, true))
                {
                    inProcessOfTakingChip = false;
                }
            }
            else
            {
                SetChipCorrectly(buttonPosition, ref button, ref warningExists, ref warningText);
                inProcessOfTakingChip = GameLogic.MovingLogic.CheckIfCanTakeOponentsChip(Models.GameState.TurnOfThePlayer, buttonPosition);
            }
        }
        private static bool ChipIsLocatedInNeighbourPosition(in Models.ButtonPosition currentPos,
            List<List<Models.ButtonPosition>> neighbourButtons)
        {
            foreach(var neighbourList in neighbourButtons)
            {
                foreach (var neighbour in neighbourList)
                {
                    if (neighbour == currentPos)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        private static bool SetChipOnThePosition(System.Windows.Controls.Button button, 
             in Models.ButtonPosition buttonPosition, in Models.Turn turn)
        {
            if (ChipBelongsToAPlayer(buttonPosition))
            {
                return false;
            }
            if (turn == Models.Turn.Player1)
            {
                button.Background = ChipColors.player1Color;
                Models.GameState.ButtonStates[buttonPosition] = Models.ChipState.Player1;
            }
            else
            {
                button.Background = ChipColors.player2Color;
                Models.GameState.ButtonStates[buttonPosition] = Models.ChipState.Player2;
            }
            return true;
        }
        
        private static bool CheckIfCanTakeOponentsChip(in Models.Turn turn, in Models.ButtonPosition previousSetButton)
        {
            bool isEqual = false;
            foreach (var ar in CollectionOfLines.MatchingLinesForTheButton[previousSetButton])
            {
                foreach (var chip in ar)
                {
                    if ((int)Models.GameState.ButtonStates[chip] != (int)turn)
                    {
                        isEqual = false;
                        break;
                    }
                    else
                    {
                        isEqual = true;
                    }
                }
                if (isEqual)
                {
                    return true;
                }
            }
            return false; 
        }
        private static bool TakeOponentsChip(in Models.ButtonPosition buttonPosition,in Models.Turn currentTurn, System.Windows.Controls.Button targetButton)
        {
            if (!ChipBelongsToAPlayer(buttonPosition))
            {
                return false;
            }
            var targetChipState = Models.GameState.GetOponentsTurn();
            if ((byte)targetChipState != (byte)currentTurn)
            {
                if (!CheckIfTheChipFormsAMill(buttonPosition, targetChipState))
                {
                    targetButton.Background = ChipColors.defaultColor;
                    Models.GameState.ButtonStates[buttonPosition] = Models.ChipState.None;
                    return true;
                }
            }
            return false;
        }
        private static bool CheckIfTheChipFormsAMill(in Models.ButtonPosition buttonPosition, in Models.Turn whoseMillMustBeChecked)
        {
            if((byte)Models.GameState.ButtonStates[buttonPosition] != (byte)whoseMillMustBeChecked)
            {
                return true;
            }
            byte whoseMillIsChecking = (byte)whoseMillMustBeChecked;
            bool millIsNotLockated = false;
            var arrays = CollectionOfLines.MatchingLinesForTheButton[buttonPosition];
            foreach (var array in arrays)
            {
               foreach (var line in array)
               {
                    if ((byte)Models.GameState.ButtonStates[line] != whoseMillIsChecking)
                    {
                        millIsNotLockated = true;
                        break;
                    }
                    else
                    {
                        millIsNotLockated = false;
                    }
               }
                if (!millIsNotLockated)
                {
                    return true;
                }
            }
            return false;
        }
        private static bool CheckWhetherTheChipOnTheLineWithTheButton(in Models.ButtonPosition buttonPosition)
        {
            bool onTheLineWithTheChip = false;
            var arrays = CollectionOfLines.MatchingLinesForTheButton[buttonPosition];
            foreach (var array in arrays)
            {
                if (!onTheLineWithTheChip)
                {
                    foreach (var line in array)
                    {
                        if (buttonPosition == line)
                        {
                            onTheLineWithTheChip = true;
                        }
                        else
                        {
                            onTheLineWithTheChip = false;
                            break;
                        }
                    }
                }
                else
                {
                    break;
                }
            }
            return onTheLineWithTheChip;
        }

    }
}
