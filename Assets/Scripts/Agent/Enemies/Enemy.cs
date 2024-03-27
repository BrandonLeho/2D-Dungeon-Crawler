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

    private void Start()
    {
        Health = EnemyData.MaxHealth;
    }
    public void GetHit(int damage, GameObject damageDealer)
    {
        Health -= damage;
        OnGetHit?.Invoke();
        Health health;
        if(health = gameObject.GetComponent<Health>())
        {
            health.GetHit(damage, gameObject);
        }

        if(Health <= 0)
        {
            OnDie?.Invoke();
            StartCoroutine(WaitToDie());
        }
    }

    IEnumerator WaitToDie()
    {
        yield return new WaitForSeconds(.35f);
        Destroy(gameObject);
    }
}
