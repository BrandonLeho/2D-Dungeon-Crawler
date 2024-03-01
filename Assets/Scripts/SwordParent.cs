using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.Events;

public class SwordParent : MonoBehaviour
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


    private void Update() {
        if(IsAttacking)
            return;
        Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        difference.Normalize();
        float rotation_z = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rotation_z);

        Vector2 scale = transform.localScale;
        if(difference.x < 0)
            scale.y = -1;
        else if(difference.x > 0)
            scale.y = 1;
        transform.localScale = scale;

        if(transform.eulerAngles.z > 0 && transform.eulerAngles.z < 180)
            weaponRenderer.sortingOrder = characterRenderer.sortingOrder - 1;
        else
            weaponRenderer.sortingOrder = characterRenderer.sortingOrder + 1;
    }

    public bool canAttack;
    public int attackState = 0;
    public bool chainAttack;

    public void Attack()
    {
        //Debug.Log(attackState);
        //Debug.Log(canAttack);
        if(canAttack)
        {
            if(attackState < 3)
            {
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
            chainAttack = false;
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
                health.GetHit(1, transform.parent.gameObject);
            }
        }
    }
}

