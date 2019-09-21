using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameScoreUI : MonoBehaviour
{
    TextMeshProUGUI scoreText;

    private void Awake()
    {
        scoreText = GetComponent<TextMeshProUGUI>();
    }

    public void UpdateGameScore(int score)
    {
        scoreText.text = score.ToString();
    }
}
