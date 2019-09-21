using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemy : MonoBehaviour
{
    public int hp;
    public float speed;
    public int damateToCastle;
    public int enemyLevel;
    public bool canAct;
    private Transform castleTransform;
    private Castle castle;
    public bool canAttackCastle;
    public float attackCD;
    public bool isAttacking;

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
        castle.HurtCastle(damateToCastle);
    }

    private void MoveTowardsCastle()
    {
        float step = speed * Time.deltaTime;
        transform.position = Vector2.MoveTowards(transform.position, castleTransform.position, step);
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

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.tag)
        {
            case "Player":
                GlobalEvents.PlayerCollision(this, new PlayerCollisionArgs(collision.transform.up, 10));
                break;
            case "CASTLE":

                break;
        }
    }
}
