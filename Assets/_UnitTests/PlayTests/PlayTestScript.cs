using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class PlayTestScript
{
    /*v TODO - implement v*/
    // // A Test behaves as an ordinary method
    // [Test]
    // public void PlayTestScriptSimplePasses()
    // {
    //     // Use the Assert class to test conditions
    //     Assert.AreEqual(1, 2);
    // }

    // // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // // `yield return null;` to skip a frame.
    // [UnityTest]
    // public IEnumerator PlayTestScriptWithEnumeratorPasses()
    // {
    //     // Use the Assert class to test conditions.
    //     // Use yield to skip a frame.
    //     yield return null;
    // }
    // private Game game;

    [SetUp]
    public void Setup()
    {
        // GameObject gameGameObject =
        //     MonoBehaviour.Instantiate(Resources.Load<GameObject>("Prefabs/Game"));
        // game = gameGameObject.GetComponent<Game>();
    }

    [TearDown]
    public void Teardown()
    {
        // Object.Destroy(game.gameObject);
    }

    // [UnityTest]
    // public IEnumerator AsteroidsMoveDown()
    // {
    //     GameObject asteroid = game.GetSpawner().SpawnAsteroid();
    //     float initialYPos = asteroid.transform.position.y;
    //     yield return new WaitForSeconds(0.1f);

    //     Assert.Less(asteroid.transform.position.y, initialYPos);
    // }

    [Test]
    public void TestHint() {
    }

    [Test]
    public void TestUndo() {
    }

    [Test]
    public void TestWin() {
    }

    [Test]
    public void TestLose() {
    }

    [Test]
    public void TestDraw() {
    }
}
