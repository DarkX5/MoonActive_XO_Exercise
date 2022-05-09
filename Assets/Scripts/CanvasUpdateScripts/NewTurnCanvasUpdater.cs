using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XO.Core;

public class NewTurnCanvasUpdater : MonoBehaviour
{
    [Header("4 Debug")]
    [SerializeField] private Canvas newTurnCanvas = null;

    // Start is called before the first frame update
    void Start()
    {
        newTurnCanvas = GetComponent<Canvas>();
        if (GameHandler.Instance.FastTurns)
        {
            DisableCanvas();
        }
        else
        {
            EnableCanvas();
            GameHandler.onEndTurn += EnableCanvas;
            GameHandler.onNextTurn += DisableCanvas;
        }
    }

    private void OnDestroy()
    {
        GameHandler.onEndTurn -= EnableCanvas;
        GameHandler.onNextTurn -= DisableCanvas;
    }

    private void EnableCanvas(uint turnNo = 0)
    {
        newTurnCanvas.enabled = true;
    }
    private void DisableCanvas(uint turnNo = 0)
    {
        newTurnCanvas.enabled = false;
    }
}
