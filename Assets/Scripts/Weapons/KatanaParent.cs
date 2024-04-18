using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.Events;
using Unity.VisualScripting;
using Random = UnityEngine.Random;

public class KatanaParent : MonoBehaviour
{
    [SerializeField] protected MeleeData meleeData;
    public SpriteRenderer characterRenderer, weaponRenderer;
    public Vector2 PointerPosition { get; set; }
    public Animator animator;
    public bool IsAttacking { get; private set; }
    public Transform circleOrigin;
    [SerializeField] private Parry parry;
    [SerializeField] private AgentMover agentMover;
    [field: SerializeField] public UnityEvent OnHit { get; set; }
    
    public bool canAttack, chainAttack, canLunge;
    public int attackState = 0;

    private Knockback knockback;


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
                    meleeData.LungeDistance = 1f;
                    meleeData.KnockbackPower = 200f;
                }
                else
                {
                    meleeData.LungeDistance = 0.5f;
                    meleeData.KnockbackPower = 100f;
                }
                if(canLunge)
                {
                    Vector2 direction = (PointerPosition - (Vector2)transform.position).normalized;
                    //Debug.Log(direction);
                    agentMover.Lunge(direction, meleeData.LungeDistance, meleeData.LungeSpeed); // Lunge towards the pointer position
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
            if(gameObject.activeInHierarchy == true)
            {
                StartCoroutine(DelayAttack());
                canAttack = true;
                attackState = 0;
                animator.SetInteger("AttackState", attackState);
            }
        }
    }

     private IEnumerator DelayAttack()
    {
        yield return new WaitForSeconds(meleeData.AttackDelay);
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
        Gizmos.DrawWireSphere(position, meleeData.MeleeHitRadius);
    }

    public void DetectColliders()
    {
        foreach (Collider2D collider in Physics2D.OverlapCircleAll(circleOrigin.position,meleeData.MeleeHitRadius))
        {
            if(collider.name != transform.root.gameObject.name)
            {
                OnHit?.Invoke();
                var hittable = collider.GetComponent<IHittable>();
                hittable?.GetHit(meleeData.Damage, meleeData.StaminaDamage, transform.root.gameObject);

                if(knockback = collider.GetComponent<Knockback>())
                {
                    knockback.PlayFeedback(meleeData.KnockbackPower, transform.root.gameObject); 
                }
            }
        }
    }
}
