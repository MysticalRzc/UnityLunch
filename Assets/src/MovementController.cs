using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    public float movementSpeed = 3.0f;
    Vector2 movement = new Vector2();
    Animator animator;

    Rigidbody2D rb2D;

    //enum CharStates
    //{
    //    walkEast = 1,
    //    walkSouth = 2,
    //    walkWest = 3,
    //    walkNorth = 4,

    //    idleSouth = 5
    //}

    private void Start()
    {
        animator = GetComponent<Animator>();
        rb2D = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        UpdateState();
    }

    void FixedUpdate()
    {
        MoveCharacter();
    }

    private void MoveCharacter()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        movement.Normalize();
        rb2D.velocity = movement * movementSpeed;
    }

    string IS_WALKING = "isWalking";
    string IS_FIRE = "isFiring";
    string X_DIR = "xDir";
    string Y_DIR = "yDir";
    private void UpdateState()
    {
        if (Mathf.Approximately(movement.x, 0) && Mathf.Approximately(movement.y, 0))
        {
            animator.SetBool(IS_WALKING, false);
        }
        else
        {
            animator.SetBool(IS_WALKING, true);
        }

        if (Input.GetMouseButtonDown(0))
        {
            animator.SetBool(IS_FIRE, true);
        }
        else
        {
            animator.SetBool(IS_FIRE, false);
        }
        animator.SetFloat(X_DIR, movement.x);
        animator.SetFloat(Y_DIR, movement.y);
    }
}