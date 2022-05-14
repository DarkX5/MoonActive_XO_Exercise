using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using XO.Core;

public class UndoTestScript
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

        prefabObject = new GameObject();
        prefabObject = MonoBehaviour.Instantiate(Resources.Load<GameObject>("Prefabs/Game Handler"));
        prefabObject.SetActive(true);
        gameHandler = prefabObject.GetComponent<GameHandler>();

        prefabObject = new GameObject();
        prefabObject = MonoBehaviour.Instantiate(Resources.Load<GameObject>("Prefabs/Game Buttons Canvas"));
        prefabObject.SetActive(true);
        xoActionHandler = prefabObject.GetComponent<XOActionsHandler>();

        dataLoader.Init();
        dataLoader.SetGamePlayers(new PlayerTypes[] { PlayerTypes.Player, PlayerTypes.AI });
        gameHandler.Init();
        xoActionHandler.Init();

        gameHandler.StartNewGame();
    } 

    [UnityTest]
    public IEnumerator TestUndoAction()
    {
        // test undo
        gameHandler.UndoLastMove();

        // wait for object creation and initial setup
        yield return new WaitForSeconds(0.25f);

        var currentPlayer = gameHandler.CurrentPlayer;
        var turn = 1;

        // check turn is still first
        Assert.IsTrue(gameHandler.TurnNo == turn);

        // check first player selected
        Assert.IsTrue(gameHandler.CurrentPlayer == 0);

        Debug.Log($"xoAH: {xoActionHandler.BoardSize}");

        xoActionHandler.Move(xoActionHandler.GetHintPosition());
        xoActionHandler.Move(xoActionHandler.GetHintPosition());
        xoActionHandler.Move(xoActionHandler.GetHintPosition());
        xoActionHandler.Move(xoActionHandler.GetHintPosition());

        turn = (int)gameHandler.TurnNo;
        currentPlayer = gameHandler.CurrentPlayer;
        Debug.Log($"TurnNO: {gameHandler.TurnNo} || turn {turn}");
        
        // test undo
        gameHandler.UndoLastMove();

        Debug.Log($"UNDO TurnNO: {gameHandler.TurnNo} || turn {turn}");

        /*v TODO - check/fix test v*/
        // // check turn moved back 2 units
        // Assert.IsTrue(gameHandler.TurnNo == turn - 2);

        Debug.Log($"TurnNO: {gameHandler.CurrentPlayer} || turn {currentPlayer}");
        // check same player selected
        Assert.IsTrue(gameHandler.CurrentPlayer == currentPlayer);

    }
}