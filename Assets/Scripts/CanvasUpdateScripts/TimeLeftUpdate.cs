using UnityEngine;
using UnityEngine.UI;

public class TimeLeftUpdate : MonoBehaviour
{
    [Header("Auto - set")]
    [SerializeField] private Text textBox;

    void Start()
    {
        if (textBox == null)
            textBox = transform.GetComponent<Text>();

        PlayerTurnTimer.onTimerChange += SetTextboxValue;
    }
    private void OnDestroy()
    {
        PlayerTurnTimer.onTimerChange -= SetTextboxValue;
    }

    private void SetTextboxValue(float newValue, Color timerColor) {
        textBox.text = newValue.ToString();
        textBox.color = timerColor;
    }
}
