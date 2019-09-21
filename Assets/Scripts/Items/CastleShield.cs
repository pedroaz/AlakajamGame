using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastleShield : BaseItem
{
    internal override void CollectItem()
    {
        FindObjectOfType<Castle>().Protect();
    }
}
