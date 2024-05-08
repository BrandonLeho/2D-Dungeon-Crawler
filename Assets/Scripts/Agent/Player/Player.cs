using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour, IHittable
{
    [field: SerializeField] public int Health { get; set; }
    [field: SerializeField] public int maxHealth { get; set; }
    [field: SerializeField] public int Stamina { get; set; }
    [field: SerializeField] public int Mana { get; set; }
    [field: SerializeField] public int ManaRegenOnMelee = 0;
    [field: SerializeField] public int goldTotal = 0;
    [field: SerializeField] public UnityEvent OnDie { get; set; }
    [field: SerializeField] public UnityEvent OnGetHit { get; set; }
    private bool dead = false;
    private AK47Parent ak47;

    Parry parry;
    Health health;
    Stamina stamina;
    Mana mana;
    GoldManager gold;

    private void Awake()
    {
        ak47 = GetComponentInChildren<AK47Parent>();
    }

    private void Start()
    {
        parry = gameObject.GetComponent<Parry>();

        health = gameObject.GetComponent<Health>();
        health.InitializeHealth(Health);

        stamina = gameObject.GetComponent<Stamina>();
        stamina.InitializeStamina(Stamina);

        mana = gameObject.GetComponent<Mana>();
        mana.InitializeMana(Mana);

        gold = gameObject.GetComponent<GoldManager>();
    }

    public void GetHit(int damage, int staminaDamage, GameObject damageDealer)
    {
        if (dead == false)
        {
            if (parry.GetParryState())
            {
                if (damageDealer.GetComponent<Stamina>() != null)
                    damageDealer.GetComponent<Stamina>().UseStamina(150);
                return;
            }
            else if (parry.GetBlockState())
            {
                damage /= 2;
            }

            if (gameObject.GetComponent<Stamina>().GetCurrentStamina() <= 0)
                damage *= 2;

            Health -= damage;
            OnGetHit?.Invoke();
            if (health = gameObject.GetComponent<Health>())
            {
                health.GetHit(damage, damageDealer);
            }

            if (stamina = gameObject.GetComponent<Stamina>())
            {
                stamina.damageStamina(staminaDamage, damageDealer);
            }

            if (Health <= 0)
            {
                OnDie?.Invoke();
                Destroy(gameObject);
            }
        }
    }

    public void RegenManaOnMelee()
    {
        if (mana = gameObject.GetComponent<Mana>())
        {
            mana.RestoreMana(ManaRegenOnMelee);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Resource"))
        {
            var resource = collision.gameObject.GetComponent<Resource>();
            if (resource != null)
            {
                switch (resource.ResourceData.ResourceType)
                {
                    case ResourceTypeEnum.Health:
                        if (Health >= maxHealth)
                        {
                            return;
                        }
                        int heal = resource.ResourceData.GetAmount();
                        Health += heal;
                        health.Heal(heal);
                        if (Health > maxHealth)
                        {
                            Health = maxHealth;
                        }
                        resource.PickUpResource();
                        break;
                    case ResourceTypeEnum.Ammo:
                        if (ak47.AmmoFull)
                        {
                            return;
                        }
                        ak47.AddAmmo(resource.ResourceData.GetAmount());
                        resource.PickUpResource();
                        break;
                    case ResourceTypeEnum.Gold:
                        int goldAmount = resource.ResourceData.GetAmount();
                        goldTotal += goldAmount;
                        gold.AddGold(goldTotal);
                        resource.PickUpResource();
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
