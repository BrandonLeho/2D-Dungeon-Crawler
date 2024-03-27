using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentSounds : AudioPlayer
{
    [SerializeField] private AudioClip 
    hitClip = null, deathClip = null, voiceLineClip = null,  parryClip = null, blockClip = null, attackClip = null, dashClip = null;

    public void PlayHitSound()
    {
        PlayClipWithVariablePitch(hitClip);
    }

    public void PlayDeathSound()
    {
        PlayClip(deathClip);
    }

    public void PlayVoiceLineSound()
    {
        PlayClip(voiceLineClip);
    }

    public void PlayParrySound()
    {
        PlayClipWithVariablePitch(parryClip);
    }

    public void PlayBlockSound()
    {
        PlayClipWithVariablePitch(blockClip);
    }

    public void PlayAttackSound()
    {
        PlayClipWithVariablePitch(attackClip);
    }

    public void PlayDashSound()
    {
        PlayClipWithVariablePitch(dashClip);
    }
}
