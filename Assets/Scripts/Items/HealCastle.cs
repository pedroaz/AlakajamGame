using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealCastle : BaseItem
{
    public int amountToHeal;

    internal override void CollectItem()
    {
        GameObject.FindGameObjectWithTag("POWER_UP").GetComponent<AudioSource>().Play();
        FindObjectOfType<Castle>().HealCastle(amountToHeal);
        FindObjectOfType<ItemPanel>().ShowItemPanel(3, "Heal Castle");
    }
}
