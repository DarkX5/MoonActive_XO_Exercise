using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class EditorTestScript
{
    /*v TODO - implement v*/
    // A Test behaves as an ordinary method
    [Test]
    public void EditorTestScriptSimplePasses()
    {
        // Use the Assert class to test conditions
        // Assert.AreEqual(1, 2);
    }

    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnityTest]
    public IEnumerator EditorTestScriptWithEnumeratorPasses()
    {
        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        yield return null;
    }
}
