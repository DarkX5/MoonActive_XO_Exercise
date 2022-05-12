using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using XO.Core;

public class VictoryLinesUpdater : MonoBehaviour
{
    [Tooltip("Auto-Set -> small boost to performance if set.")]
    [SerializeField] private Image lineImage = null;
    private RectTransform lineImageRect;
    private Vector2 lineImageStartingSize;

    // Start is called before the first frame update
    void Start()
    {
        if (lineImage == null) {
            lineImage = transform.GetChild(0).GetComponentInChildren<Image>();
        }

        lineImageRect = lineImage.GetComponent<RectTransform>();
        lineImageStartingSize = lineImageRect.sizeDelta;
        lineImage.gameObject.SetActive(false);

        XOActionsHandler.onWinningStreak += DrawVictoryStrikeout;
    }
    private void OnDestroy()
    {
        XOActionsHandler.onWinningStreak -= DrawVictoryStrikeout;
    }
    private void DrawVictoryStrikeout(WinType winType, int idx)
    {
        Debug.Log($"winType: {winType.ToString()} | idx: {idx}");
        if (winType == WinType.Diagonal1)
        {
            lineImageRect.sizeDelta = new Vector2(lineImageStartingSize.x * 1.25f, lineImageStartingSize.y);
            lineImageRect.localRotation = Quaternion.AngleAxis(-45, Vector3.forward);
            ShowStrikeoutLine();
            return;
        }
        else if (winType == WinType.Diagonal2)
        {
            lineImageRect.sizeDelta = new Vector2(lineImageStartingSize.x * 1.25f, lineImageStartingSize.y);
            lineImageRect.localRotation = Quaternion.AngleAxis(45, Vector3.forward);
            ShowStrikeoutLine();
            return;
        }

        // reset size back to normal if it needed
        if (lineImageRect.sizeDelta != lineImageStartingSize)
        {
            lineImageRect.sizeDelta = lineImageStartingSize;
        }

        Vector3 newPosition = Vector3.zero;
        if (winType == WinType.Vertical)
        {
            switch (idx)
            {
                case 0:
                    newPosition = new Vector3(-330f, 0f, 0f);
                    break;
                case 1:
                    newPosition = Vector3.zero;
                    break;
                case 2:
                    newPosition = new Vector3(330f, 0f, 0f);
                    break;
                default:
                    newPosition = Vector3.zero;
                    break;
            }
            lineImageRect.localRotation = Quaternion.AngleAxis(90f, Vector3.forward);
        } else if (winType == WinType.Horizontal) {
            switch (idx)
            {
                case 0:
                    newPosition = new Vector3(0f, 330f, 0f);
                    break;
                case 1:
                    newPosition = Vector3.zero;
                    break;
                case 2:
                    newPosition = new Vector3(0f, -330f, 0f);
                    break;
                default:
                    newPosition = Vector3.zero;
                    break;
            }
        }

        lineImageRect.localPosition = newPosition;

        ShowStrikeoutLine();
    }

    private void ShowStrikeoutLine()
    {

        // show strikeout image
        lineImage.gameObject.SetActive(true);
    }
}
