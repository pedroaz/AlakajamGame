using UnityEngine;

public class StartMenuManager : MonoBehaviour
{
    public enum SCENENAMES
    {
        MenuScene = 0,
        StartScene = 1
    }

    public SCENENAMES sceneToLoad;
    public GameObject mainPanel;
    public GameObject creditsPanel;

    public void StartButton()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene((int)sceneToLoad);
    }

    public void ExitButton()
    {
        Debug.Log("Quitting game.");
        Application.Quit();
    }

    public void CreditsButton()
    {
        mainPanel.SetActive(false);
        creditsPanel.SetActive(true);
    }

    public void ReturnButton()
    {
        mainPanel.SetActive(true);
        creditsPanel.SetActive(false);
    }
}
