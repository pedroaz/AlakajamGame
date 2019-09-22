using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : SpriteBase
{
    public float speed;
    public float atkSpeed = 0.07f;

    public GameObject baseWeaponObjLeft;
    public GameObject baseWeaponObjRight;
    public GameObject baseWeaponObjUp;
    public GameObject baseWeaponObjDown;

    private bool bIsAttacking = false;
    public Animator spriteAnimator;

    private int hasBeenIdleFor = 0;

    public AudioSource attackAudio;
    public AudioSource attackAudioTwo;
    public AudioSource hurtAudio;
    public AudioSource hurtAudio2;
    public AudioSource hurtAudio3;

    void FixedUpdate()
    {
        #region Player Movement
        spriteAnimator.SetBool("GoToIdle", false);
        spriteAnimator.SetBool("WalkingRight", false);
        spriteAnimator.SetBool("WalkingLeft", false);
        spriteAnimator.SetBool("WalkingUp", false);
        spriteAnimator.SetBool("WalkingDown", false);

        if ((Input.GetAxis("Horizontal") == 0) && (Input.GetAxis("Vertical") == 0))
        {
            hasBeenIdleFor++;
        }
        if(hasBeenIdleFor > 2)
        {
            spriteAnimator.SetBool("GoToIdle", true);
        }

        if (canAct && (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0))
        {
            hasBeenIdleFor = 0;
            if (Input.GetAxis("Horizontal") != 0)
            {
                if (Input.GetAxis("Horizontal") > 0)
                    spriteAnimator.SetBool("WalkingRight", true);
                else
                    spriteAnimator.SetBool("WalkingLeft", true);
            }
            if (Input.GetAxis("Vertical") != 0)
            {
                if (Input.GetAxis("Vertical") > 0)
                    spriteAnimator.SetBool("WalkingUp", true);
                else
                    spriteAnimator.SetBool("WalkingDown", true);
            }

            Vector3 input = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);
            boxCollider.transform.Translate(transform.TransformDirection(input * speed * Time.deltaTime));

            if(!bIsAttacking)
                spriteRenderer.flipX = (input.x < 0);
        }
        #endregion

        if (bIsPushBack)
        {
            if (!hurtAudio.isPlaying && !hurtAudio2.isPlaying && !hurtAudio3.isPlaying) {
                float randomValue = Random.Range(0f, 1f);

                if(randomValue < 0.33f) {

                    hurtAudio.Play();
                }
                else if(randomValue < 0.66f) {

                    hurtAudio2.Play();
                }
                else {

                    hurtAudio3.Play();
                }

                
            }

            transform.position = Vector2.Lerp(transform.position, perpDirection, pushbackSpeed * Time.fixedDeltaTime);
        }

        if (canAct && Input.GetKeyUp("space"))
        {
            StartCoroutine(PlayerAttack());
        }

        if (!spriteAnimator.GetBool("StartAttackAnim"))
        {
            baseWeaponObjLeft.SetActive(false);
            baseWeaponObjRight.SetActive(false);
            baseWeaponObjUp.SetActive(false);
            baseWeaponObjDown.SetActive(false);
            bIsAttacking = false;
        }
    }

    IEnumerator PlayerAttack()
    {
        if (!attackAudio.isPlaying && !attackAudioTwo.isPlaying) {

            float randomValue = Random.Range(0f, 1f);
            if(randomValue < 0.5f) {
                attackAudio.Play();
            }
            else {
                attackAudioTwo.Play();
            }
            
        }

        hasBeenIdleFor = 0;
        spriteAnimator.SetBool("StartAttackAnim", true);
        //spriteAnimator.SetBool("GoToIdle", false);
        bIsAttacking = true;

        if (spriteAnimator.GetBool("WalkingRight"))
            baseWeaponObjRight.SetActive(true);
        if (spriteAnimator.GetBool("WalkingLeft"))
            baseWeaponObjLeft.SetActive(true);
        if (spriteAnimator.GetBool("WalkingUp"))
            baseWeaponObjUp.SetActive(true);
        if (spriteAnimator.GetBool("WalkingDown"))
            baseWeaponObjDown.SetActive(true);
        if (spriteAnimator.GetBool("GoToIdle"))
        {
            if (spriteRenderer.flipX)
                baseWeaponObjLeft.SetActive(true);
            else
                baseWeaponObjRight.SetActive(true);
        }

        yield return new WaitForSeconds(atkSpeed);

        yield return new WaitForSeconds(0.5f);
        spriteAnimator.SetBool("StartAttackAnim", false);
    }

    public void IncreasePlayerStats(int amountAtk, int amountSpeed, float pushBackIncreasePerc, int durationSecs, Color spriteTint)
    {
        StartCoroutine(PlayerIncreaseStatsCoroutine(amountAtk, amountSpeed, pushBackIncreasePerc, durationSecs, spriteTint));
    }

    IEnumerator PlayerIncreaseStatsCoroutine(int amountAtk, int amountSpeed, float pushBackIncreasePerc, int durationSecs, Color spriteTint)
    {
        GameObject.FindGameObjectWithTag("POWER_UP").GetComponent<AudioSource>().Play();
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
        GameObject.FindGameObjectWithTag("POWER_DOWN").GetComponent<AudioSource>().Play();
    }
}
