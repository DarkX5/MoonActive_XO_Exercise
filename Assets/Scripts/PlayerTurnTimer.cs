using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XO.Core;

public class PlayerTurnTimer : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField][Range(0.1f, 1f)] private float timeUpdateFrequencyS = 1f;
    [SerializeField] private float timeLimitForTurns = 5f;
    [SerializeField] private Color timeLeftFullTextColor = Color.green;
    [SerializeField] private Color timeLeft2ThirdsTextColor = Color.yellow;
    [SerializeField] private Color timeLeft1ThirdTextColor = Color.red;

    public static event Action<float, Color> onTimerChange = null;

    IEnumerator tempCO = null;
    private float timeLeft = 5f;
    private bool gameEnded = false;
    private Color currentTextColor;

    private void Start()
    {
        GameHandler.onNextTurn += ResetTimer;
        GameHandler.onUndoTurn += ResetTimer;
        GameHandler.onEndTurn += StopTimer;
        GameHandler.onGameDraw += GameEnded;
        GameHandler.onGameEnd += GameEnded;
    }
    private void OnDestroy()
    {
        GameHandler.onNextTurn -= ResetTimer;
        GameHandler.onUndoTurn -= ResetTimer;
        GameHandler.onEndTurn -= StopTimer;
        GameHandler.onGameDraw -= GameEnded;
        GameHandler.onGameEnd -= GameEnded;
    }

    private void StopTimer(uint turnNo)
    {
        if (tempCO != null)
        {
            StopCoroutine(tempCO);
        }

        timeLeft = timeLimitForTurns;
    }
    private void GameEnded(uint turnNo)
    {
        gameEnded = true;
        StopTimer(0);
    }
    private void GameEnded()
    {
        gameEnded = true;
        StopTimer(0);
    }
    private void ResetTimer(uint turnNo)
    {
        // stop coroutine if running
        if (tempCO != null)
        {
            StopCoroutine(tempCO);
        }

        tempCO = RunTimerCo();
        StartCoroutine(tempCO);

        timeLeft = timeLimitForTurns;
    }

    private IEnumerator RunTimerCo()
    {
        // ignore any currently running coroutines after game end
        if (gameEnded == false)
        {
            // send time left to subscribers (if any - UI)
            onTimerChange?.Invoke(timeLeft, GetCurrentColor());
        }

        yield return new WaitForSeconds(timeUpdateFrequencyS);

        // ignore any currently running coroutines after game end
        if (gameEnded == false)
        {
            // update time left
            timeLeft -= timeUpdateFrequencyS;

            // check for coroutine start/stop
            if (timeLeft > 0f)
            {
                tempCO = RunTimerCo();
                StartCoroutine(tempCO);
            }
            else
            {
                // send time left to subscribers (if any - UI)
                onTimerChange?.Invoke(0f, GetCurrentColor());

                GameHandler.Instance.TimeEndGame();
            }
        }
    }

    private Color GetCurrentColor()
    {
        if (timeLeft <= (timeLimitForTurns / 3) + 1)
        {
            return timeLeft1ThirdTextColor;
        }
        else if (timeLeft <= (timeLimitForTurns / 2) + 1)
        {
            return timeLeft2ThirdsTextColor;
        }

        return timeLeftFullTextColor;
    }
}
