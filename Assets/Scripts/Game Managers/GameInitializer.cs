using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInitializer : MonoBehaviour
{
    LevelManager levelManager;
    LevelPanel levelPanel;
    private void Awake()
    {
        levelManager = FindObjectOfType<LevelManager>();
        levelPanel = FindObjectOfType<LevelPanel>();
    }

    void Start()
    {
        StartNextWave();
        levelPanel.ShowPanel(1);
    }

    private void StartNextWave()
    {
        print("START NEXT WAVE");
        GlobalEvents.StartWave(this, null);
    }

    public void EndWave()
    {
        StartCoroutine(NextWave());
    }

    public IEnumerator NextWave()
    {
        levelManager.AddLevel();
        levelPanel.ShowPanel(levelManager.GetCurrentLevel());
        yield return new WaitForSeconds(5);
        StartNextWave();
    }
}
