using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Agent : MonoBehaviour
{
    private AgentAnimations agentAnimations;
    private AgentMover agentMover;
    private Parry parry;

    private Vector2 pointerInput, movementInput;

    public Vector2 PointerInput { get => pointerInput; set => pointerInput = value; }
    public Vector2 MovementInput { get => movementInput; set => movementInput = value; }

    private SwordParent swordParent;
    private KatanaParent katanaParent;
    private AK47Parent aK47Parent;
    private ShotgunParent shotgunParent;
    private RPGParent rpgParent;
    private SniperParent sniperParent;

    public void PerformAttack()
    {
        if(swordParent.gameObject.activeInHierarchy)
            swordParent.Attack();
        if(katanaParent.gameObject.activeInHierarchy)
            katanaParent.Attack();
        
    }

    public void PerformFire()
    {
        if(aK47Parent.gameObject.activeInHierarchy)
        {
            aK47Parent.Shoot();
            agentMover.canDashOnShoot();
        }
        if(shotgunParent.gameObject.activeInHierarchy)
        {
            shotgunParent.Shoot();
            agentMover.canDashOnShoot();
        }
        if(rpgParent.gameObject.activeInHierarchy)
        {
            rpgParent.Shoot();
            agentMover.canDashOnShoot();
        }
        if(sniperParent.gameObject.activeInHierarchy)
        {
            sniperParent.Shoot();
            agentMover.canDashOnShoot();
        }
    }

    public void PerformedFire()
    {
        if(aK47Parent.gameObject.activeInHierarchy)
        {
            aK47Parent.StopShoot();
            agentMover.canDashOffShoot();
        }
        if(shotgunParent.gameObject.activeInHierarchy)
        {
            shotgunParent.StopShoot();
            agentMover.canDashOffShoot();
        }
        if(rpgParent.gameObject.activeInHierarchy)
        {
            rpgParent.StopShoot();
            agentMover.canDashOffShoot();
        }
        if(sniperParent.gameObject.activeInHierarchy)
        {
            sniperParent.StopShoot();
            agentMover.canDashOffShoot();
        }
    }

    public void PerformDash()
    {
        agentMover.Dash();
    }

    public void PerformParry()
    {
        if(!aK47Parent.gameObject.activeInHierarchy)
        {
            parry.StartBlockAndParry();
        }
        
    }

    public void PerformedParry()
    {
        parry.EndBlockAndParry();
    }

    private void Update()
    {
        agentMover.MovementInput = movementInput;
        swordParent.PointerPosition = pointerInput;
        katanaParent.PointerPosition = pointerInput;
        aK47Parent.PointerPosition = pointerInput;
        shotgunParent.PointerPosition = pointerInput;
        rpgParent.PointerPosition = pointerInput;
        sniperParent.PointerPosition = pointerInput;

        AnimateCharacter();
    }

    private void Awake()
    {
        agentAnimations = GetComponentInChildren<AgentAnimations>();
        swordParent = GetComponentInChildren<SwordParent>();
        katanaParent = GetComponentInChildren<KatanaParent>();
        aK47Parent = GetComponentInChildren<AK47Parent>();
        shotgunParent = GetComponentInChildren<ShotgunParent>();
        rpgParent = GetComponentInChildren<RPGParent>();
        sniperParent = GetComponentInChildren<SniperParent>();
        
        agentMover = GetComponent<AgentMover>();
        parry = GetComponent<Parry>();
    }

    private void AnimateCharacter()
    {
        Vector2 lookDirection = pointerInput - (Vector2)transform.position;
        agentAnimations.RotateToPointer(lookDirection);
        agentAnimations.PlayAnimation(movementInput);
    }
}