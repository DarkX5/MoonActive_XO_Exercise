using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using XO.Core;

public class GameWonUpdater : MonoBehaviour
{
    private Canvas gameWonCanvas;
    private GameObject winImage;
    private Text winningPlayerIDText;

    // Start is called before the first frame update
    void Start()
    {
        gameWonCanvas = GetComponent<Canvas>();
        gameWonCanvas.enabled = false;

        GameHandler.onGameEnd += ActivateGameWon;
        GameHandler.onGameDraw += ActivateGameDraw;
    }

    private void OnDestroy()
    {
        GameHandler.onGameEnd -= ActivateGameWon;
        GameHandler.onGameDraw -= ActivateGameDraw;
    }
    private void ActivateGameWon(uint winnerID)
    {
        winningPlayerIDText.text = (winnerID + 1).ToString();
        winningPlayerIDText.color = DataLoader.Instance.PlayerColors[(int)(winnerID)];

        winImage.SetActive(true);
        gameWonCanvas.enabled = true;
    }
    private void ActivateGameDraw()
    {
        winningPlayerIDText.text = "-";
        winningPlayerIDText.color = Color.gray;

        winImage.SetActive(false);
        gameWonCanvas.enabled = true;
    }
}
