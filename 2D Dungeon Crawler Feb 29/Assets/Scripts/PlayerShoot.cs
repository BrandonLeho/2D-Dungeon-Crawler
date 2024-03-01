using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting.ReorderableList;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    //Ranged
    public GameObject bulletPrefab;
    public float bulletSpeed = 50f;

    //Melee
    public GameObject Melee;
    public bool isAttacking = false;
    float atkDuration = 1f;
    float atkTimer = 0f;
    public Transform Aim;

    // Update is called once per frame
    void Update()
    {
        CheckMeleeTimer();

        if(Input.GetMouseButton(0)) //Left Click
        {
            OnAttack();
        }
        if(Input.GetMouseButtonDown(1)) //Right Click
        {
            Shoot();
        }
    }

    void Shoot()
    {
        //Get mouse position
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        
        //Direction from us to mouse
        Vector3 shootDirection = (mousePosition - transform.position).normalized;

        GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(shootDirection.x, shootDirection.y) * bulletSpeed;
        Destroy(bullet, 2f);
    }

    void OnAttack()
    {
        if(!isAttacking)
        {
            Melee.SetActive(true);
            isAttacking = true;

            //Get mouse position
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        
            //Direction from us to mouse
            Vector3 shootDirection = (mousePosition - transform.position).normalized;

            Aim.rotation = Quaternion.LookRotation(Vector3.forward, -shootDirection);
        }
    }

    void CheckMeleeTimer()
    {
        if(isAttacking)
        {
            atkTimer += Time.deltaTime;
            if(atkTimer >= atkDuration)
            {
                atkTimer = 0;
                isAttacking = false;
                Melee.SetActive(false);
            }
        }
    }
}
