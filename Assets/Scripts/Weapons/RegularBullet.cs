using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Animations;

public class RegularBullet : Bullet
{
    protected Rigidbody2D rb2d;
    private bool isDead = false;

    private Knockback Knockback;

    public override BulletDataSO BulletData { 
        get => base.BulletData; 
        set 
        {
            base.BulletData = value;
            rb2d = GetComponent<Rigidbody2D>();
            rb2d.drag = BulletData.Friction;

        }
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
        if(isDead)
            return;
        isDead = true;
        var hittable = collision.GetComponent<IHittable>();
        hittable?.GetHit(BulletData.Damage, BulletData.StaminaDamage, gameObject);

        if(collision.gameObject.layer == LayerMask.NameToLayer("SolidObjects"))
        {
            HitObsticle(collision);
        }
        if(collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            HitEnemy(collision);
        }
        Destroy(gameObject);
    }

    private void HitObsticle(Collider2D collision)
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.right, 1, BulletData.BulletLayerMask);
        if(hit.collider!= null)
        {
            Instantiate(BulletData.ImpactObsticlePrefab, hit.point, Quaternion.identity);
        }
    }

    private void HitEnemy(Collider2D collision)
    {
        var knockback = collision.GetComponent<Knockback>();
        if(knockback != null)
        {
            knockback.PlayFeedback(BulletData.KnockbackPower, transform.root.gameObject); 
        }
        Vector2 randomOffset = Random.insideUnitCircle * 0.5f;
        Instantiate(BulletData.ImpactEnemyPrefab, collision.transform.position + (Vector3)randomOffset, Quaternion.identity);
    }
}


