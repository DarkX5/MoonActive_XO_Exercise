using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace XO.Core
{

    public enum WinType
    {
        Horizontal, Vertical, Diagonal1, Diagonal2
    }
    public class XOActionsHandler : MonoBehaviour
    {
        public static event Action<WinType, int> onWinningStreak = null;

        private XOContainerActions[] buttonList = null;
        private uint[,] gameBoard = null;


        // cache free spaces
        List<int> freeSpaces = new List<int>();
        private uint boardSize;
        private uint minWinPoints;
        private bool hintUsed = false;

        void Start()
        {
            // get game data
            boardSize = GameHandler.Instance.BoardSize;
            minWinPoints = GameHandler.Instance.MinWinPoints;

            // get buttons and set their IDs
            buttonList = GetComponentsInChildren<XOContainerActions>();
            for (uint i = 0; i < buttonList.Length; i += 1)
            {
                buttonList[i].SetMoveID(i);
            }

            gameBoard = new uint[boardSize, boardSize];
            freeSpaces = new List<int>();

            // subscribe to turn end event
            GameHandler.onEndTurn += CheckValues;
            GameHandler.onMoveAI += AIMove;
            GameHandler.onUndoTurn += EnableButton;
        }
        private void OnDestroy()
        {
            // unsubscribe to turn end event
            GameHandler.onEndTurn -= CheckValues;
            GameHandler.onMoveAI -= AIMove;
            GameHandler.onUndoTurn -= EnableButton;
        }

        private void UpdateGameBoardData()
        {
            freeSpaces.Clear();
            // update gameBoard data (PlayerID's) - use to make sure gameBoard data stays in sync with what is on screen
            for (int i = 0; i < boardSize; i += 1)
            {
                for (int j = 0, idx; j < boardSize; j += 1)
                {
                    idx = (int)(i * boardSize) + j;
                    gameBoard[i, j] = buttonList[idx].PlayerID;

                    // get empty positions
                    if (buttonList[idx].IsButtonEnabled != false)
                    {
                        freeSpaces.Add(idx);
                    }

                    if (hintUsed)
                    {
                        // clear previous hint
                        buttonList[idx].DeactivateHintButton();
                    }
                }
            }
        }
        private void CheckValues(uint turnNo)
        {
            UpdateGameBoardData();

            uint hPointsID, vPointsID, d1PointsID, d2PointsID;
            uint hPoints, vPoints, d1Points = 1, d2Points = 1;
            long invertedI;
            bool movesLeft = false;
            // check horizontal & vertical lines
            for (int i = 0; i < boardSize; i += 1)
            {
                // reset points & possible winner
                hPointsID = vPointsID = d1PointsID = d2PointsID = 0;
                hPoints = vPoints = 1;
                if (gameBoard[i, 0] < 1 || gameBoard[0, i] < 1)
                {
                    movesLeft = true;
                }
                for (int j = 1; j < boardSize; j += 1)
                {
                    // check horizontal
                    if (gameBoard[i, j] > 0)
                    {
                        if (gameBoard[i, j - 1] == gameBoard[i, j])
                        {
                            hPointsID = gameBoard[i, j];
                            hPoints += 1;
                        }
                    }
                    else
                    {
                        movesLeft = true;
                    }

                    // check vertical
                    if (gameBoard[j, i] > 0)
                    {
                        if (gameBoard[j - 1, i] == gameBoard[j, i])
                        {
                            vPointsID = gameBoard[j, i];
                            vPoints += 1;
                        }
                    }
                }

                // check diagonals 
                if (i > 0)
                {
                    if (gameBoard[i, i] > 0)
                    {
                        if (gameBoard[i, i] == gameBoard[i - 1, i - 1])
                        {
                            d1PointsID = gameBoard[i, i];
                            d1Points += 1;
                        }
                        else
                        {
                            d1Points = 1;
                        }

                        // check points for diagonal 1 win
                        if (d1Points >= minWinPoints)
                        {
                            onWinningStreak?.Invoke(WinType.Diagonal1, 0);
                            GameHandler.Instance.WinGame(d1PointsID);
                            movesLeft = true;
                            return;
                        }
                    }

                    invertedI = boardSize - 1 - i;
                    if (gameBoard[i, invertedI] > 0)
                    {
                        if (gameBoard[i, invertedI] == gameBoard[i - 1, invertedI + 1])
                        {
                            d2PointsID = gameBoard[i, invertedI];
                            d2Points += 1;
                        }
                        else
                        {
                            d2Points = 1;
                        }

                        // check points for diagonal 2 win
                        if (d2Points >= minWinPoints)
                        {
                            onWinningStreak?.Invoke(WinType.Diagonal2, 0);
                            GameHandler.Instance.WinGame(d2PointsID);
                            movesLeft = true;
                            return;
                        }
                    }
                }

                // check points for horizontal win
                if (hPoints >= minWinPoints)
                {
                    onWinningStreak?.Invoke(WinType.Horizontal, i);
                    GameHandler.Instance.WinGame(hPointsID);
                    movesLeft = true;
                    return;
                }

                // check points for vertical win
                if (vPoints >= minWinPoints)
                {
                    onWinningStreak?.Invoke(WinType.Vertical, i);
                    GameHandler.Instance.WinGame(vPointsID);
                    movesLeft = true;
                    return;
                }
            }

            // no moves left - game ended
            if (!movesLeft)
            {
                GameHandler.Instance.DrawGame();
            }
        }

        public void AIMove()
        {
            UpdateGameBoardData();

            if (freeSpaces.Count < 1) { return; }

            // get random empty position button index
            int idx = freeSpaces[UnityEngine.Random.Range((int)0, (int)freeSpaces.Count)];
            // activate position button
            buttonList[idx].AIActivateButton();
        }

        public void HintMove()
        {
            hintUsed = true;
            UpdateGameBoardData();

            if (freeSpaces.Count < 1) { return; }

            // get random empty position button index
            int idx = freeSpaces[UnityEngine.Random.Range((int)0, (int)freeSpaces.Count)];

            // activate position button
            buttonList[idx].ActivateHintButton();
        }

        private void EnableButton(uint buttonID)
        {
            buttonList[buttonID].EnableButton();
        }
    }
}