using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonSizeSetter : MonoBehaviour
{
    [SerializeField] private RectTransform rect = null;
    [SerializeField] private Button activationButton = null;

    // Start is called before the first frame update
    void Start()
    {
        rect = GetComponent<RectTransform>();
        rect.sizeDelta = ButtonSizeData.Instance.ButtonSizes; // new Vector2(Screen.width * widthPercent, Screen.height * heightPercent);

        activationButton = GetComponentInChildren<Button>();
    }

    // public void SetSize(Vector2 newSize) {
    //     rect.sizeDelta = newSize;
    // }

    // // Update is called once per frame
    // void Update()
    // {
        
    // }
}
