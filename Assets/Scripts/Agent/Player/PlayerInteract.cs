using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    private Interactables interactables;
    public bool canInteract;
    public GameObject npc;
    [SerializeField] public TMP_Text resetTimeText;
    Player player;
    GoldManager gold;
    ClockManager clockManager;
    private void Awake()
    {
        interactables = GetComponent<Interactables>();
        player = GetComponent<Player>();
        clockManager = FindObjectOfType<ClockManager>();
        gold = FindObjectOfType<GoldManager>();
    }

    public void Interact()
    {
        if (canInteract)
        {
            if (player.goldTotal >= clockManager.currentGoldCost)
            {
                player.goldTotal -= clockManager.currentGoldCost;
                gold.RemoveGold(clockManager.currentGoldCost);
                clockManager.ResetClock();
                npc.GetComponent<TimeReset>().Reset();
                if (player.goldTotal <= clockManager.currentGoldCost)
                {
                    resetTimeText.text = "Not enough gold";
                    resetTimeText.color = Color.red;
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "NPC")
        {
            canInteract = true;
            npc = collision.gameObject;
            if (collision.gameObject.name == "Clock")
            {
                if (player.goldTotal >= clockManager.currentGoldCost)
                {
                    resetTimeText.text = "[E] Reset Time";
                    resetTimeText.color = Color.white;
                }
                else
                {
                    resetTimeText.text = "Not enough gold";
                    resetTimeText.color = Color.red;
                }
                resetTimeText.gameObject.SetActive(true);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "NPC")
        {
            canInteract = false;
            npc = null;
            if (collision.gameObject.name == "Clock")
            {
                resetTimeText.gameObject.SetActive(false);
            }
        }
    }
}
