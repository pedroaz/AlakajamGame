using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEnder : MonoBehaviour
{
    private void Awake()
    {
        GlobalEvents.OnCastleDamage += CheckIfGameEnd;
    }

    private void OnDestroy()
    {
        GlobalEvents.OnCastleDamage -= CheckIfGameEnd;
    }

    public void CheckIfGameEnd(object sender, System.EventArgs e)
    {
        CastleDamageArgs arg = (CastleDamageArgs) e;

        if(arg.currentCastleHealth <=0) {

            EndGame();
        }
    }

    public void EndGame()
    {
        print("GAME HAS ENDED");
    }
}
