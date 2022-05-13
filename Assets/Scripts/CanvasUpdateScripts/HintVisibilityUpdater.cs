using System.Collections.Generic;
using UnityEngine;
using XO.Core;

public class HintVisibilityUpdater : MonoBehaviour
{
    [SerializeField] private List<GameObject> toggledButtonsList;

    // Start is called before the first frame update
    void Start()
    {
        ToggleHintButtons();
        GameHandler.onGameStart += ToggleHintButtons;
    }
    private void OnDestroy() {
        GameHandler.onGameStart -= ToggleHintButtons;
    }

    private void ToggleHintButtons()
    {
        if (DataLoader.Instance.HintsActive == false)
        {
            foreach(var go in toggledButtonsList) {
                go.SetActive(false);
            }
        } else {
            foreach (var go in toggledButtonsList)
            {
                go.SetActive(true);
            }
        }
    }
}
