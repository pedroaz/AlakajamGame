using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : SpriteBase
{
    public float speed;

    void FixedUpdate()
    {
        #region Player Movement
        if (canAct && (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0))
        {
            Vector3 input = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);
            boxCollider.transform.Translate(transform.TransformDirection(input * speed * Time.deltaTime));

            spriteRenderer.flipX = (input.x < 0);
        }
        #endregion

        if (bIsPushBack)
        {
            transform.position = Vector2.Lerp(transform.position, perpDirection, pushbackSpeed * Time.fixedDeltaTime);
        }
    }
}
