using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Enemy : MonoBehaviour, IHittable
{

    [field: SerializeField] public EnemyDataSO EnemyData { get; set; }
    [field: SerializeField] public int Health { get; private set; } = 1000;
    [field: SerializeField] public UnityEvent OnGetHit { get; set; }
    [field: SerializeField] public UnityEvent OnDie { get; set; }

    Health health;
    private void Start()
    {
        Health = EnemyData.MaxHealth;
        health = gameObject.GetComponent<Health>();
        health.InitializeHealth(Health);
    }
    public void GetHit(int damage, GameObject damageDealer)
    {
        Health -= damage;
        OnGetHit?.Invoke();
        if(health = gameObject.GetComponent<Health>())
        {
            health.GetHit(damage, gameObject, false);
        }

        if(Health <= 0)
        {
            OnDie?.Invoke();
        }
    }

    public void Die()
    {
        Destroy(gameObject);
    }
}
