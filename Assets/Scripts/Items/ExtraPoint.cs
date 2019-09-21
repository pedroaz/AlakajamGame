using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtraPoint : BaseItem
{

    public int extraPointToAdd;
    
    internal override void CollectItem()
    {
        GlobalEvents.AddGameScore(this, new GameScoreArgs(extraPointToAdd));    
    }
}
