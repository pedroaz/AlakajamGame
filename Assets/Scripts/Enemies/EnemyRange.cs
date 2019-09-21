using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRange : MonoBehaviour
{

    private BaseEnemy baseEnemy;

    private void Awake()
    {
        baseEnemy = GetComponentInParent<BaseEnemy>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "CASTLE") {
            baseEnemy.CanAttackCastle();
        }
    }
}
