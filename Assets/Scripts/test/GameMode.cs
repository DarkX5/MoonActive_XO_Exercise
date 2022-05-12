using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMode
{
    /*v TODO Decision - only used internally - maybe move to an enum file containing all/some enums?!? */
    public enum GameModeTypes
    {
        PvP, PvE
    }
    /*^ TODO Decision - only used internally - maybe move to an enum file containing all/some enums?!?  ^*/

    protected GameModeTypes gameType;

    protected virtual void Init() {
        gameType = GameModeTypes.PvE;
    }

    protected virtual void Move() {}
}
