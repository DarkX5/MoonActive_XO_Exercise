using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace XO.Core
{
    public class GameHandler : MonoBehaviour
    {
        public static GameHandler Instance { get; private set; }
        // used for UI updates
        public static event Action<uint> onNextTurn;
        // used for checking victory condition & some UI updates
        public static event Action<uint> onEndTurn;
        // UI updates
        public static event Action<uint> onGameEnd;
        public static event Action onGameStart;
        public static event Action onGameDraw;
        // AI Moves
        // public static event Action onMoveAI;
        public static event Action<uint> onUndoTurn;
        public static event Action<uint> onGameButtonMove = null;

        [Header("----------------- Settings")]
        // [SerializeField] private bool enableEnemyAI = false;
        [SerializeField] private bool fastTurns = false;
        [SerializeField] private bool randomStartOrder = true;
        [SerializeField][Range(0.1f, 5f)] private float startUpdateFrequency = 0.25f;

        [Header("----------------- Auto-Set")]
        [SerializeField] private PlayerTypes[] playerTypes = null;
        [SerializeField] private List<uint> movesList = null;

        /*v TODO - implement variable board size v*/
        [SerializeField][Range(3, 100)] private uint boardSize = 3;
        /*^ TODO - implement variable board size  ^*/
        [SerializeField][Range(3, 100)] private uint minWinPoints = 3;

        private uint playerNo = 2;
        private PlayerController[] playerList = null;
        private uint turn = 1;
        private uint currentPlayer = 0;
        // private uint aiEnemyPlayer = 1;
        // private bool aiMovesFirst;
        private bool gameEnded = false;

        public uint PlayerNo { get { return playerNo; } }
        public uint BoardSize { get { return boardSize; } }
        public uint MinWinPoints { get { return minWinPoints; } }
        public uint CurrentPlayer { get { return currentPlayer; } }
        public bool IsPlayerActive { get { return (playerList?[currentPlayer].GetPlayerType() == PlayerTypes.Player); } }
        // public bool IsPlayerActive { get { return (!enableEnemyAI) || (enableEnemyAI && currentPlayer != aiEnemyPlayer); } }
        public bool FastTurns { get { return fastTurns; } }
        private void Awake()
        {
            if (!Instance)
                Instance = this;
            else
                Destroy(Instance);
        }

        void Start()
        {
            onGameStart?.Invoke();
            movesList = new List<uint>();
            StartCoroutine(StartGameAfterDataLoadCO());
        }
        IEnumerator StartGameAfterDataLoadCO()
        {
            yield return new WaitForSeconds(startUpdateFrequency);
            if (DataLoader.Instance.IsDataLoaded)
            {
                StartNewGame();
            }
            else
            {
                StartCoroutine(StartGameAfterDataLoadCO());
            }
        }

        // called from UI on "slow" turns
        public void StartNewGame()
        {
            currentPlayer = 0;
            turn = 1;

            /*v TODO - remove next 1 line - only 4 debugging v*/
            playerTypes = DataLoader.Instance.PlayerTypesList;

            // get player List
            playerList = DataLoader.Instance.PlayerList;
            playerNo = (uint)playerList.Length;
            
            if (randomStartOrder) {
                // randomize player list order
                int randIdx = 0;
                for(int i = 0; i < playerList.Length; i += 1) {
                    randIdx = UnityEngine.Random.Range((int)0, (int)playerList.Length);
                    if (randIdx != i) {
                        var auxPC = playerList[randIdx];
                        playerList[randIdx] = playerList[i];
                        playerList[i] = auxPC;
                    }
                }
            }
            
            playerList[currentPlayer].Move();
        }

        public void TimeEndGame()
        {
            gameEnded = true;
            if (currentPlayer < 1)
            {
                onGameEnd?.Invoke(playerNo - 1);
            }
            else
            {
                onGameEnd?.Invoke(currentPlayer - 1);
            }
        }
        public void WinGame(uint winningPlayerID)
        {
            gameEnded = true;
            onGameEnd?.Invoke(winningPlayerID - 1);
        }
        public void DrawGame()
        {
            gameEnded = true;
            onGameDraw?.Invoke();
        }
        // called from UI
        public void EndTurn()
        {
            if (gameEnded) { return; }
            turn += 1;
            currentPlayer += 1;
            if (currentPlayer >= playerNo)
            {
                currentPlayer = 0;
            }

            onEndTurn?.Invoke(turn);
        }

        // called from UI (Next Turn Mask Canvas)
        public void NextTurn()
        {
            if (gameEnded) { return; }

            onNextTurn?.Invoke(turn);

            playerList[currentPlayer].Move();
            // if (enableEnemyAI && currentPlayer == aiEnemyPlayer)
            // {
            //     onMoveAI?.Invoke();
            // }
        }

        // called from UI (button on-click event)
        public void UndoLastMove()
        {
            if (movesList == null || movesList.Count < 1 || playerList[currentPlayer].GetPlayerType() != PlayerTypes.Player) { return; }
            // if (movesList == null || movesList.Count < 1 || !enableEnemyAI || currentPlayer == aiEnemyPlayer) { return; }

            turn -= 2;
            if (turn < 1)
            {
                turn = 1;
                currentPlayer = 0;
            }

            // re-enable button, reset button UI & remove from list
            if (movesList.Count > 1)
            {
                onUndoTurn?.Invoke(movesList[movesList.Count - 1]);
                movesList.RemoveAt(movesList.Count - 1);
            }
            if (movesList.Count > 0)
            {
                onUndoTurn?.Invoke(movesList[movesList.Count - 1]);
                movesList.RemoveAt(movesList.Count - 1);
            }

            NextTurn();
        }

        public void AddMove(uint buttonID)
        {
            movesList.Add(buttonID);
            onGameButtonMove?.Invoke(buttonID);
        }
    }
}