using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerColor", menuName = "ScriptableObjects/PlayerColor")]
public class PlayerColor : ScriptableObject
{
    public Color playerColor = Color.white;
}
