using System.Collections;
// using System.Collections.Generic;
using NUnit.Framework;
// using UnityEditor;
using UnityEngine;
using UnityEngine.TestTools;
using XO.Core;

public class PlayTestScript
{
    DataLoader dataLoader;
    GameHandler gameHandler;
    XOActionsHandler xoActionHandler;

    GameObject prefabObject;

    /*v TODO - implement v*/
    [SetUp]
    public void Setup()
    {
        prefabObject = new GameObject();
        prefabObject = MonoBehaviour.Instantiate(Resources.Load<GameObject>("Prefabs/Data Loader"));
        // add audio listener for audio tests
        prefabObject.AddComponent<AudioListener>();
        prefabObject.SetActive(true);
        dataLoader = prefabObject.GetComponent<DataLoader>();

        // var gameButtonsCanvas = PrefabUtility.LoadPrefabContents("Prefabs/Game Buttons Canvas");
    }

    [UnityTest]
    public IEnumerator TestGetHintPosition()
    {
        dataLoader.SetGamePlayers(new PlayerTypes[] { PlayerTypes.Player, PlayerTypes.AI });
        prefabObject = new GameObject();
        prefabObject = MonoBehaviour.Instantiate(Resources.Load<GameObject>("Prefabs/Game Buttons Canvas"));
        prefabObject.SetActive(true);
        xoActionHandler = prefabObject.GetComponent<XOActionsHandler>();

        yield return new WaitForSeconds(0.1f);

        // DebugLogBoardContents();
        
        int newHintPos = -1;
        // fill board
        for (int i = 0, pos = 0; i < xoActionHandler.BoardSize; i += 1)
        {
            for (int j = 0; j < xoActionHandler.BoardSize; j += 1)
            {
                pos = (int)(i * xoActionHandler.BoardSize + j);
                xoActionHandler.HintMove();
                xoActionHandler.Move(pos);

                // check if it works across the game for all positions (NO need to test if positions are valid -> ONLY returns "free"/"unclaimed positions)
                newHintPos = xoActionHandler.GetHintPosition();
                if (i == j && (i >= xoActionHandler.BoardSize - 1)) {
                    // if no more free positions left -> hint should be -1 
                    Assert.Less(newHintPos, 0);
                } else {
                    // free spaces left -> hint should be greater than the current position
                    Assert.Greater(newHintPos, pos);
                }
            }
        }

        // DebugLogBoardContents();
    }

    [UnityTest]
    public IEnumerator TestGameDraw()
    {
        dataLoader.SetGamePlayers(new PlayerTypes[] { PlayerTypes.Player, PlayerTypes.Player });
        prefabObject = new GameObject();
        prefabObject = MonoBehaviour.Instantiate(Resources.Load<GameObject>("Prefabs/Game Handler"));
        prefabObject.SetActive(true);
        gameHandler = prefabObject.GetComponent<GameHandler>();
        
        prefabObject = new GameObject();
        prefabObject = MonoBehaviour.Instantiate(Resources.Load<GameObject>("Prefabs/Audio Manager"));
        prefabObject.SetActive(true);
        var audioHandler = prefabObject.GetComponent<AudioHandler>();

        prefabObject = new GameObject();
        prefabObject = MonoBehaviour.Instantiate(Resources.Load<GameObject>("Prefabs/GAME END Mask Canvas"));
        prefabObject.SetActive(true);
        var gameEndUpdater = prefabObject.GetComponent<GameEndUpdater>();
        var gameEndCanvas = prefabObject.GetComponent<Canvas>();

        prefabObject = new GameObject();
        prefabObject = MonoBehaviour.Instantiate(Resources.Load<GameObject>("Prefabs/Player-Turn Timer Updater"));
        prefabObject.SetActive(true);
        var playerTurnTimer = prefabObject.GetComponent<PlayerTurnTimer>();

        // test game draw 
        gameHandler.DrawGame();

        // wait for object creation and initial setup
        yield return new WaitForSeconds(0.1f);

        // check if gamehandler set game ended
        Assert.IsTrue(gameHandler.GameEnded);

        // check if audio has data for draw
        Assert.IsNotNull(DataLoader.Instance.DrawSFX);
        Assert.IsNotNull(DataLoader.Instance.ButtonClickSFX);

        // check if end game canvas has been disabled
        Assert.IsFalse(gameEndCanvas.enabled);

        // check if timer stopped
        Assert.False(playerTurnTimer.IsGameEnded);
    }

#region Internal Helpers

    private void DebugLogBoardContents()
    {
        Debug.Log($"boardSize: {xoActionHandler?.BoardSize}");
        string board = string.Empty;
        for (int i = 0; i < xoActionHandler.BoardSize; i += 1)
        {
            for (int j = 0; j < xoActionHandler.BoardSize; j += 1)
            {
                board += " " + xoActionHandler.GameBoard[i, j].ToString();
            }
            Debug.Log($"boardSize[{i}, ...]: {board}");
            board = string.Empty;
        }
    }

#endregion

}