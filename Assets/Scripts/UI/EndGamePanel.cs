using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;

public class EndGamePanel : MonoBehaviour
{
    public GameObject container;
    public GameObject loadingScreen;
    public List<TextMeshProUGUI> listOfText;

    public TextMeshProUGUI currentScoreText;
    public TextMeshProUGUI maxScoreText;


    public void ShowLoading(int currentScore)
    {

        maxScoreText.text = "High Score: " + PlayerPrefs.GetInt("HIGH_SCORE").ToString();
        currentScoreText.text = "Score: " + currentScore;

        container.SetActive(true);
        loadingScreen.SetActive(true);
    }

    public void ShowComplete(ScoreList scoreList)
    {
        

        scoreList.list = scoreList.list.OrderByDescending(x => int.Parse(x.player_score)).ToArray();
        for (int i = 0; i < 5; i++) {

            listOfText[i].text = scoreList.list[i].player_name + " - " + scoreList.list[i].player_score;
        }
        loadingScreen.SetActive(false);
    }

    public void Hide()
    {
        container.SetActive(false);
        loadingScreen.SetActive(false);
    }

}
