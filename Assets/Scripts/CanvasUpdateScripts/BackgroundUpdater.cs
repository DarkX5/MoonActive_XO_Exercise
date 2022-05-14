using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using XO.Core;

public class BackgroundUpdater : MonoBehaviour
{
    [SerializeField][Range(0.1f, 5f)] private float startUpdateFrequency = 0.25f;
    // Start is called before the first frame update
    void Start()
    {
            Debug.Log($"BackgroundUpdater: {transform.GetComponentInChildren<Image>().sprite}");
        StartCoroutine(LoadBackgroundAfterDataLoadCO());
    }
    IEnumerator LoadBackgroundAfterDataLoadCO()
    {
        yield return new WaitForSeconds(startUpdateFrequency);
        if (DataLoader.Instance.IsDataLoaded)
        {
            Debug.Log($"BackgroundUpdater: {transform.GetComponentInChildren<Image>().sprite}");
            transform.GetComponentInChildren<Image>().sprite = DataLoader.Instance.BGSprite;
        }
        else
        {
            StartCoroutine(LoadBackgroundAfterDataLoadCO());
        }
    }
}
