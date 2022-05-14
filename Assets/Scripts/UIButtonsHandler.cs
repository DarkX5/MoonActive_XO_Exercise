using System;
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