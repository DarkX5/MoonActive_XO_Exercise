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
    GameEndUpdater gameEndUpdater;
    PlayerTurnTimer playerTurnTimer;

    GameObject prefabObject;

    [UnityTest]
    public IEnumerator TestUndoAction()
    {
        dataLoader.SetGamePlayers(new PlayerTypes[] { PlayerTypes.Player, PlayerTypes.Player });
        // wait for object creation and initial setup
        yield return new WaitForSeconds(0.1f);
    }

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
        dataLoader.SetGamePlayers(new PlayerTypes[] { PlayerTypes.Player, PlayerTypes.AI });

        prefabObject = new GameObject();
        prefabObject = MonoBehaviour.Instantiate(Resources.Load<GameObject>("Prefabs/Game Handler"));
        prefabObject.SetActive(true);
        gameHandler = prefabObject.GetComponent<GameHandler>();

        prefabObject = new GameObject();
        prefabObject = MonoBehaviour.Instantiate(Resources.Load<GameObject>("Prefabs/GAME END Mask Canvas"));
        prefabObject.SetActive(true);
        gameEndUpdater = prefabObject.GetComponent<GameEndUpdater>();

        gameHandler.StartNewGame();
    }

    [UnityTest]
    public IEnumerator TestGameDraw()
    {
        // dataLoader.SetGamePlayers(new PlayerTypes[] { PlayerTypes.Player, PlayerTypes.Player });

        // test game draw 
        gameHandler.DrawGame();

        // wait for object creation and initial setup
        yield return new WaitForSeconds(0.1f);

        // check if gamehandler set game ended
        Assert.IsTrue(gameHandler.GameEnded);

        // check if audio has data for draw
        Assert.IsNotNull(DataLoader.Instance.DrawSFX);
        Assert.IsNotNull(DataLoader.Instance.ButtonClickSFX);

        Assert.IsTrue(gameEndUpdater.IsCanvasActive);
    }

    [UnityTest]
    public IEnumerator TestGameWin()
    {
        // dataLoader.SetGamePlayers(new PlayerTypes[] { PlayerTypes.Player, PlayerTypes.Player });

        // SetupGameEndPrefabs();

        gameHandler.WinGame((uint)Random.Range(1, 3));

        // wait for object creation and initial setup
        yield return new WaitForSeconds(0.1f);

        // check if gamehandler set game ended
        Assert.IsTrue(gameHandler.GameEnded);

        // check if player won set game ended
        Assert.IsTrue(gameHandler.GameEnded);

        // check if audio has data for draw
        Assert.IsNotNull(DataLoader.Instance.VictorySFX);
        Assert.IsNotNull(DataLoader.Instance.ButtonClickSFX);
    }

    [UnityTest]
    public IEnumerator TestGameLose()
    {
        // dataLoader.SetGamePlayers(new PlayerTypes[] { PlayerTypes.Player, PlayerTypes.AI });

        // SetupGameEndPrefabs();

        // wait for object creation and initial setup
        yield return new WaitForSeconds(0.1f);

        gameHandler.WinGame(1);

        // check if gamehandler set game ended
        Assert.IsTrue(gameHandler.GameEnded);

        // check if audio has data for draw
        Assert.IsNotNull(DataLoader.Instance.VictorySFX);
        Assert.IsNotNull(DataLoader.Instance.ButtonClickSFX);
    }
}