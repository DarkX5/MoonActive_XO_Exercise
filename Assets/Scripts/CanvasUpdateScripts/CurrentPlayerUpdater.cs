using UnityEngine;
using UnityEngine.UI;
using XO.Core;

public class CurrentPlayerUpdater : MonoBehaviour
{
    [SerializeField] private Text textBox;
    [SerializeField] private uint lastPlayerPos = 0;

    void Start()
    {
        if (textBox == null)
            textBox = transform.GetComponent<Text>();
        lastPlayerPos = (GameHandler.Instance.PlayerNo - 1);

        GameHandler.onNextTurn += SetTextboxValue;
        GameHandler.onEndTurn += SetTextboxValue;
        // GameHandler.onGameEnd += SetGameEndTextboxValue;
        // GameHandler.onGameDraw += SetGameDrawTextboxValue;
    }
    private void OnDestroy() {
        GameHandler.onNextTurn -= SetTextboxValue;
        GameHandler.onEndTurn -= SetTextboxValue;
        // GameHandler.onGameEnd -= SetGameEndTextboxValue;
        // GameHandler.onGameDraw -= SetGameDrawTextboxValue;
    }

    private void SetTextboxValue(uint turnNo)
    {
        textBox.text = (GameHandler.Instance.CurrentPlayer + 1).ToString();
        textBox.color = DataLoader.Instance.PlayerColors[(int)GameHandler.Instance.CurrentPlayer];
        // uint currentPlayerNo = turnNo % GameHandler.Instance.PlayerNo;
        // // set player number by current turn value (assumes min turn = 1)
        // if(currentPlayerNo == 0) {
        //     textBox.text = GameHandler.Instance.PlayerNo.ToString();
        //     textBox.color = GameHandler.Instance.PlayerColors[(int)lastPlayerPos];
        // } else {
        //     textBox.text = currentPlayerNo.ToString();
        //     textBox.color = GameHandler.Instance.PlayerColors[(int)currentPlayerNo];
        // }
    }
}
