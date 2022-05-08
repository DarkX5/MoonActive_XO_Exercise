using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrientationHandler : MonoBehaviour
{
    // [SerializeField] private List<Canvas> portraitCanvases = null;
    // [SerializeField] private List<Canvas> landscapeCanvases = null;
    private bool portraitOrientation = true;
    #region  Event Actions
    public static event Action<bool> onOrientationChange = null;
    #endregion

    private void Start()
    {
        if (Screen.width < Screen.height)
            portraitOrientation = true;
        else
            portraitOrientation = false;
    }

    // Update is called once per frame
    void Update()
    {
        // TODO - check screen size and decide which canvases to show
        if (portraitOrientation == true &&
            Screen.width > Screen.height)
        {
            portraitOrientation = false;
            onOrientationChange?.Invoke(portraitOrientation);
            Debug.Log($"onOrientationChange?.Invoke({portraitOrientation});");
        }
        else if (portraitOrientation == false &&
                 Screen.width < Screen.height)
        {
            portraitOrientation = true;
            onOrientationChange?.Invoke(portraitOrientation);
            Debug.Log($"onOrientationChange?.Invoke({portraitOrientation});");
        }
    }
}
