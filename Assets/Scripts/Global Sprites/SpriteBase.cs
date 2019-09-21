using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteBase : MonoBehaviour
{
    protected BoxCollider2D boxCollider;
    protected SpriteRenderer spriteRenderer;
    protected Vector2 perpDirection;
    protected float pushbackSpeed;

    protected bool canAct = true;
    protected bool bIsPushBack = false;

    public float pushbackTimer = 0.2f;
    public int invulnFlashes = 5;
    public float startingFlashCD = 0.3f;

    private void Awake()
    {
        GlobalEvents.OnPlayerCollision += SpritePushback;
    }

    void Start()
    {
        boxCollider = gameObject.GetComponent<BoxCollider2D>();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    protected void SpritePushback(object sender, System.EventArgs e)
    {
        if (!canAct) return;

        PlayerCollisionArgs arg = (PlayerCollisionArgs)e;

        bIsPushBack = true;
        perpDirection = (Vector2)(boxCollider.transform.position - arg.direction) * arg.pushbackValue;
        perpDirection += (Vector2)boxCollider.transform.position;
        pushbackSpeed = arg.pushbackValue;

        SpriteRenderer[] sprites = GetComponentsInChildren<SpriteRenderer>();
        StartCoroutine(SpriteFlash(sprites, invulnFlashes, startingFlashCD));
        StartCoroutine(SpriteSlideBack());
    }

    IEnumerator SpriteSlideBack()
    {
        yield return new WaitForSeconds(pushbackTimer);
        if (bIsPushBack) bIsPushBack = false;
    }

    IEnumerator SpriteFlash(SpriteRenderer[] sprites, int numTimes, float intialDelay, bool disable = false)
    {
        canAct = false;

        for (int loop = 1; loop <= numTimes; loop++)
        {
            for (int i = 0; i < sprites.Length; i++)
            {
                if (disable)
                    sprites[i].enabled = false;
                else
                    sprites[i].color = new Color(sprites[i].color.r, sprites[i].color.g, sprites[i].color.b, 0.5f);
            }

            yield return new WaitForSeconds(intialDelay / loop);

            for (int i = 0; i < sprites.Length; i++)
            {
                if (disable)
                    sprites[i].enabled = true;
                else
                    sprites[i].color = new Color(sprites[i].color.r, sprites[i].color.g, sprites[i].color.b, 1);
            }

            yield return new WaitForSeconds(intialDelay / loop);
        }

        canAct = true;
    }
}
