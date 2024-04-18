using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.Events;
using Random = UnityEngine.Random;

public class RegularBullet : Bullet
{
    protected Rigidbody2D rb2d;
    private bool isDestroyed = false;
    private Knockback Knockback;
    private new Collider2D collider;
    private int pierceCount = 0;
    public override BulletDataSO BulletData { 
        get => base.BulletData; 
        set 
        {
            base.BulletData = value;
            rb2d = GetComponent<Rigidbody2D>();
            rb2d.drag = BulletData.Friction;

        }
    }

    private void Awake()
    {
        collider = GetComponent<Collider2D>();
    }

    private void FixedUpdate()
    {
        if(rb2d != null && BulletData != null)
        {
            rb2d.MovePosition(transform.position + BulletData.BulletSpeed * transform.right * Time.fixedDeltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(isDestroyed)
            return;
        
        if(collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            HitEnemy(collision);
        }
        if(collision.gameObject.layer == LayerMask.NameToLayer("SolidObjects"))
        {
            HitObsticle(collision);
        }

        if(!BulletData.Pierce)
        {
            Destroy(gameObject);
            isDestroyed = true;
        }
        pierceCount++;
    }

    private void HitObsticle(Collider2D collision)
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.right, 1, BulletData.BulletLayerMask);
        if(hit.collider != null)
        {
            Instantiate(BulletData.ImpactObsticlePrefab, hit.point, Quaternion.identity);
            if(BulletData.Explosion)
            {
                Explosion(collision);
            }
            else
            {
                var hittable = collision.GetComponent<IHittable>();
                hittable?.GetHit(BulletData.Damage, BulletData.StaminaDamage, gameObject);
            }
        }
        Destroy(gameObject);
        isDestroyed = true;
    }

    private void HitEnemy(Collider2D collision)
    {
        if(BulletData.Explosion)
        {
            Explosion(collision);
        }
        else
        {
            var hittable = collision.GetComponent<IHittable>();
            hittable?.GetHit((int)(BulletData.Damage * (1 - (0.05f * pierceCount))), (int)(BulletData.StaminaDamage * (1 - (0.05f * pierceCount))), gameObject);
            var knockback = collision.GetComponent<Knockback>();
            if(knockback != null)
            {
                knockback.PlayFeedback(BulletData.KnockbackPower, transform.root.gameObject); 
            }
        }
        Vector2 randomOffset = Random.insideUnitCircle * 0.5f;
        Instantiate(BulletData.ImpactEnemyPrefab, collision.transform.position + (Vector3)randomOffset, Quaternion.identity);
    }

    private void Explosion(Collider2D directCollision)
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, BulletData.ExplosionRadius, BulletData.BulletLayerMask);
        foreach(var collider in colliders)
        {
            float dist = Vector3.Distance(collider.transform.position, transform.position);
            var hittable = collider.GetComponent<IHittable>();

            int adjustedDamage = (int)(-(BulletData.Damage / BulletData.ExplosionRadius / BulletData.ExplosionRadius) * Math.Pow(dist, 2)) + BulletData.Damage;
            int adjustedStaminaDamage = (int)(-(BulletData.StaminaDamage / BulletData.ExplosionRadius / BulletData.ExplosionRadius) * Math.Pow(dist, 2)) + BulletData.StaminaDamage;

            int adjustedKnockbackPower = (int)((-(BulletData.KnockbackPower / BulletData.ExplosionRadius / BulletData.ExplosionRadius) * Math.Pow(dist, 2)) + BulletData.KnockbackPower);

            if(directCollision.gameObject == collider.gameObject)
                adjustedDamage = (int)(adjustedDamage * 1.5f);

            hittable?.GetHit(adjustedDamage, adjustedStaminaDamage, gameObject);
            
            var knockback = collider.GetComponent<Knockback>();
            if(knockback != null)
            {
                knockback.PlayFeedback(adjustedKnockbackPower, gameObject);
            }
        }
    }
}


