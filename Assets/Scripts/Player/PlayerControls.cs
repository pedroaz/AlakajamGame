using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    public float speed;

    private CharacterController characterController;
    public BoxCollider2D boxCollider;

    void Start()
    {
        characterController = gameObject.GetComponent<CharacterController>();
    }

    void FixedUpdate()
    {
        #region Player Movement
        if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
        {
            Vector3 input = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);
            boxCollider.transform.Translate(transform.TransformDirection(input * speed * Time.deltaTime));
        }
        #endregion
    }
}
