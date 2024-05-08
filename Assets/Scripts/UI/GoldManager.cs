using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GoldManager : MonoBehaviour
{
    [SerializeField] TMP_Text goldText;
    Player player;

    private void Awake()
    {
        player = FindObjectOfType<Player>();
    }
    public void AddGold(int amount)
    {
        goldText.text = player.goldTotal.ToString();
    }

    public void RemoveGold(int amount)
    {
        goldText.text = player.goldTotal.ToString();
    }
}
