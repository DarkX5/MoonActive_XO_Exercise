using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class PlayTestScript
{
    /*v TODO - implement v*/
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

    [UnityTest]
    public IEnumerator TestGetHintPosition()
    {
        // // We have references to all episodes and their levels in Scriptable Object
        // var episodesContainer = Resources.Load<XOActionsHandler>("EpisodesContainer");
        // XO.Core.XOActionsHandler.Instance asteroid = game.GetSpawner().SpawnAsteroid();
        // float initialYPos = asteroid.transform.position.y;
        yield return new WaitForSeconds(0.1f);

        // Assert.Less(asteroid.transform.position.y, initialYPos);
    }
}