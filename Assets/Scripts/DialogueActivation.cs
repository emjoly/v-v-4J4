using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueActivation : MonoBehaviour
{
    public Dialogue dialogue;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Collision detected!");
        // Check if the colliding object has the tag "Player"
        if (collision.collider.CompareTag("Player"))
        {
            // Trigger the dialogue
            TriggerDialogue();
        }
    }

    public void TriggerDialogue()
    {
        Debug.Log("Dialogue triggered!");
        FindObjectOfType<DialogueReglages>().CommencerDialogue(dialogue);
    }
}
