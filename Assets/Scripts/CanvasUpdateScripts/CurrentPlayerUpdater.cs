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
    }
    private void OnDestroy()
    {
        GameHandler.onNextTurn -= SetTextboxValue;
        GameHandler.onEndTurn -= SetTextboxValue;
    }

    private void SetTextboxValue(uint turnNo)
    {
        textBox.text = (GameHandler.Instance.CurrentPlayer + 1).ToString();
        textBox.color = DataLoader.Instance.PlayerColors[(int)GameHandler.Instance.CurrentPlayer];
    }
}
