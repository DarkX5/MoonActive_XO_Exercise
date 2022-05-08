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
    private void OnDestroy() {
        XOActionsHandler.onWinningStreak -= DrawVictoryStrikeout;
    }
    private void DrawVictoryStrikeout(WinType winType, int idx)
    {
        if (winType == WinType.Diagonal1) {
            lineImageRect.sizeDelta = new Vector2(lineImageStartingSize.x * 1.25f, lineImageStartingSize.y);
            lineImageRect.localRotation = Quaternion.AngleAxis(-45, Vector3.forward); // new Quaternion(lineImageRect.rotation.x, lineImageRect.rotation.y, -45f, lineImageRect.rotation.w);
            return;
        } else if (winType == WinType.Diagonal2) {
            lineImageRect.sizeDelta = new Vector2(lineImageStartingSize.x * 1.25f, lineImageStartingSize.y);
            lineImageRect.localRotation = Quaternion.AngleAxis(45, Vector3.forward); // new Quaternion(lineImageRect.rotation.x, lineImageRect.rotation.y, 45f, lineImageRect.rotation.w);
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
        
        if (winType == WinType.Vertical) {
            lineImageRect.localRotation = Quaternion.AngleAxis(90, Vector3.forward); // new Quaternion(lineImageRect.rotation.x, lineImageRect.rotation.y, 90f, lineImageRect.rotation.w);
        }

        // show strikeout image
        lineImage.gameObject.SetActive(true);
    }

    // private void DrawVictoryStrikeout(WinType winType, int idx) {
    //     switch(winType) {
    //         case WinType.Horizontal:
    //             horizontalLineImages[idx].SetActive(true);
    //             break;
    //         case WinType.Vertical:
    //             verticalLineImages[idx].SetActive(true);
    //             break;
    //         case WinType.Diagonal1:
    //             diagonal1LineImages.SetActive(true);
    //             break;
    //         case WinType.Diagonal2:
    //             diagonal2LineImages.SetActive(true);
    //             break;
    //     }
    // }
    // private void DrawVictoryStrikeout(WinType winType, int idx) { 
    //     Debug.Log("Strikeout");
    //     float newPos;
    //     GameObject tmpGO = new GameObject();
    //     tmpGO.AddComponent<Image>().sprite = DataLoader.Instance.HorizontalStrikeoutSprite;

    //     switch (winType)
    //     {
    //         case WinType.Horizontal:
    //             if (idx == 0) {
    //                 newPos = distancePx;
    //             } else if (idx == 1) {
    //                 newPos = 0f;
    //             } else {
    //                 newPos = -distancePx;
    //             }
    //             tmpGO.GetComponent<RectTransform>().position = new Vector3(0f, newPos, 0f);
    //             tmpGO.AddComponent<RectTransform>().sizeDelta = new Vector2(lineWidthPx, lineHeightPx);
    //             break;
    //         case WinType.Vertical:
    //             if (idx == 0) {
    //                 newPos = distancePx;
    //             } else if (idx == 1) {
    //                 newPos = 0f;
    //             } else {
    //                 newPos = -distancePx;
    //             }
    //             tmpGO.GetComponent<RectTransform>().position = new Vector3(0f, newPos, 0f);
    //             tmpGO.AddComponent<RectTransform>().sizeDelta = new Vector2(lineWidthPx, lineHeightPx);
    //             break;
    //         case WinType.Diagonal1:
    //             tmpGO.GetComponent<RectTransform>().sizeDelta = new Vector2(lineWidthPx * 0.25f, lineHeightPx);
    //             break;
    //         case WinType.Diagonal2:
    //             tmpGO.GetComponent<RectTransform>().sizeDelta = new Vector2(lineWidthPx * 0.25f, lineHeightPx);
    //             break;
    //     }
    // }

    // private void ShowVictoryLine(WinType winType, int hIdx, int vIdx) {
    //     switch(winType) {
    //         case WinType.Horizontal:
                
    //             break;
    //         case WinType.Vertical:
    //             break;
    //         case WinType.Diagonal1:
    //             break;
    //         case WinType.Diagonal2:
    //             break;
    //     }
    // }
}
