using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryManager : MonoBehaviour
{
    public Transform containerTransform;
    List<Transform> listOfStories;
    public int currentIndex;
    public int maxIndex;

    public GameObject previousButton;
    public GameObject nextButton;
    public GameObject playButton;

    void Start()
    {
        listOfStories = new List<Transform>();
        for (int i = 0; i < containerTransform.childCount; i++) {

            listOfStories.Add(containerTransform.GetChild(i));
        }
        maxIndex = listOfStories.Count-1;
        ShowScene(0);   
    }

    public void ShowButtons()
    {
        previousButton.SetActive(true);
        nextButton.SetActive(true);
        playButton.SetActive(false);
        if (currentIndex == 0) {

            previousButton.SetActive(false);
        }
        if(currentIndex == maxIndex) {

            nextButton.SetActive(false);
            playButton.SetActive(true);
        }
    }

    public void Next()
    {
        ShowScene(currentIndex + 1);
    }

    public void Previous()
    {
        ShowScene(currentIndex - 1);
    }

    public void ShowScene(int index)
    {
        currentIndex = index;
        ShowButtons();
        foreach (var t in listOfStories) {

            t.gameObject.SetActive(false);
        }
        listOfStories[currentIndex].gameObject.SetActive(true);
    }
}
