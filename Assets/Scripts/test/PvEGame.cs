using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PvEGame : GameMode
{
    protected override void Init()
    {
        gameType = GameModeTypes.PvE;
    }
}