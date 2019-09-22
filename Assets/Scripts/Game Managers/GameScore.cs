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
        ResetGameScore();
    }

    private void OnDestroy()
    {
        GlobalEvents.OnAddGameScore -= UpdateGameScore;
    }

    public void UpdateGameScore(object sender, System.EventArgs e)
    {
        GameScoreArgs arg = (GameScoreArgs)e;
        currentGameScore += arg.scoreToAdd;
        gameScoreUI.UpdateGameScore(currentGameScore);
    }

    public void ResetGameScore()
    {
        currentGameScore = 0;
        GlobalEvents.AddGameScore(this, new GameScoreArgs(0));
    }
}
