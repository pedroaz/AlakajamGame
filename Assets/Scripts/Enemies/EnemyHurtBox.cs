using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHurtBox : MonoBehaviour
{
    private BaseEnemy baseEnemy;

    public float pushbackStrength = 0.1f;

    private void Awake()
    {
        baseEnemy = GetComponentInParent<BaseEnemy>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Player":
                //GlobalEvents.PlayerCollision(this, new PlayerCollisionArgs(baseEnemy.transform.position, pushbackStrength));
                break;
        }
    }
}
