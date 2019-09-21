using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Castle : MonoBehaviour
{

    public int maxHealth;
    public int currentHealth;

    public void HurtCastle(int damage)
    {
        currentHealth -= damage;
        GlobalEvents.CastleDamage(this, new CastleDamageArgs(damage, currentHealth));
    }
}
