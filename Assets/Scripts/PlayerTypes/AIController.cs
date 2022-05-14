using System.Collections.Generic;
using XO.Core;

public class AIController : PlayerController
{
    private List<int> validMoves = null;
    // set move delay for AI
    private float moveDelay = 0.25f;
    protected override void Init()
    {
        playerType = PlayerTypes.AI;
    }
    public override void Move()
    {
        validMoves = XOActionsHandler.Instance.GetValidMoves();
        if (validMoves?.Count > 0)
        {
            // activate random valid move
            XOActionsHandler.Instance.MoveWithDelay(validMoves[UnityEngine.Random.Range((int)0, (int)validMoves.Count)],
                                                    moveDelay);
        }
    }
}
