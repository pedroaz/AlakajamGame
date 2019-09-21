using UnityEngine;

public class StartMenuManager : MonoBehaviour
{
    

    public GameObject mainPanel;
    public GameObject creditsPanel;

    public void LoadScene(string sceneName)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
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

    public void GoToPortifolio(int i)
    {
        if (i == 0) {

        }

        if (i == 1) {

        }

        if (i == 2) {

        }

        if (i == 3) {
            Application.OpenURL("https://www.settingscon.com/");
        }
    }
}