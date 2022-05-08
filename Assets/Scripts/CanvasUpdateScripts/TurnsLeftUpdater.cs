using UnityEngine;
using UnityEngine.UI;
using XO.Core;

public class TurnsLeftUpdater : MonoBehaviour
{
    [SerializeField] private Text textBox;

    void Start()
    {
        if (textBox == null)
            textBox = transform.GetComponent<Text>();

        GameHandler.onEndTurn += SetTextboxValue;
        GameHandler.onNextTurn += SetTextboxValue;
    }
    private void OnDestroy()
    {
        GameHandler.onEndTurn -= SetTextboxValue;
        GameHandler.onNextTurn -= SetTextboxValue;
    }

    private void SetTextboxValue(uint newValue)
    {
        textBox.text = newValue.ToString();
    }
}
