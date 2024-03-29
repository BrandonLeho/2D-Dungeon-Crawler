using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Enemies/EnemyData")]
public class EnemyDataSO : ScriptableObject
{
    [field: SerializeField] public int MaxHealth { get; set; } = 1000;
    [field: SerializeField] public int MaxStamina { get; set; } = 1000;
    [field: SerializeField] public int Damage { get; set; } = 100;
}
