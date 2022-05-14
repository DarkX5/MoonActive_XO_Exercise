using NUnit.Framework;
using UnityEditor;

public class EditorTestScript
{
    [Test]
    public void TestScenesAddedToBuild()
    {
        Assert.AreNotEqual(GetSceneFilePath("_MainMenu"), "");
        Assert.AreNotEqual(GetSceneFilePath("_GameScene"), "");
    }

    #region  Helpers
    // Helper to find a scene path
    static string GetSceneFilePath(string sceneName)
    {
        foreach (var scene in EditorBuildSettings.scenes)
        {
            if (scene.path.Contains(sceneName))
            {
                return scene.path;
            }
        }

        // We do not need to do anything fancy here. If the scene has not been found,
        // it will fail with the empty string and we know something is wrong.
        return "";
    }
    #endregion
}
