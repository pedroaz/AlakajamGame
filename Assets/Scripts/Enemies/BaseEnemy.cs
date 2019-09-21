using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemy : MonoBehaviour
{
    public int hp;
    public float speed;
    public int damageToCastle;
    public int enemyLevel;
    private bool canAct;
    public float attackCD;

    private Transform castleTransform;
    private Castle castle;
    private  bool canAttackCastle;
    private bool isAttacking;
    

    private void Awake()
    {
        castle = FindObjectOfType<Castle>();
        castleTransform = castle.transform;
    }

    private void Update()
    {
        if (canAct && !isAttacking) {
            Act();
        }
    }

    public virtual void Act()
    {
        if (canAttackCastle) {

            StartCoroutine(AttackCastle());
        }
        else{
            MoveTowardsCastle();
        }
    }

    internal virtual IEnumerator AttackCastle()
    {
        isAttacking = true;
        while (true) {
            Attack();
            yield return new WaitForSeconds(attackCD);
        }
        
    }

    internal virtual void Attack()
    {
        castle.HurtCastle(damageToCastle);
    }

    private void MoveTowardsCastle()
    {
        float step = speed * Time.deltaTime;
        transform.position = Vector2.MoveTowards(transform.position, castleTransform.position, step);
    }

    internal virtual void Die()
    {
        DropItem();
        GlobalEvents.EnemyDeath(this, null);
        Destroy(this);
    }

    internal virtual void DropItem()
    {

    }

    public void StartActing()
    {
        canAct = true;
    }

    public void StopActing()
    {
        canAct = false;
    }

    public bool IsInRangeOfCastle()
    {
        return false;
    }

    public void TakeDamage(int damage)
    {
        hp -= damage;
        if(hp <= 0) {
            Die();
        }
    }

    public void CanAttackCastle()
    {
        canAttackCastle = true;
    }
}
