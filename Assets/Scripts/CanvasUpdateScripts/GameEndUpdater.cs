using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using XO.Core;

public class GameEndUpdater : MonoBehaviour
{
    [SerializeField] private Canvas gameWonCanvas;
    [SerializeField] private GameObject winImage;
    [SerializeField] private Text winningPlayerIDText;

    [Tooltip("Not needed - Setup for extra performance & to no longer be dependand on positioning")]
    [SerializeField] private GameObject drawImage;

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
        drawImage?.SetActive(false);
        gameWonCanvas.enabled = true;
    }
    private void ActivateGameDraw()
    {
        winningPlayerIDText.text = "-";
        winningPlayerIDText.color = Color.gray;

        winImage.SetActive(false);
        drawImage?.SetActive(true);
        gameWonCanvas.enabled = true;
    }
}
