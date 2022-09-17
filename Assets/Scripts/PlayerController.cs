using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

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
    private Coroutine coroutine;

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
        controls.Player.Attack.performed += Attack;
        //We do this so the player starts facing down
        animator.SetFloat("moveX", 0);
        animator.SetFloat("moveY", -1);
    }

    private void Update()
    {
        //if(Input.GetKeyDown(KeyCode.Space))
        //{
        //    Debug.Log("Pressed");
        //    Attack();
        //}
        UpdateAnimator();
    }

    private void FixedUpdate()
    {
        moveInput = controls.Player.Movement.ReadValue<Vector2>();
        rBody.velocity = moveInput * speed;
    }

    private void Attack(InputAction.CallbackContext context)
    {
        animator.SetTrigger("attacking");
        //if (coroutine != null)
        //{
        //    StopCoroutine(coroutine);
        //}
        //coroutine = StartCoroutine(AttackCoRoutine());
    }

    private IEnumerator AttackCoRoutine()
    {
        animator.SetBool("IsAttacking", true);
        yield return null;
        animator.SetBool("IsAttacking", false);
        yield return new WaitForSeconds(.5f);
    }

    private void UpdateAnimator()
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
}
