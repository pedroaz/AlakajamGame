using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopEnemies : BaseItem
{
    internal override void CollectItem()
    {
        GlobalEvents.StopEnemies(this, null);
        FindObjectOfType<ItemPanel>().ShowItemPanel(4, "Stun Enemies");
    }
}
