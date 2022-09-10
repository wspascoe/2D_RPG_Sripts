using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed = 5f;

    private Controls controls;
    private Rigidbody2D rBody;
    private Vector2 moveInput;

    private void Awake()
    {
        controls = new Controls();
        rBody = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        controls.Player.Enable();
    }

    private void OnDisable()
    {
        controls.Player.Disable();
    }

    private void FixedUpdate()
    {
        moveInput = controls.Player.Movement.ReadValue<Vector2>();
        rBody.velocity = moveInput * speed;
    }
}
