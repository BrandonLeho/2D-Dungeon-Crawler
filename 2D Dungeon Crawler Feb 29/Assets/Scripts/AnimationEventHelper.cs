using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AnimationEventHelper : MonoBehaviour
{
    public bool canAttack;
    public int attackState = 0;
    public Animator animator;
    public UnityEvent OnAnimationEventTriggered, OnAttackPerformed;

    public void AttackCombo()
    {
        if(canAttack)
        {
            if(attackState < 3)
            {
                attackState++;
                canAttack = false;
                animator.SetInteger("AttackState", attackState);
            }
        }
    }

    public void Next()
    {
        OnAnimationEventTriggered?.Invoke();
        canAttack = true;
    }

    public void Stop()
    {
        OnAttackPerformed?.Invoke();
        attackState = 0;
        canAttack = true;
    }
}
