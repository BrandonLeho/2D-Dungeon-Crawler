using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentMover : MonoBehaviour
{
    private Rigidbody2D rb2d;

    [SerializeField]
    private float maxSpeed = 2, acceleration = 50, deacceleration = 100;
    [SerializeField]
    private float currentSpeed = 0;
    private Vector2 oldMovementInput;
    public Vector2 MovementInput { get; set; }

    public float dashSpeed;
    public float dashLength = 0.5f, dashCooldown = 1f;
    public float dashCounter;
    public float dashCoolCounter;
    public bool isDashing;

    private void Start()
    {

    }

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if(isDashing)
        {
            if(dashCounter > 0)
            {
                dashCounter -= Time.deltaTime;
                 rb2d.velocity = oldMovementInput * currentSpeed;

                if(dashCounter <= 0)
                {
                    currentSpeed = maxSpeed;
                    dashCoolCounter = dashCooldown;
                    isDashing = false;
                }
                
            }

            
        }
        else
        {
            if (MovementInput.magnitude > 0 && currentSpeed >= 0)
            {
                oldMovementInput = MovementInput;
                currentSpeed += acceleration * maxSpeed * Time.deltaTime;
            }
            else
            {
                currentSpeed -= deacceleration * maxSpeed * Time.deltaTime;
            }
            currentSpeed = Mathf.Clamp(currentSpeed, 0, maxSpeed);
            rb2d.velocity = oldMovementInput * currentSpeed;
            
            if(dashCoolCounter > 0)
            {
                dashCoolCounter -= Time.deltaTime;
            }
        }
        

        
    }

    public void Dash()
    {
        if(dashCoolCounter <= 0 && dashCounter <= 0)
        {
            currentSpeed = dashSpeed;
            dashCounter = dashLength;
            isDashing = true;
        }
    }

    public void Lunge(Vector2 direction, float lungeDistance, float lungeSpeed)
    {
        StartCoroutine(LungeCoroutine(direction, lungeDistance, lungeSpeed));
    }

    private IEnumerator LungeCoroutine(Vector2 direction, float lungeDistance, float lungeSpeed)
    {
        //Debug.Log("Lunge");
        float startTime = Time.time;
        // Calculate lunge time based on distance and speed
        float lungeTime = lungeDistance / lungeSpeed;

        while (Time.time < startTime + lungeTime)
        {
            rb2d.velocity = direction.normalized * lungeSpeed;
            yield return null;
        }

        // Optionally, reset velocity to 0 or keep momentum
        rb2d.velocity = Vector2.zero;
    }


}
