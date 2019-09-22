using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopEnemies : BaseItem
{
    internal override void CollectItem()
    {
        GameObject.FindGameObjectWithTag("POWER_UP").GetComponent<AudioSource>().Play();
        GlobalEvents.StopEnemies(this, null);
        FindObjectOfType<ItemPanel>().ShowItemPanel(5, "Stun Enemies");
    }
}
