using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    public float speed;

    private BoxCollider2D boxCollider;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        boxCollider = gameObject.GetComponent<BoxCollider2D>();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    void FixedUpdate()
    {
        #region Player Movement
        if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
        {
            Vector3 input = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);
            boxCollider.transform.Translate(transform.TransformDirection(input * speed * Time.deltaTime));

            spriteRenderer.flipX = (input.x < 0);
        }
        #endregion
    }

    public void PlayerPushback(object sender, System.EventArgs e)
    {
        PlayerCollisionArgs arg = (PlayerCollisionArgs) e;

        boxCollider.transform.Translate(transform.TransformDirection(arg.direction * arg.pushbackValue * Time.deltaTime));
    }
}
