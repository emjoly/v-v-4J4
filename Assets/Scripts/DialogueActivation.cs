using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueActivation : MonoBehaviour
{
    public Dialogue dialogue;
    private int hitCount = 0; // Variable to track the number of hits

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Collision detected!");
        // Check if the collider is the player
        if (collision.collider.CompareTag("Player"))
        {
            // Increase hit count
            hitCount++;

            // Check if hit count is 3
            if (hitCount >= 3)
            {
                // Trigger the dialogue
                TriggerDialogue();
                // Reset hit count for future interactions
                hitCount = 0;
            }
        }
    }

    // Function to trigger the dialogue
    public void TriggerDialogue()
    {
        Debug.Log("Dialogue triggered!");
        // Find the DialogueReglages object and call its CommencerDialogue function
        FindObjectOfType<DialogueReglages>().CommencerDialogue(dialogue);
    }
}

