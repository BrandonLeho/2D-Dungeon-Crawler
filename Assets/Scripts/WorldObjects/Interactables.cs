using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Interactables : MonoBehaviour
{
    public GameObject dialogPanel;
    public TextMeshProUGUI dialogText;
    public string[] dialog;
    private int index = 0;

    public float wordSpeed;
    public bool playerIsClose, isTyping;

    private void Update()
    {
        if(isTyping)
        {
                dialogPanel.SetActive(true);
                StartCoroutine(Typing());
        }
    }

    public void StartDialog()
    {
        isTyping = true;
    }

    public void StopDialog()
    {
        isTyping = false;
    }

    public void RemoveText()
    {
        dialogText.text = "";
        index = 0;
        dialogPanel.SetActive(false);
        isTyping = false;
    }

    public void NextLine()
    {
        if(index < dialog.Length - 1)
        {
            index++;
            dialogText.text = "";
            StartCoroutine(Typing());
        }
        else
        {
            RemoveText();
        }
    }

    IEnumerator Typing()
    {
        foreach(char letter in dialog[index].ToCharArray())
        {
            dialogText.text += letter;
            yield return new WaitForSeconds(wordSpeed);
        }
    }
}
