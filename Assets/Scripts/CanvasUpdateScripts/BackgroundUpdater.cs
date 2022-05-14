using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using XO.Core;

public class BackgroundUpdater : MonoBehaviour
{
    [SerializeField][Range(0.1f, 5f)] private float startUpdateFrequency = 0.25f;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(LoadBackgroundAfterDataLoadCO());
    }
    IEnumerator LoadBackgroundAfterDataLoadCO()
    {
        yield return new WaitForSeconds(startUpdateFrequency);
        if (DataLoader.Instance.IsDataLoaded)
        {
            transform.GetComponentInChildren<Image>().sprite = DataLoader.Instance.BGSprite;
        }
        else
        {
            StartCoroutine(LoadBackgroundAfterDataLoadCO());
        }
    }
}
