using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseItem : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "ITEM_COLLECTION_AREA") {

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
