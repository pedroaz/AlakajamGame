using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScore : MonoBehaviour
{
    public int currentGameScore;
    private GameScoreUI gameScoreUI;

    private void Awake()
    {
        GlobalEvents.OnAddGameScore += UpdateGameScore;
        gameScoreUI = FindObjectOfType<GameScoreUI>();
    }

    private void OnDestroy()
    {
        GlobalEvents.OnCastleDamage -= UpdateGameScore;
    }

    public void UpdateGameScore(object sender, System.EventArgs e)
    {
        GameScoreArgs arg = (GameScoreArgs)e;
        currentGameScore += arg.scoreToAdd;
        gameScoreUI.UpdateGameScore(currentGameScore);
    }

    public void ResetGameScore()
    {
        GlobalEvents.AddGameScore(this, new GameScoreArgs(0));
    }
}
