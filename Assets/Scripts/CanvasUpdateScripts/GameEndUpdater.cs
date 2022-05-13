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

    private bool isCanvasActive = false;

    public bool IsCanvasActive { get { return isCanvasActive; } }

    // Start is called before the first frame update
    void Start()
    {
        isCanvasActive = false;
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
        isCanvasActive = true;
        winningPlayerIDText.text = (winnerID + 1).ToString();
        winningPlayerIDText.color = DataLoader.Instance.PlayerColors[(int)(winnerID)];

        winImage.SetActive(true);
        drawImage?.SetActive(false);
        gameWonCanvas.enabled = true;
    }
    private void ActivateGameDraw()
    {
        isCanvasActive = true;
        winningPlayerIDText.text = "-";
        winningPlayerIDText.color = Color.gray;

        winImage.SetActive(false);
        drawImage?.SetActive(true);
        gameWonCanvas.enabled = true;
    }
}
