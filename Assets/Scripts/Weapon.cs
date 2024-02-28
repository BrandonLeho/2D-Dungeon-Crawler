using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public float damage = 1;
    public enum WeaponType {Melee, Bullet}
    public WeaponType weaponType;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        EnemyController enemy = collision.GetComponent<EnemyController>();

        if(enemy != null)
        {
            enemy.TakeDamage(damage);

            //Destroys bullet on hit
            //TODO make bullet pass thru enemies for collateral weapons
            if(weaponType == WeaponType.Bullet)
            {
                Destroy(gameObject);
            }
        }

        // Check if the object collided with is tagged as "Wall"
        if (collision.gameObject.CompareTag("Wall") && weaponType == WeaponType.Bullet)
        {
            // Destroy the bullet gameObject
            Destroy(gameObject);
        }
    }
}
