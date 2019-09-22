using UnityEngine;

public class BaseWeapon : MonoBehaviour
{
    public float weaponPushback = 5.0f;
    public int weaponDamage = 10;

    PlayerControls basePlayer;

    private void Awake()
    {
        basePlayer = GetComponentInParent<PlayerControls>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "ENEMY")
        {
            GlobalEvents.WeaponCollision(this, new WeaponCollisionArgs(collision.gameObject.GetInstanceID(),
                basePlayer.gameObject.transform.position, weaponPushback, weaponDamage));
        }
    }
}
