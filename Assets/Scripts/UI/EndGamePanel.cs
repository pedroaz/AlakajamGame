using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGamePanel : MonoBehaviour
{
    public GameObject container;
    public GameObject loadingScreen;

    public void ShowLoading()
    {
        container.SetActive(true);
        loadingScreen.SetActive(true);
    }

    public void ShowComplete()
    {
        loadingScreen.SetActive(false);
    }

    public void Hide()
    {
        container.SetActive(false);
        loadingScreen.SetActive(false);
    }

}
