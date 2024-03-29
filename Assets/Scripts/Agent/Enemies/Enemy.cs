using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Enemy : MonoBehaviour, IHittable
{

    [field: SerializeField] public EnemyDataSO EnemyData { get; set; }
    [field: SerializeField] public int Health { get; private set; } = 1000;
    [field: SerializeField] public int Stamina { get; private set; } = 1000;
    [field: SerializeField] public UnityEvent OnGetHit { get; set; }
    [field: SerializeField] public UnityEvent OnDie { get; set; }

    Health health;
    Stamina stamina;
    private void Start()
    {
        Health = EnemyData.MaxHealth;
        health = gameObject.GetComponent<Health>();
        health.InitializeHealth(Health);

        Stamina = EnemyData.MaxStamina;
        stamina = gameObject.GetComponent<Stamina>();
        stamina.InitializeStamina(Stamina);
    }
    public void GetHit(int damage, int staminaDamage, GameObject damageDealer)
    {
        if(gameObject.GetComponent<Stamina>().GetCurrentStamina() <= 0)
                damage *= 2;
        
        Health -= damage;
        OnGetHit?.Invoke();
        if(health = gameObject.GetComponent<Health>())
        {
            health.GetHit(damage, damageDealer);
        }

        if(stamina = gameObject.GetComponent<Stamina>())
        {
            stamina.damageStamina(staminaDamage, damageDealer);
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
