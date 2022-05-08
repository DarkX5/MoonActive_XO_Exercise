using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSessionData : MonoBehaviour
{
    public static GameSessionData Instance { get; private set; }

    // private uint currentPlayer = 0;


    // private void Awake() {
    //     if (Instance == null)
    //         Instance = this;
    //     else
    //         Destroy(gameObject);
    // }

    // // Start is called before the first frame update
    // void Start()
    // {
    //     for (int i = 0; i < playerNo; i += 1) {
    //         playerIDs.Add(i);
    //         playerColors.Add(new Color( UnityEngine.Random.Range(0f, 1f), 
    //                                     UnityEngine.Random.Range(0, 255), 
    //                                     UnityEngine.Random.Range(0f, 1f)));
    //     }

    //     GameHandler.
    // }

    // private void NewTurnAction(uint turnNo) {
    //     currentPlayer += 1;
    //     if (currentPlayer >= turnNo) {
    //         currentPlayer = 1;
    //     }
    // }
}
