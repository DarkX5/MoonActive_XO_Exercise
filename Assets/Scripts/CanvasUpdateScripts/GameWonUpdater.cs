using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using XO.Core;

public class GameWonUpdater : MonoBehaviour
{
    /*v TODO - remove [SerializeField] - 4 debug v*/
    // [SerializeField] private GameObject gameWonContainer;
    [SerializeField] private Canvas gameWonCanvas;
    [SerializeField] private GameObject winImage;
    [SerializeField] private Text winningPlayerIDText;
    /*^ TODO - remove [SerializeField] - 4 debug ^*/

    // Start is called before the first frame update
    void Start()
    {
        // gameWonContainer = transform.GetChild(0).gameObject;
        // gameWonContainer.SetActive(false);
        gameWonCanvas = GetComponent<Canvas>();
        gameWonCanvas.enabled = false;

        GameHandler.onGameEnd += ActivateGameWon;
        GameHandler.onGameDraw += ActivateGameDraw;
    }

    private void OnDestroy() {
        GameHandler.onGameEnd -= ActivateGameWon;
        GameHandler.onGameDraw -= ActivateGameDraw;
    }
    private void ActivateGameWon(uint winnerID) {
        winningPlayerIDText.text = (winnerID + 1).ToString();
        winningPlayerIDText.color = DataLoader.Instance.PlayerColors[(int)(winnerID)];
        // Debug.Log($"> WinnerID: {winnerID} | {winningPlayerIDText.text}");

        winImage.SetActive(true);
        gameWonCanvas.enabled = true;
    }
    private void ActivateGameDraw() {
        winningPlayerIDText.text = "-";
        winningPlayerIDText.color = Color.gray;
        
        winImage.SetActive(false);
        gameWonCanvas.enabled = true;
    }
}
