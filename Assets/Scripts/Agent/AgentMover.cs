using System;
using System.Collections;
using UnityEngine;

public class AgentMover : MonoBehaviour
{
    private Rigidbody2D rb2d;

    [SerializeField]
    private float maxSpeed, acceleration, deacceleration, finalMaxSpeed, shootingSpeed;
    [SerializeField]
    private float currentSpeed = 0;
    private Vector2 oldMovementInput;
    public Vector2 MovementInput { get; set; }

    public float dashSpeed;
    public float dashLength = 0.5f, dashCooldown = 1f;
    public float dashCounter;
    public float dashCoolCounter;
    public bool isDashing, canDash, isLunging, isBlocking;

    public float lungeDistance, lungeSpeed, lungeTime, startTime;
    private Vector2 direction;

    private Stamina stamina;

    protected bool isKnockedBack = false;

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        finalMaxSpeed = maxSpeed;
        stamina = GetComponent<Stamina>();
        canDash = true;
    }

    private void FixedUpdate()
    {
        if(isDashing)
        {
            if(dashCounter > 0)
            {
                StartCoroutine(Dashing());
                /* dashCounter -= Time.deltaTime;
                rb2d.velocity = oldMovementInput * currentSpeed;

                if(dashCounter <= 0)
                {
                    currentSpeed = maxSpeed;
                    dashCoolCounter = dashCooldown;
                    isDashing = false;
                } */
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
        if(!isBlocking && canDash)
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

    IEnumerator Dashing()
    {
        while(dashCounter > 0)
        {
            dashCounter -= Time.deltaTime;
            rb2d.velocity = oldMovementInput * currentSpeed;
            if(dashCounter <= 0)
            {
                currentSpeed = maxSpeed;
                dashCoolCounter = dashCooldown;
                isDashing = false;
            }
            yield return null;
        }
    }

    public void canDashOnShoot()
    {
        canDash = false;
        maxSpeed = shootingSpeed;
    }

    public void canDashOffShoot()
    {
        canDash = true;
        maxSpeed = finalMaxSpeed;
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

    public void KnockBack(Vector2 direction, float power, float duration)
    {
        if(isKnockedBack == false)
        {
            isKnockedBack = true;
            StartCoroutine(KnockBackCoroutine(direction, power, duration));
        }
    }

    public void ResetKnockBack()
    {
        StopAllCoroutines();
        StopCoroutine("KnockBackCoroutine");
        ResetKnockBackParameters();
    }

    IEnumerator KnockBackCoroutine(Vector2 direction, float power, float duration)
    {
        rb2d.AddForce(direction.normalized * power, ForceMode2D.Impulse);
        yield return new WaitForSeconds(duration);
        ResetKnockBackParameters();
    }

    private void ResetKnockBackParameters()
    {
        currentSpeed = 0;
        rb2d.velocity = Vector2.zero;
        isKnockedBack = false;
    }
}
