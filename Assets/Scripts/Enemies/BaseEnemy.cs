using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemy : SpriteBase
{
    public int hp;
    public float speed;
    public int damageToCastle;
    public int enemyLevel;

    public float chanceToDropItems = 35f;

    public float attackCD;

    private Transform castleTransform;
    private Castle castle;
    private bool canAttackCastle;
    private bool isAttacking;
    private bool stunned = false;
    
    public List<GameObject> listOfItems;
    
    private void Awake()
    {
        castle = FindObjectOfType<Castle>();
        castleTransform = castle.transform;

        GlobalEvents.OnWeaponCollision += TakeDamageFromPlayer;
        GlobalEvents.OnStopEnemies += StunEnemy;
    }

    private void OnDestroy()
    {
        GlobalEvents.OnWeaponCollision -= TakeDamageFromPlayer;
        GlobalEvents.OnStopEnemies -= StunEnemy;
    }

    private void StunEnemy(object sender, System.EventArgs e)
    {
        StartCoroutine(Stun());
    }

    private IEnumerator Stun()
    {
        stunned = true;
        yield return new WaitForSeconds(3);
        stunned = false;
    }

    private void Update()
    {
        if (canAct && !isAttacking)
        {
            Act();
        }

        if (bIsPushBack)
        {
            transform.position = Vector2.Lerp(transform.position, perpDirection, pushbackSpeed * Time.fixedDeltaTime);
        }
    }

    public virtual void Act()
    {
        if (stunned) {
            return;
        }
        if (canAttackCastle)
        {
            if (isAttacking) {
                return;
            }
            StartCoroutine(AttackCastle());
        }
        else
        {
            MoveTowardsCastle();
        }
    }

    internal virtual IEnumerator AttackCastle()
    {
        isAttacking = true;
        while (true)
        {
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
        var random = Random.Range(0, 100);
        if(random <= chanceToDropItems)
            DropItem();

        GlobalEvents.EnemyDeath(this, null);
        Destroy(this.gameObject);
    }

    internal virtual void DropItem()
    {
        if(listOfItems.Count > 0) {

            int index = Random.Range(0, listOfItems.Count);
            GameObject item = Instantiate(listOfItems[index], this.transform.position, Quaternion.identity);
        }
        
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
        if (hp <= 0)
        {
            Die();
        }
    }

    public void CanAttackCastle(bool value)
    {
        canAttackCastle = value;
    }

    private void TakeDamageFromPlayer(object sender, System.EventArgs e)
    {
        WeaponCollisionArgs arg = (WeaponCollisionArgs)e;
        
        if ((!canAct) || (gameObject == null) || arg.enemyAttackedID != gameObject.GetInstanceID()) return;
        
        SpritePushback(this, new PlayerCollisionArgs(arg.direction, arg.pushbackValue));
        TakeDamage(arg.weaponDamage);
    }
}
