using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GlobalEvents
{
    public static PlayerControls player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControls>();

    public static event EventHandler OnWaveStart = delegate { };

   
    public static void StartWave(object sender, EventArgs eventArgs)
    {

        OnWaveStart(sender, eventArgs);
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

    public static event EventHandler OnPlayerCollision = delegate { };

    public static void PlayerCollision(object sender, PlayerCollisionArgs eventArgs)
    {
        OnPlayerCollision(sender, eventArgs);
    }

    public static event EventHandler OnAddGameScore = delegate { };

    public static void AddGameScore(object sender, GameScoreArgs eventArgs)
    {
        OnAddGameScore(sender, eventArgs);
    }

    public static event EventHandler OnEnemyDeath = delegate { };

    public static void EnemyDeath(object sender, EventArgs eventArgs)
    {
        OnEnemyDeath(sender, eventArgs);
    }

}

public class GameScoreArgs : EventArgs
{
    public int scoreToAdd;

    public GameScoreArgs(int scoreToAdd)
    {
        this.scoreToAdd = scoreToAdd;
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

public class PlayerCollisionArgs : EventArgs
{
    public Vector3 direction;

    public float pushbackValue;

    public PlayerCollisionArgs(Vector3 direction, float pushbackValue)
    {
        this.direction = direction;
        this.pushbackValue = pushbackValue;
    }
}

