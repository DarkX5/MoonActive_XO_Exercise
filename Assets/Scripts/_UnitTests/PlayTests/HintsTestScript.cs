using System.Collections;
// using System.Collections.Generic;
using NUnit.Framework;
// using UnityEditor;
using UnityEngine;
using UnityEngine.TestTools;
using XO.Core;

public class HintsTestScript
{
    DataLoader dataLoader;
    GameHandler gameHandler;
    XOActionsHandler xoActionHandler;

    GameObject prefabObject;

    [SetUp]
    public void Setup()
    {
        prefabObject = new GameObject();
        prefabObject = MonoBehaviour.Instantiate(Resources.Load<GameObject>("Prefabs/Data Loader"));
        prefabObject.SetActive(true);
        dataLoader = prefabObject.GetComponent<DataLoader>();
        dataLoader.SetGamePlayers(new PlayerTypes[] { PlayerTypes.Player, PlayerTypes.AI });

        prefabObject = new GameObject();
        prefabObject = MonoBehaviour.Instantiate(Resources.Load<GameObject>("Prefabs/Game Handler"));
        prefabObject.SetActive(true);
        gameHandler = prefabObject.GetComponent<GameHandler>();

        prefabObject = new GameObject();
        prefabObject = MonoBehaviour.Instantiate(Resources.Load<GameObject>("Prefabs/Game Buttons Canvas"));
        prefabObject.SetActive(true);
        xoActionHandler = prefabObject.GetComponent<XOActionsHandler>();

        gameHandler.StartNewGame();
    }

    [UnityTest]
    public IEnumerator TestGetHintPosition()
    {
        yield return new WaitForSeconds(0.1f);

        DebugLogBoardContents();
        
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

        DebugLogBoardContents();
    }


#region Helpers

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