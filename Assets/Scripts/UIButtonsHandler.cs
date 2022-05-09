using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace XO.UI
{
    public class UIButtonsHandler : MonoBehaviour
    {
        public static event Action onUIButtonClick = null;

        public void UIButtonClick()
        {
            onUIButtonClick?.Invoke();
        }
    }
}