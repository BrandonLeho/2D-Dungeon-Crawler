using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class RegularBullet : Bullet
{
    protected Rigidbody2D rb2d;

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
        if(collision.gameObject.layer == LayerMask.NameToLayer("SolidObjects"))
        {
            HitObsticle();
        }
        Destroy(gameObject);
    }

    private void HitObsticle()
    {
        Debug.Log("Hitting Obsticle");
    }
}


