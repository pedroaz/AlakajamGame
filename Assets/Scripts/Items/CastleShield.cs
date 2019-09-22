using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastleShield : BaseItem
{
    internal override void CollectItem()
    {
        GameObject.FindGameObjectWithTag("POWER_UP").GetComponent<AudioSource>().Play();
        FindObjectOfType<Castle>().Protect();
        FindObjectOfType<ItemPanel>().ShowItemPanel(1, "Castle Defense");
    }
}
