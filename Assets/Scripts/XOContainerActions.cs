using System;
using UnityEngine;
using UnityEngine.UI;
using XO.Core;

public class XOContainerActions : MonoBehaviour
{
    /* TODO - remove [SerializeField] - used 4 Debugging */
    [SerializeField] private Image buttonImage;
    [SerializeField] private Button button;
    [SerializeField] private uint playerID = 0;
    [SerializeField] private uint buttonID;

    public bool? IsButtonEnabled { get { return button?.enabled; } }

    public uint PlayerID { get { return playerID; } }
    public uint ButtonID { get { return buttonID; } }

    // Start is called before the first frame update
    void Start()
    {
        // cache button
        button = GetComponentInChildren<Button>();
        // cache image
        buttonImage = button.image;

        // start button in "up" state (uses image to set where it was used or not)
        button.GetComponent<Image>().sprite = null;
    }

    // called from UI by player
    public void ActivateButton() {
        if (!GameHandler.Instance.IsPlayerActive) { return; }
        if (button == null) {
            // cache button
            button = GetComponentInChildren<Button>();
        }

        // turn off button
        button.enabled = false;
        GameHandler.Instance.AddMove(buttonID);

        if (buttonImage == null) {
            // cache image
            buttonImage = button.image;
        }

        // set button image in "down" state
        playerID = GameHandler.Instance.CurrentPlayer + 1;
        buttonImage.sprite = DataLoader.Instance.GetCurrentPlayerIcon((int)GameHandler.Instance.CurrentPlayer);
        buttonImage.color = Color.white;

        if (GameHandler.Instance.FastTurns) {
            GameHandler.Instance.EndTurn();
            GameHandler.Instance.NextTurn();
        } else {
            // call end Turn
            GameHandler.Instance.EndTurn();
        }
    }

    // called from UI
    public void AIActivateButton()
    {
        if (button == null)
        {
            // cache button
            button = GetComponentInChildren<Button>();
        }

        // turn off button
        button.enabled = false;
        GameHandler.Instance.AddMove(buttonID);

        if (buttonImage == null)
        {
            // cache image
            buttonImage = button.image;
        }

        // set button image in "down" state
        playerID = GameHandler.Instance.CurrentPlayer + 1;
        buttonImage.sprite = DataLoader.Instance.GetCurrentPlayerIcon((int)GameHandler.Instance.CurrentPlayer);
        buttonImage.color = Color.white;

        GameHandler.Instance.EndTurn();
        GameHandler.Instance.NextTurn();
    }

    // called from UI
    public void ActivateHintButton()
    {
        // set button image in "hint" state
        buttonImage.sprite = DataLoader.Instance.GetCurrentPlayerIcon((int)GameHandler.Instance.CurrentPlayer);
        buttonImage.color = Color.gray;
    }
    public void DeactivateHintButton() {
        if (playerID > 0) { return; }
        buttonImage.sprite = null;
        buttonImage.color = new Color(1f, 1f, 1f, 0f);
    }
    private void AddToGlobalMoveList()
    {
        GameHandler.Instance.AddMove(buttonID);
    }

    public void EnableButton() {
        playerID = 0;
        buttonImage.color = new Color(1f, 1f, 1f, 0f);
        buttonImage.sprite = null;
        button.enabled = true;
    }

    public void SetMoveID(uint newMoveID) {
        buttonID = newMoveID;
    }
}
