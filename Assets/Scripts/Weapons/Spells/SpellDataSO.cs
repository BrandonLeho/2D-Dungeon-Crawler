using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Weapons/SpellData")]
public class SpellDataSO : ScriptableObject
{
    [field: SerializeField] public BulletDataSO BulletData { get; set; }
    [field: SerializeField] public bool AutomaticFire { get; set; } = false;
    [field: SerializeField] [field: Range (0, 1000000)] public int Mana { get; set; } = 100;
    [field: SerializeField] [field: Range (0.01f, 10f)]public float WeaponDelay { get; set; } = 0.1f;
    [field: SerializeField] [field: Range (0, 10000f)]public float Recoil { get; set; } = 0.1f;
    [field: SerializeField] [field: Range (0, 180)] public float SpreadAngle { get; set; } = 5;
    [SerializeField] private bool multiBulletShot = false;
    [SerializeField] [Range (1, 100)]private int bulletCount = 0;

    internal int GetBulletCountToSpawn()
    {
        if(multiBulletShot == true)
        {
            return bulletCount;
        }
        return 1;
    }
}
