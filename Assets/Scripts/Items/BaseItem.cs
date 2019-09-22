using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseItem : MonoBehaviour
{
    public int itemSpriteLifetime = 10;

    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        StartCoroutine(ItemLifetime());
    }

    IEnumerator ItemLifetime()
    {
        yield return new WaitForSeconds(itemSpriteLifetime*0.75f);

        for (var steps = 1; steps <= 10; steps++)
        {
            spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 0f);

            yield return new WaitForSeconds(itemSpriteLifetime * 0.05f / steps);

            spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 1f);

            yield return new WaitForSeconds(itemSpriteLifetime * 0.25f / steps);
        }

        DestroyItem();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "ITEM_COLLECTION_AREA")
        {
            CollectItem();
            DestroyItem();
        }
    }

    internal virtual void CollectItem()
    {

    }

    private void DestroyItem()
    {
        Destroy(gameObject);
    }
}
