using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    public Rigidbody2D rb;
    private bool isMoving;
    public Vector2 input;
    private Vector2 lastMoveDirection;
    private Animator animator;
    public GameObject sword;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void Update()
    {
        ProccessInput();
        SprintInput();
        
    }

    private void FixedUpdate()
    {
        //Physics Calculations
        Move();
        Animate();
    }

    private void ProccessInput()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        if(moveX == 0 && moveY == 0 && (input.x != 0 || input.y != 0))
        {
            lastMoveDirection = input;
        }
        
        input = new Vector2(moveX, moveY).normalized;
    }

    private void SprintInput()
    {
        if(Input.GetKeyDown(KeyCode.LeftShift))
            moveSpeed = 13;
        else if (Input.GetKeyUp(KeyCode.LeftShift))
            moveSpeed = 10;
    }

    private void Move()
    {
        rb.velocity = new Vector2(input.x * moveSpeed, input.y * moveSpeed);
    }

    private void Animate()
    {
        if(input != Vector2.zero)
        {
            isMoving = true;
            animator.SetFloat("moveX", input.x);
            animator.SetFloat("moveY", input.y);
        }
        else
        {
            isMoving = false;
        }

        animator.SetBool("isMoving", isMoving);
    }
}
