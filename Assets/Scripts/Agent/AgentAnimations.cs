using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentAnimations : MonoBehaviour
{
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void RotateToPointer(Vector2 lookDirection)
    {
        animator.SetFloat("moveX", lookDirection.x);
        //animator.SetFloat("moveY", lookDirection.y);
    }

    public void PlayAnimation(Vector2 movementInput)
    {
        animator.SetBool("isMoving", movementInput.magnitude > 0);
    }

}