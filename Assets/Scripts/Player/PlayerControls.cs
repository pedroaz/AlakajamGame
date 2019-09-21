using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : SpriteBase
{
    public float speed;
    public float atkSpeed = 0.07f;

    public GameObject baseWeaponObjLeft;
    public GameObject baseWeaponObjRight;
    public GameObject playerArea;

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

        if (canAct && Input.GetKeyUp("space"))
        {
            StartCoroutine(PlayerAttack());
        }
    }

    IEnumerator PlayerAttack()
    {
        var playerAreaCollider = playerArea.GetComponent<BoxCollider2D>();

        spriteRenderer.flipX = !spriteRenderer.flipX;
        if(!spriteRenderer.flipX)
            baseWeaponObjLeft.SetActive(true);
        else
            baseWeaponObjRight.SetActive(true);

        yield return new WaitForSeconds(atkSpeed);

        spriteRenderer.flipX = !spriteRenderer.flipX;
        baseWeaponObjLeft.SetActive(false);
        baseWeaponObjRight.SetActive(false);
    }
}
