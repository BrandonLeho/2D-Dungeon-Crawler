using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Weapons/BulletData")]
public class BulletDataSO : ScriptableObject
{
    [field: SerializeField] public GameObject bulletPrefab { get; set; }   
    [field: SerializeField] [field: Range(1, 1000)] public float BulletSpeed { get; internal set; } = 1;
    [field: SerializeField] [field: Range(1, 10000000)] public int Damage { get; set; } = 1;
    [field: SerializeField] [field: Range(1, 10000000)] public int StaminaDamage { get; set; } = 1;
    [field: SerializeField] [field: Range(0f, 100f)]public float Friction { get; internal set; } = 0f;
    [field: SerializeField] public bool Ricochet { get; set; } = false;
    [field: SerializeField] public bool Pierce { get; set; } = false;
    [field: SerializeField] public bool RayCast { get; set; } = false;
    [field: SerializeField] public bool Explosion { get; set; } = false;
    [field: SerializeField] [field: Range(1, 1000)] public float ExplosionRadius { get; set; } = 5;
    [field: SerializeField] [field: Range(0, 100)] public float ExplosionFeedbackRadius { get; set; } = 1;
    [field: SerializeField] public GameObject ImpactObsticlePrefab { get; set; }
    [field: SerializeField] public GameObject ImpactEnemyPrefab { get; set; }
    [field: SerializeField] [field: Range(0, 1000)] public float KnockbackPower { get; set; } = 5;
    [field: SerializeField] [field: Range(0.01f, 5f)]public float KnockbackDelay { get; set; } = 0.1f;
    [field: SerializeField] public LayerMask BulletLayerMask { get; set; }
}
