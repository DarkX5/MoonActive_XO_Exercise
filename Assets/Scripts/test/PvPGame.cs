using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PvPGame : GameMode
{
    protected override void Init()
    {
        gameType = GameModeTypes.PvP;
    }
}