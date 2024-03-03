using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.Events;

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

    [SerializeField] private AgentMover agentMover;
    [SerializeField] private float lungeDistance = 20f;
    [SerializeField] private float lungeSpeed = 100f;
    
    public bool canAttack;
    public int attackState = 0;
    public bool chainAttack;


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
    }

    

    public void Attack()
    {
        //Debug.Log(attackState);
        //Debug.Log(canAttack);
        if(canAttack)
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
                Vector2 direction = (PointerPosition - (Vector2)transform.position).normalized;
                //Debug.Log(direction);
                agentMover.Lunge(direction, lungeDistance, lungeSpeed); // Lunge towards the pointer position

                attackState++;
                canAttack = false;
                IsAttacking = true;
                animator.SetInteger("AttackState", attackState);
            }

        }
        else
        {
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
            canAttack = true;
            StartCoroutine(DelayAttack());
            attackState = 0;
            animator.SetInteger("AttackState", attackState);
            
        }
    }

     private IEnumerator DelayAttack()
    {
        yield return new WaitForSeconds(delay);
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
            if(health = collider.GetComponent<Health>())
            {
                health.GetHit(UnityEngine.Random.Range(10, 50), transform.parent.gameObject);
            }
        }
    }
}
