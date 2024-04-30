using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DialogueActivation : MonoBehaviour
{
    public Dialogue dialogue;
    public AudioSource backgroundMusicSource; 
    public AudioClip newBackgroundMusic; 

    private int hitCount = 0;
    private bool dialogueTriggered = false;

    public void TriggerDialogue()
    {
        if (dialogueTriggered)
        {
            return;
        }

        PlayerMovement playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        playerMovement.enabled = false;
        FindObjectOfType<DialogueReglages>().CommencerDialogue(dialogue);
        hitCount = 0;
        dialogueTriggered = true;
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
