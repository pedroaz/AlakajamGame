using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class GameEnder : MonoBehaviour
{
    public EndGamePanel endGamePanel;
    public GameScore gameScore;
    public bool gameHasEnded;

    private void Awake()
    {
        endGamePanel = FindObjectOfType<EndGamePanel>();
        gameScore = FindObjectOfType<GameScore>();
        gameHasEnded = false;
    }

    private void OnEnable()
    {
        GlobalEvents.OnCastleDamage += CheckIfGameEnd;
    }

    private void OnDestroy()
    {
        GlobalEvents.OnCastleDamage -= CheckIfGameEnd;
    }

    public void CheckIfGameEnd(object sender, System.EventArgs e)
    {

        if (gameHasEnded) {
            return;
        }

        CastleDamageArgs arg = (CastleDamageArgs) e;

        if(arg.currentCastleHealth <=0) {

            StartCoroutine(EndGame());
        }
    }

    public void PlayAgain()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MenuScene");
    }

    public IEnumerator EndGame()
    {
        gameHasEnded = true;

        ScoreList scoreList = null;

        endGamePanel.ShowLoading();

        string playerName = PlayerPrefs.GetString("PLAYER_NAME");
        string score = gameScore.currentGameScore.ToString();

        string get_url = "https://ship-games-api.herokuapp.com/GoogleSheetsAPI/get_all/1W7kbA0aYOBbAtylJ0NAECanZX8zPjCR8Gh0Gt5FHXqs/GameJam";
        string post_url = "https://ship-games-api.herokuapp.com/GoogleSheetsAPI/add_score/" + playerName + " / " + score;

       

        using (UnityWebRequest webRequest = UnityWebRequest.Get(post_url)) {
            yield return webRequest.SendWebRequest();
        }

        using (UnityWebRequest webRequest = UnityWebRequest.Get(get_url)) {
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();

            string[] pages = get_url.Split('/');
            int page = pages.Length - 1;

            if (webRequest.isNetworkError) {
                Debug.Log(pages[page] + ": Error: " + webRequest.error);
            }
            else {
                Debug.Log(pages[page] + ":\nReceived: " + webRequest.downloadHandler.text);
                scoreList = JsonUtility.FromJson<ScoreList>("{\"list\":" + webRequest.downloadHandler.text + "}" );
            }
        }

        endGamePanel.ShowComplete(scoreList);

    }

}

[System.Serializable]
public class ScoreList
{
    public ScoreData[] list = new ScoreData[1];
}

[System.Serializable]
public class ScoreData
{
    public string row_index;
    public string player_name;
    public string player_score;
}

