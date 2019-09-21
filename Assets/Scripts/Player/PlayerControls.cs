using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    public float speed;

    private BoxCollider2D boxCollider;
    private SpriteRenderer spriteRenderer;

    private bool canMove = true;
    private bool bIsPushBack = false;
    public float pushbackSpeed = 0.3f;
    public float pushbackTimer = 0.2f;
    public int invulnFlashes = 5;
    public float startingFlashCD = 0.3f;

    private Vector2 perpDirection;

    private void Awake()
    {
        GlobalEvents.OnPlayerCollision += PlayerPushback;
    }

    void Start()
    {
        boxCollider = gameObject.GetComponent<BoxCollider2D>();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    void FixedUpdate()
    {
        #region Player Movement
        if (canMove && (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0))
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

    public void PlayerPushback(object sender, System.EventArgs e)
    {
        PlayerCollisionArgs arg = (PlayerCollisionArgs) e;
        //var perpDirection = new Vector2(-1*arg.direction.x, arg.direction.y);
        perpDirection = (Vector2)(transform.position - arg.direction)*arg.pushbackValue;

        if (canMove)
        {
            bIsPushBack = true;
            SpriteRenderer[] sprites = GetComponentsInChildren<SpriteRenderer>();
            StartCoroutine(PlayerInvuln(sprites, invulnFlashes, startingFlashCD));
            StartCoroutine(PlayerSlideBack());
        }
    }

    IEnumerator PlayerSlideBack()
    {
        yield return new WaitForSeconds(pushbackTimer);
        if (bIsPushBack) bIsPushBack = false;
    }

    IEnumerator PlayerInvuln(SpriteRenderer[] sprites, int numTimes, float intialDelay, bool disable = false)
    {
        canMove = false;

        for (int loop = 1; loop <= numTimes; loop++)
        {
            for (int i = 0; i < sprites.Length; i++)
            {
                if (disable)
                    sprites[i].enabled = false;
                else
                    sprites[i].color = new Color(sprites[i].color.r, sprites[i].color.g, sprites[i].color.b, 0.5f);
            }

            yield return new WaitForSeconds(intialDelay/loop);

            for (int i = 0; i < sprites.Length; i++)
            {
                if (disable)
                    sprites[i].enabled = true;
                else
                    sprites[i].color = new Color(sprites[i].color.r, sprites[i].color.g, sprites[i].color.b, 1);
            }

            yield return new WaitForSeconds(intialDelay/loop);
        }

        canMove = true;
    }

}
