using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    private Interactables interactables;

    private void Awake()
    {
        interactables = GetComponent<Interactables>();
    }
    public void Interact()
    {
        Debug.Log("Interact");
        if(interactables.playerIsClose && interactables.isTyping == false)
            interactables.isTyping = true;
        else
        {
            interactables.NextLine();
        }
    }
}
