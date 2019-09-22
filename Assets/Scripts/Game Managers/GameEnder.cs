using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class GameEnder : MonoBehaviour
{
    public EndGamePanel endGamePanel;
    public GameScore gameScore;
    public bool gameHasEnded;

    private void Awake()
    {
        GlobalEvents.OnCastleDamage += CheckIfGameEnd;
        endGamePanel = FindObjectOfType<EndGamePanel>();
        gameScore = FindObjectOfType<GameScore>();
        gameHasEnded = false;
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

    public IEnumerator EndGame()
    {
        gameHasEnded = true;

        endGamePanel.ShowLoading();

        string get_url = "https://ship-games-api.herokuapp.com/GoogleSheetsAPI/get_all/1W7kbA0aYOBbAtylJ0NAECanZX8zPjCR8Gh0Gt5FHXqs/GameJam";
        string post_url = "https://ship-games-api.herokuapp.com/GoogleSheetsAPI/add_score/Pedro/500";

        string playerName = PlayerPrefs.GetString("PLAYER_NAME");
        string score = gameScore.currentGameScore.ToString();

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
                ScoreList scoreList = JsonUtility.FromJson<ScoreList>("{\"score\":" + webRequest.downloadHandler.text + "}" );

                print(scoreList.list[0].player_name);
            }
        }
    }

}

[System.Serializable]
class ScoreList
{
    public ScoreData[] list;
}

[System.Serializable]
class ScoreData
{
    public string row_index;
    public string player_name;
    public string player_score;
}

