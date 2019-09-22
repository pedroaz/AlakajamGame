using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedPowerUp : BaseItem
{
    public int speedIncrease = 2;
    public int duration = 15;


    internal override void CollectItem()
    {
        GlobalEvents.player.IncreasePlayerStats(0, speedIncrease, 0, duration, new Color(0.1f,0.7f,0.1f));
    }
}
