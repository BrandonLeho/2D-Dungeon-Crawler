using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.Events;
using Unity.VisualScripting;

public class KatanaParent : MonoBehaviour
{
    public SpriteRenderer characterRenderer, weaponRenderer;
    public Vector2 PointerPosition { get; set; }
    public float delay = 0.3f;
    public Animator animator;
    private AnimationEventHelper animationEventHelper;
    
    public bool IsAttacking { get; private set; }
    public Transform circleOrigin;

    [SerializeField]
    public float radius;
    [SerializeField] private Parry parry;
    [SerializeField] private AgentMover agentMover;
    [SerializeField] private float lungeDistance = 20f;
    [SerializeField] private float lungeSpeed = 100f;
    
    public bool canAttack, chainAttack, canLunge;
    public int attackState = 0, damage = 50, staminaDamage = 75;


    private void Update() {
        if(IsAttacking)
            return;
        Vector2 direction = (PointerPosition-(Vector2)transform.position).normalized;
        transform.right = direction;

        Vector2 scale = transform.localScale;
        if(direction.x < 0)
            scale.y = -1;
        else if(direction.x > 0)
            scale.y = 1;
        transform.localScale = scale;

        if(transform.eulerAngles.z > 90 && transform.eulerAngles.z < 270)
            weaponRenderer.sortingOrder = characterRenderer.sortingOrder + 1;
        else
            weaponRenderer.sortingOrder = characterRenderer.sortingOrder - 1;

        if(attackState >= 4)
        {
            attackState = 0;
            animator.SetInteger("AttackState", attackState);
        }
        if(parry != null && parry.GetParryState() && !parry.GetBlockState())
        {
            animator.SetBool("Parry", true);
            canLunge = false;
            chainAttack = false;
            canAttack = false;
        }
        else
        {
            animator.SetBool("Parry", false);
        }
            

        if(parry != null && parry.GetBlockState())
        {
            animator.SetBool("Block", true);
            canLunge = false;
            chainAttack = false;
            
            Stop();
        }     
        else
        {
            animator.SetBool("Block", false);
        }
            
    }

    

    public void Attack()
    {
        //Debug.Log(attackState);
        //Debug.Log(canAttack);
        if(canAttack && !(parry != null && parry.GetBlockState()) && !(parry != null && parry.GetParryState() && parry.GetBlockState()))
        {
            if(attackState < 4)
            {
                if(attackState == 3)
                {
                    lungeDistance = 1f;
                }
                else
                {
                    lungeDistance = 0.5f;
                }
                if(canLunge)
                {
                    Vector2 direction = (PointerPosition - (Vector2)transform.position).normalized;
                    //Debug.Log(direction);
                    agentMover.Lunge(direction, lungeDistance, lungeSpeed); // Lunge towards the pointer position
                }

                attackState++;
                canAttack = false;
                IsAttacking = true;
                animator.SetInteger("AttackState", attackState);
            }

        }
        else
        {
            canLunge = false;
            chainAttack = false;
            Stop();
        }
    }

    public void Next()
    {
        canAttack = true;
        IsAttacking = false;
        if (Input.GetMouseButtonUp(0))
        {
            chainAttack = true;
            
        }
    }

    public void Stop()
    {
        IsAttacking = false;
        if(chainAttack)
        {
            Attack();
        }
        else
        {
            StartCoroutine(DelayAttack());
            canAttack = true;
            attackState = 0;
            animator.SetInteger("AttackState", attackState);
            
        }
    }

     private IEnumerator DelayAttack()
    {
        yield return new WaitForSeconds(delay);
        canLunge = true;
    }
    
    
    public void ResetIsAttacking()
    {
        IsAttacking = false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Vector3 position = circleOrigin == null ? Vector3.zero : circleOrigin.position;
        Gizmos.DrawWireSphere(position, radius);
    }

    public void DetectColliders()
    {
        foreach (Collider2D collider in Physics2D.OverlapCircleAll(circleOrigin.position,radius))
        {
            //Debug.Log(collider.name);
            Health health;
            Stamina stamina;
            if(health = collider.GetComponent<Health>())
            {
                health.GetHit(damage, transform.parent.gameObject);

                if(stamina = collider.GetComponent<Stamina>())
                {
                        stamina.damageStamina(staminaDamage, transform.parent.gameObject);
                }
                    
            }
        }
        //FindAnyObjectByType<HitLag>().Stop(1f);
    }
}
