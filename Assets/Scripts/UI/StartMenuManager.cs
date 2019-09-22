using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class StartMenuManager : MonoBehaviour
{
    

    public GameObject mainPanel;
    public GameObject creditsPanel;
    TMP_InputField inputField;
    public Button startButton;
    public AudioSource uiSound;
    public AudioSource playSound;

    private void Awake()
    {
        inputField = FindObjectOfType<TMP_InputField>();

        if(inputField!= null) {
            CanStart();
        }
    }

    

    public void CanStart()
    {
        if(inputField.text == "") {
            startButton.interactable = false;
        }
        else {
            startButton.interactable = true;
        }
    }

    public void PlayTheGame()
    {
        playSound.Play();
        PlayerPrefs.SetString("PLAYER_NAME", inputField.text);
        LoadScene("StartScene");
    }

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
        playSound.Play();
        mainPanel.SetActive(false);
        creditsPanel.SetActive(true);
    }

    public void ReturnButton()
    {
        playSound.Play();
        mainPanel.SetActive(true);
        creditsPanel.SetActive(false);
    }

    public void GoToPortifolio(int i)
    {
        playSound.Play();
        if (i == 0) {

            Application.OpenURL("https://guilhermenery.artstation.com/");
        }

        if (i == 1) {

            Application.OpenURL("https://gustavomaranhao.carbonmade.com/");
        }

        if (i == 2) {

            Application.OpenURL("https://www.pedroazvm.com/");
        }

        if (i == 3) {
            Application.OpenURL("https://www.settingscon.com/");
        }
    }
}