using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SniperParent : MonoBehaviour
{
    protected float desiredAngle;

    public SpriteRenderer characterRenderer, weaponRenderer;
    public Vector2 PointerPosition { get; set; }

    [SerializeField] protected GameObject muzzle;

    [SerializeField] protected int ammo = 30;
    [SerializeField] protected WeaponDataSO weaponData;

    public int Ammo
    {
        get { return ammo; }
        set { 
            ammo = Mathf.Clamp(value, 0, weaponData.AmmoCapacity); 
            }
    }

    public bool AmmoFull { get => ammo >= weaponData.AmmoCapacity; }
    protected bool isShooting = false;
    [SerializeField] protected bool reloadCoroutine = false;
    [field: SerializeField] public UnityEvent OnShoot { get; set; }
    [field: SerializeField] public UnityEvent OnShootNoAmmo { get; set; }
    [field: SerializeField] public UnityEvent OnRechamber { get; set; }

    private void Start()
    {
        Ammo = weaponData.AmmoCapacity;
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

    public void Reload(int ammo)
    {
        Ammo += ammo;
    }

    private void UseWeapon()
    {
        if(isShooting && reloadCoroutine == false)
        {
            if(Ammo > 0)
            {
                Ammo--;
                OnShoot?.Invoke();
                for(int i = 0; i < weaponData.GetBulletCountToSpawn(); i++)
                {
                    ShootBullet();
                }
                Recoil();
            }
            else
            {
                isShooting = false;
                OnShootNoAmmo?.Invoke();
                return;
            }
            FinishShooting();
        }
    }

    private void Recoil()
    {
        var knockback = transform.root.gameObject.GetComponent<Knockback>();
        if(knockback != null)
        {
            knockback.PlayFeedbackV2(weaponData.Recoil, PointerPosition); 
        }
    }

    private void FinishShooting()
    {
        StartCoroutine(DelayNextShootCoroutine());
        if(weaponData.AutomaticFire == false)
        {
            isShooting = false;
        }
    }

    protected IEnumerator DelayNextShootCoroutine()
    {
        reloadCoroutine = true;
        yield return new WaitForSeconds(weaponData.WeaponDelay / 2);
        OnRechamber?.Invoke();
        yield return new WaitForSeconds(weaponData.WeaponDelay / 2);
        reloadCoroutine = false;
    }

    private void ShootBullet()
    {
        SpawnBullet(muzzle.transform.position, CalculateAngle(muzzle));
    }

    private void SpawnBullet(Vector3 position, Quaternion rotation)
    {
        var bulletPrefab = Instantiate(weaponData.BulletData.bulletPrefab, position, rotation);
        bulletPrefab.GetComponent<Bullet>().BulletData = weaponData.BulletData;
    }

    private Quaternion CalculateAngle(GameObject muzzle)
    {
        float spread = Random.Range(-weaponData.SpreadAngle, weaponData.SpreadAngle);
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

    internal void AddAmmo(int ammoAmount)
    {
        ammo += ammoAmount;
    }
}
