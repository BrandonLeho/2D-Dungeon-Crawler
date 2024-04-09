using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Weapons/MeleeData")]
public class MeleeData : ScriptableObject
{
    [field: SerializeField] [field: Range(1, 10000000)] public int Damage { get; set; } = 1;
    [field: SerializeField] [field: Range(1, 10000000)] public int StaminaDamage { get; set; } = 1;
    [field: SerializeField] [field: Range(0.01f, 2)] public float AttackDelay { get; set; } = 0.2f;
    [field: SerializeField] [field: Range(0, 10)] public float LungeDistance { get; set; } = 0.5f;
    [field: SerializeField] [field: Range(0, 100)] public float LungeSpeed { get; set; } = 10;
    [field: SerializeField] public bool CanParry { get; set; } = false;
    [field: SerializeField] [field: Range(0.01f, 2)] public float ParryDuration { get; set; } = 0.5f;
    [field: SerializeField] public GameObject ImpactObsticlePrefab { get; set; }
    [field: SerializeField] public GameObject ImpactEnemyPrefab { get; set; }
    [field: SerializeField] [field: Range(0, 1000)] public float KnockbackPower { get; set; } = 5;
    [field: SerializeField] [field: Range(0.01f, 5f)]public float KnockbackDelay { get; set; } = 0.1f;
    [field: SerializeField] [field: Range(0.01f, 10)]public float MeleeHitRadius { get; set; } = 1.1f;
    [field: SerializeField] public LayerMask MeleeLayerMask { get; set; }
}
