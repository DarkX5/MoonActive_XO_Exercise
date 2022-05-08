using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollViewSetup : MonoBehaviour
{
    [SerializeField] private GridLayoutGroup gridLayout;
    [SerializeField] private Vector2 spacingSize = new Vector2(5f, 5f);

    // Start is called before the first frame update
    void Start()
    {
        spacingSize *= 4;
        SetupScrollViewSizes();
    }

    // Update is called once per frame
    void Update()
    {
        SetupScrollViewSizes();
    }

    private void SetupScrollViewSizes()
    {
        RectTransform rect = GetComponent<RectTransform>();
        if (Screen.width > Screen.height)
        {
            rect.sizeDelta = new Vector2(   (Screen.width / 2) + spacingSize.x,
                                            Screen.height + spacingSize.y);
            // GetComponent<RectTransform>().offsetMin = new Vector2(-(Screen.width / 2) - spacingSize.x, -Screen.height - spacingSize.y); // = -GetComponent<RectTransform>().sizeDelta.x;
        }
        else
        {
            rect.sizeDelta = new Vector2(   Screen.width + spacingSize.x,
                                            (Screen.height / 2) + spacingSize.y);
        }

        Debug.Log($"X: {rect.anchoredPosition.x} Y: {rect.anchoredPosition.y}");
        rect.anchoredPosition = new Vector2( rect.sizeDelta.x / 2, - rect.sizeDelta.y / 2);

        Vector2 buttonSize = new Vector2((rect.sizeDelta.x - spacingSize.x)/ 3, (rect.sizeDelta.y - spacingSize.y)/ 3);

        // setup grid
        gridLayout = GetComponentInChildren<GridLayoutGroup>();
        gridLayout.cellSize = buttonSize;
        gridLayout.spacing = spacingSize / 4;

        foreach (var canvas in GetComponentsInChildren<Canvas>())
        {
            // set button size relative to screen size
            canvas.transform.GetComponentInChildren<RectTransform>().sizeDelta = buttonSize;
        };
    }
}
