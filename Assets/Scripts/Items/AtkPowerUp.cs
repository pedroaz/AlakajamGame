using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtkPowerUp : BaseItem
{
    public int atkIncrease = 2;
    public float pushbackPerc = 0.45f;
    public int duration = 15;


    internal override void CollectItem()
    {
        GlobalEvents.player.IncreasePlayerStats(atkIncrease, 0, pushbackPerc, duration, new Color(0.7f,0.1f,0.1f));
    }
}
