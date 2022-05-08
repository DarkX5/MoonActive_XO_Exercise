using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameInfoSetup : MonoBehaviour
{
    [SerializeField] private RectTransform displayBox = null;

    // Start is called before the first frame update
    void Start()
    {
        if (displayBox == null)
            displayBox = GetComponentInChildren<RectTransform>();
        
        displayBox.sizeDelta = new Vector3();
    }

    // // Update is called once per frame
    // void Update()
    // {
        
    // }
}
