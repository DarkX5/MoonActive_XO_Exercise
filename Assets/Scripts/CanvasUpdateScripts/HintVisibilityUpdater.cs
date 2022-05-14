using UnityEngine;
using XO.Core;

public class HintVisibilityUpdater : MonoBehaviour
{
    [SerializeField] private bool destroyInsteadOfHide = false;
    private void Start()
    {
        if (DataLoader.Instance.HintsActive == false)
        {
            if (destroyInsteadOfHide)
            {
                Destroy(gameObject);
            }
            else
            {
                gameObject.SetActive(false);
            }
        }
    }
}
