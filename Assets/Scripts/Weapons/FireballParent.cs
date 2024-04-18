using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

public class FireballParent : MonoBehaviour
{
    protected float desiredAngle;

    public SpriteRenderer characterRenderer, weaponRenderer;
    public Vector2 PointerPosition { get; set; }

    [SerializeField] protected GameObject muzzle;
    [SerializeField] protected SpellDataSO spellData;
    protected bool isShooting = false;
    [SerializeField] protected bool reloadCoroutine = false;
    private Mana mana;
    [field: SerializeField] public UnityEvent OnShoot { get; set; }
    [field: SerializeField] public UnityEvent OnShootNoMana { get; set; }

    private void Start()
    {
        mana = GetComponentInParent<Mana>();
    }

    private void OnDisable()
    {
        reloadCoroutine = false;
        StopAllCoroutines();
        StopShoot();
        gameObject.GetComponentInChildren<FeedbackPlayer>().FinishFeedback();
    }

    public void Shoot()
    {
        isShooting = true;
    }

    public void StopShoot()
    {
        isShooting = false;
    }

    private void UseWeapon()
    {
        if(isShooting && reloadCoroutine == false)
        {
            if(mana.GetMana() > 0 && spellData.Mana <= mana.GetMana())
            {
                mana.DrainMana(spellData.Mana);
                OnShoot?.Invoke();
                for(int i = 0; i < spellData.GetBulletCountToSpawn(); i++)
                {
                    ShootBullet();
                }
                Recoil();
            }
            else
            {
                isShooting = false;
                OnShootNoMana?.Invoke();
                return;
            }
            FinishShooting();
        }
    }

    private void Recoil()
    {
        if(spellData.Recoil == 0)
            return;
        
        var knockback = transform.root.gameObject.GetComponent<Knockback>();
        if(knockback != null)
        {
            knockback.PlayFeedbackV2(spellData.Recoil, PointerPosition); 
        }
    }

    private void FinishShooting()
    {
        StartCoroutine(DelayNextShootCoroutine());
        if(spellData.AutomaticFire == false)
        {
            isShooting = false;
        }
    }

    protected IEnumerator DelayNextShootCoroutine()
    {
        reloadCoroutine = true;
        yield return new WaitForSeconds(spellData.WeaponDelay);
        reloadCoroutine = false;
    }

    private void ShootBullet()
    {
        SpawnBullet(muzzle.transform.position, CalculateAngle(muzzle));
    }

    private void SpawnBullet(Vector3 position, Quaternion rotation)
    {
        var bulletPrefab = Instantiate(spellData.BulletData.bulletPrefab, position, rotation);
        bulletPrefab.GetComponent<Bullet>().BulletData = spellData.BulletData;
    }

    private Quaternion CalculateAngle(GameObject muzzle)
    {
        float spread = Random.Range(-spellData.SpreadAngle, spellData.SpreadAngle);
        Quaternion bulletSpreadRotation = Quaternion.Euler(new Vector3(0, 0, spread));
        return muzzle.transform.rotation * bulletSpreadRotation;
    }

    private void Update()
    {
        UseWeapon();

        Vector2 direction = (PointerPosition-(Vector2)transform.position).normalized;
        transform.right = direction;

        Vector2 scale = transform.localScale;
        if(direction.x < 0)
            scale.y = -1;
        else if(direction.x > 0)
            scale.y = 1;
        transform.localScale = scale;

        if(transform.eulerAngles.z > 90 && transform.eulerAngles.z < 270)
            weaponRenderer.sortingOrder = characterRenderer.sortingOrder + 1;
        else
            weaponRenderer.sortingOrder = characterRenderer.sortingOrder - 1;
    }
}
