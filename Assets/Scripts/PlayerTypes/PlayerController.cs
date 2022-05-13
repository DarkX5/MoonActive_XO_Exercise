// using UnityEngine;
using System;

/*v TODO - maybe move to enum file / class ?!? v*/
[Serializable]
public enum PlayerTypes {
    Player, AI
}
public class PlayerController // : MonoBehaviour
{ // Monobehaviour has bigger overhead -> downside -> needs M.E.C. package to have acess to coroutines
    protected PlayerTypes playerType;
    public PlayerController() {
        Init();
    }
    protected virtual void Init() {
        playerType = PlayerTypes.Player;
    }

    public virtual void Move() {
        // SP Main Player - Moves are called by click/touch
    }
    public PlayerTypes GetPlayerType() {
        return playerType;
    }
}
