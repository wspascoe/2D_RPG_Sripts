using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] SpriteRenderer playerSprite;
    [SerializeField] bool Using2Dir = false;
    private Controls controls;
    private Rigidbody2D rBody;
    private Animator animator;
    private Vector2 moveInput;
    private bool isMoving = false;

    private void Awake()
    {
        controls = new Controls();
        rBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        controls.Player.Enable();
    }

    private void OnDisable()
    {
        controls.Player.Disable();
    }

    private void Start()
    {
        //We do this so the player starts facing down
        animator.SetFloat("moveX", 0);
        animator.SetFloat("moveY", -1);
    }

    private void Update()
    {
        if(Using2Dir)
        {
            UpdateAnimator2Dir();
        }
        else
        {
            UpdateAnimator4Dir();
        }
    }

    private void FixedUpdate()
    {
        moveInput = controls.Player.Movement.ReadValue<Vector2>();
        rBody.velocity = moveInput * speed;
    }

    private void UpdateAnimator4Dir()
    {
        if(moveInput != Vector2.zero)
        {
            animator.SetBool("IsWalking", true);
            animator.SetFloat("moveX", moveInput.x);
            animator.SetFloat("moveY", moveInput.y);          
        }
        else
        {
            animator.SetBool("IsWalking", false);
        }
    }

    private void UpdateAnimator2Dir()
    {
        if(moveInput == Vector2.zero)
        {
            isMoving = false;
        }
        else
        {
            isMoving = true;
        }
        //Set Bool based on whether we are moving or not
        animator.SetBool("isWalking", isMoving);

        //We use this because we dont have 4 dir sprites
        if (moveInput.x < 0 && isMoving) //Going Left
        {
            playerSprite.flipX = true;
        }
        else if (moveInput.x > 0 && isMoving)
        {
            playerSprite.flipX = false;
        }

    }
}
