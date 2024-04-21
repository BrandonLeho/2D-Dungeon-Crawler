using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    private Interactables interactables;
    public bool canInteract;
    public GameObject npc;

    private void Awake()
    {
        interactables = GetComponent<Interactables>();
    }
    public void Interact()
    {
        if(canInteract)
        {
            npc.GetComponent<TimeReset>().Reset();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "NPC")
        {
            canInteract = true;
            npc = collision.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "NPC")
        {
            canInteract = false;
            npc = null;
        }
    }
}
