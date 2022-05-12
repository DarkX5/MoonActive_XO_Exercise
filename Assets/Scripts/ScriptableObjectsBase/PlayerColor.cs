using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerColor", menuName = "ScriptableObjects/PlayerColor")]
public class PlayerColor : ScriptableObject
{
    [SerializeField] private Color playerColor = Color.white;

    public Color PlayerColorValue { get { return playerColor; } }
}
