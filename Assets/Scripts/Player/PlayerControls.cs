using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : SpriteBase
{
    public float speed;
    public float atkSpeed = 0.07f;

    public GameObject baseWeaponObjLeft;
    public GameObject baseWeaponObjRight;

    private bool bIsAttacking = false;

    void FixedUpdate()
    {
        #region Player Movement
        if (canAct && (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0))
        {
            Vector3 input = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);
            boxCollider.transform.Translate(transform.TransformDirection(input * speed * Time.deltaTime));

            if(!bIsAttacking)
                spriteRenderer.flipX = (input.x < 0);
        }
        #endregion

        if (bIsPushBack)
        {
            transform.position = Vector2.Lerp(transform.position, perpDirection, pushbackSpeed * Time.fixedDeltaTime);
        }

        if (canAct && Input.GetKeyUp("space") && !bIsAttacking)
        {
            StartCoroutine(PlayerAttack());
        }
    }

    IEnumerator PlayerAttack()
    {
        bIsAttacking = true;

        spriteRenderer.flipX = !spriteRenderer.flipX;
        if(!spriteRenderer.flipX)
            baseWeaponObjLeft.SetActive(true);
        else
            baseWeaponObjRight.SetActive(true);

        yield return new WaitForSeconds(atkSpeed);

        spriteRenderer.flipX = !spriteRenderer.flipX;
        baseWeaponObjLeft.SetActive(false);
        baseWeaponObjRight.SetActive(false);

        bIsAttacking = false;
    }

    public void IncreasePlayerStats(int amountAtk, int amountSpeed, float pushBackIncreasePerc, int durationSecs, Color spriteTint)
    {
        StartCoroutine(PlayerIncreaseStatsCoroutine(amountAtk, amountSpeed, pushBackIncreasePerc, durationSecs, spriteTint));
    }

    IEnumerator PlayerIncreaseStatsCoroutine(int amountAtk, int amountSpeed, float pushBackIncreasePerc, int durationSecs, Color spriteTint)
    {
        BaseWeapon baseWeaponLeft = baseWeaponObjLeft.GetComponent<BaseWeapon>();
        BaseWeapon baseWeaponRight = baseWeaponObjRight.GetComponent<BaseWeapon>();

        spriteRenderer.color = spriteTint;

        baseWeaponLeft.weaponDamage += amountAtk;
        baseWeaponRight.weaponDamage += amountAtk;

        baseWeaponLeft.weaponPushback += amountAtk * pushBackIncreasePerc;
        baseWeaponRight.weaponPushback += amountAtk * pushBackIncreasePerc;

        speed += amountSpeed;

        yield return new WaitForSeconds(durationSecs * 0.75f);

        //Start blinking only after two thirds of the time is spent.
        for (var steps = durationSecs * 0.25f; steps <= durationSecs; steps++)
        {
            spriteRenderer.color = spriteTint;

            yield return new WaitForSeconds(1f / steps);

            spriteRenderer.color = new Color(1f, 1f, 1f);

            yield return new WaitForSeconds(1f / steps);
        }

        baseWeaponLeft.weaponDamage -= amountAtk;
        baseWeaponRight.weaponDamage -= amountAtk;

        baseWeaponLeft.weaponPushback -= amountAtk * pushBackIncreasePerc;
        baseWeaponRight.weaponPushback -= amountAtk * pushBackIncreasePerc;

        speed -= amountSpeed;

        spriteRenderer.color = new Color(1f, 1f, 1f);
    }
}
