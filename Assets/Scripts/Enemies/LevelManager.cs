using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField]
    private int currentLevel;

    private void Awake()
    {
        GlobalEvents.OnChangeLevel += ChangeLevel;
    }

    private void OnDestroy()
    {
        GlobalEvents.OnCastleDamage -= ChangeLevel;
    }

    public void ChangeLevel(object sender, System.EventArgs e)
    {
        ChangeLevelArgs arg = (ChangeLevelArgs)e;

        currentLevel = arg.levelToChange;
    }

    public int GetCurrentLevel()
    {
        return currentLevel;
    }
}
