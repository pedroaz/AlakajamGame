using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Castle : MonoBehaviour
{

    public int maxHealth;
    public int currentHealth;

    private void Awake()
    {
        currentHealth = maxHealth;
        GlobalEvents.CastleDamage(this, new CastleDamageArgs(0, currentHealth, maxHealth));
    }

    public void HurtCastle(int damage)
    {
        currentHealth -= damage;
        GlobalEvents.CastleDamage(this, new CastleDamageArgs(damage, currentHealth, maxHealth));
    }

    public void HealCastle(int heal)
    {
        currentHealth += heal;
        if(currentHealth >= maxHealth) {
            currentHealth = maxHealth;
        }
        GlobalEvents.CastleDamage(this, new CastleDamageArgs(-heal, currentHealth, maxHealth));
    }

    public void FullHealth()
    {
        GlobalEvents.CastleDamage(this, new CastleDamageArgs(0, currentHealth, maxHealth));
    }
}
