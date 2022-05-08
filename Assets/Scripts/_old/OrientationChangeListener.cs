using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrientationChangeListener : MonoBehaviour
{
    // private void OnEnable() {
    //     // subscribe to text events
    //     OrientationHandler.onOrientationChange += SetVisibility;
    //     OrientationHandler.onOrientationChange += SetVisibility;
    // }

    // private void OnDisable() {
    //     // unsubscribe to text events
    //     OrientationHandler.onOrientationChange -= SetVisibility;
    //     OrientationHandler.onOrientationChange -= SetVisibility;
    // }

    private void Start() {
        // subscribe to text events
        OrientationHandler.onOrientationChange += SetVisibility;
        OrientationHandler.onOrientationChange += SetVisibility;
    }
    private void OnDestroy() {
        // unsubscribe to text events
        OrientationHandler.onOrientationChange -= SetVisibility;
        OrientationHandler.onOrientationChange -= SetVisibility;
    }

    private void SetVisibility(bool newVisibility) {
        gameObject.SetActive(newVisibility);
    }
}
