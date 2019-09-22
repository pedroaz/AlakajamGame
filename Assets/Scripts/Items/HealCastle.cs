using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealCastle : BaseItem
{
    public int amountToHeal;

    internal override void CollectItem()
    {
        FindObjectOfType<Castle>().HealCastle(amountToHeal);
        FindObjectOfType<ItemPanel>().ShowItemPanel(3, "Heal Castle");
    }
}
