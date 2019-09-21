using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GlobalEvents
{


    public static event EventHandler OnGameStart = delegate { };

   
    public static void GameStart(object sender, EventArgs eventArgs)
    {

        OnGameStart(sender, eventArgs);
    }

   
    public static event EventHandler OnGameEnd = delegate { };

   
    public static void GameEnd(object sender, EventArgs eventArgs)
    {

        OnGameEnd(sender, eventArgs);
    }

    
    public static event EventHandler OnPauseGame = delegate { };

   
    public static void PauseGame(object sender, EventArgs eventArgs)
    {

        OnPauseGame(sender, eventArgs);
    }

    public static event EventHandler OnCastleDamage = delegate { };

    public static void CastleDamage(object sender, CastleDamageArgs eventArgs)
    {
        OnCastleDamage(sender, eventArgs);
    }

    public static event EventHandler OnChangeLevel = delegate { };

    public static void ChangeLevel(object sender, ChangeLevelArgs eventArgs)
    {
        OnCastleDamage(sender, eventArgs);
    }


}

public class CastleDamageArgs : EventArgs
{
    public int damageTaken;
    public int currentCastleHealth;
    public int maxHealth;

    public CastleDamageArgs(int damageTaken, int currentCastleHealth, int maxHealth)
    {
        this.damageTaken = damageTaken;
        this.currentCastleHealth = currentCastleHealth;
        this.maxHealth = maxHealth;
    }
}

public class ChangeLevelArgs : EventArgs
{
    public int levelToChange;
}

