using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHurtBox : MonoBehaviour
{
    public LayerMask playerAttackLayer;

    private BaseEnemy baseEnemy;

    public float pushbackStrength = 1f;

    private void Awake()
    {
        baseEnemy = GetComponentInParent<BaseEnemy>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == playerAttackLayer) {

            baseEnemy.TakeDamage(1);
        }

        switch (collision.tag)
        {
            case "Player":
                GlobalEvents.PlayerCollision(this, new PlayerCollisionArgs(baseEnemy.transform.position, pushbackStrength));
                break;
            case "CASTLE":

                break;
        }
    }
}
