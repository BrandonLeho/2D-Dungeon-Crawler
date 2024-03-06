using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentMover : MonoBehaviour
{
    private Rigidbody2D rb2d;

    [SerializeField]
    private float maxSpeed, acceleration, deacceleration, finalMaxSpeed;
    [SerializeField]
    private float currentSpeed = 0;
    private Vector2 oldMovementInput;
    public Vector2 MovementInput { get; set; }

    public float dashSpeed;
    public float dashLength = 0.5f, dashCooldown = 1f;
    public float dashCounter;
    public float dashCoolCounter;
    public bool isDashing, isLunging, isBlocking;

    public float lungeDistance, lungeSpeed, lungeTime, startTime;
    private Vector2 direction;

    private Stamina stamina;

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        finalMaxSpeed = maxSpeed;
        stamina = GetComponent<Stamina>();
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
        else if(isLunging)
        {

            lungeTime = lungeDistance / lungeSpeed;
            startTime += Time.deltaTime;

            if(startTime <= lungeTime)
            {
                rb2d.velocity = direction.normalized * lungeSpeed;
            }
            else
            {
                isLunging = false;
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
            startTime = 0;
        }        
    }

    public void Dash()
    {
        if(!isBlocking)
        {
            if(dashCoolCounter <= 0 && dashCounter <= 0 && stamina.GetCurrentStamina() > 0)
            {
                currentSpeed = dashSpeed;
                dashCounter = dashLength;
                isDashing = true;
                stamina.UseStamina(150);
            }
        }
        
    }

    public void Blocking(bool playerIsBlocking, bool playerIsParrying)
    {
        if(!playerIsBlocking && playerIsParrying)
        {
            //Debug.Log("Parry");
            isBlocking = true;
            maxSpeed = finalMaxSpeed/5;
        }
        else if(playerIsBlocking && !playerIsParrying)
        {
            //Debug.Log("Block");
            isBlocking = true;
            //maxSpeed = finalMaxSpeed/3;       
        }
        else
        {
            //Debug.Log("Normal");
            isBlocking = false;
            maxSpeed = finalMaxSpeed;
        }
        //Debug.Log(maxSpeed);
    }

    public void Lunge(Vector2 direction, float lungeDistance, float lungeSpeed)
    {
        this.direction = direction;
        this.lungeDistance = lungeDistance;
        this.lungeSpeed = lungeSpeed;
        isLunging = true;
    }
}
