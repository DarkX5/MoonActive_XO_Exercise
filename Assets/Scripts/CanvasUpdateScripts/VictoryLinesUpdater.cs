using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using XO.Core;

public class VictoryLinesUpdater : MonoBehaviour
{
    private Image lineImage = null;
    private RectTransform lineImageRect;
    private Vector2 lineImageStartingSize;

    // Start is called before the first frame update
    void Start()
    {
        lineImage = transform.GetChild(0).GetComponentInChildren<Image>();
        if (lineImage != null)
        {
            lineImageRect = lineImage.GetComponent<RectTransform>();
            lineImageStartingSize = lineImageRect.sizeDelta;
        }

        lineImage.gameObject.SetActive(false);

        XOActionsHandler.onWinningStreak += DrawVictoryStrikeout;
    }
    private void OnDestroy()
    {
        XOActionsHandler.onWinningStreak -= DrawVictoryStrikeout;
    }
    private void DrawVictoryStrikeout(WinType winType, int idx)
    {
        if (winType == WinType.Diagonal1)
        {
            lineImageRect.sizeDelta = new Vector2(lineImageStartingSize.x * 1.25f, lineImageStartingSize.y);
            lineImageRect.localRotation = Quaternion.AngleAxis(-45, Vector3.forward);
            return;
        }
        else if (winType == WinType.Diagonal2)
        {
            lineImageRect.sizeDelta = new Vector2(lineImageStartingSize.x * 1.25f, lineImageStartingSize.y);
            lineImageRect.localRotation = Quaternion.AngleAxis(45, Vector3.forward);
            return;
        }

        // reset size back to normal if it needs to in the future
        if (lineImageRect.sizeDelta != lineImageStartingSize)
        {
            lineImageRect.sizeDelta = lineImageStartingSize;
        }

        Vector3 newPosition;
        switch (idx)
        {
            case 0:
                newPosition = new Vector3(-330f, 0, 0);
                break;
            case 1:
                newPosition = Vector3.zero;
                break;
            case 2:
                newPosition = new Vector3(330f, 0, 0);
                break;
            default:
                newPosition = Vector3.zero;
                break;
        }
        lineImageRect.localPosition = newPosition;

        if (winType == WinType.Vertical)
        {
            lineImageRect.localRotation = Quaternion.AngleAxis(90, Vector3.forward);
        }

        // show strikeout image
        lineImage.gameObject.SetActive(true);
    }
}
