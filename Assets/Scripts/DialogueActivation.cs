using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueActivation : MonoBehaviour
{
    public Dialogue dialogue;
    private int hitCount = 0;

    public void TriggerDialogue()
    {
        Debug.Log("Dialogue triggered!");
        PlayerMovement playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        playerMovement.enabled = false;
        FindObjectOfType<DialogueReglages>().CommencerDialogue(dialogue);
        hitCount = 0;
    }

    public void TakeHit()
    {
        hitCount++;
        if (hitCount >= 3)
        {
            TriggerDialogue();
        }
    }
}
