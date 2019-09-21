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


    private void Awake()
    {
        castleTransform = FindObjectOfType<Castle>().transform;
    }

    private void Update()
    {
        if (canAct) {
            Act();
        }
    }

    public virtual void Act()
    {
        float step = speed * Time.deltaTime;
        transform.position = Vector2.MoveTowards(transform.position, castleTransform.position, step);
    }

    public virtual void DropItem()
    {

    }

    public void SpawnEnemy()
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
