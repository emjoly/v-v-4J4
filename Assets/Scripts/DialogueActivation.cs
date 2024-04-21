using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueActivation : MonoBehaviour
{
    public Dialogue dialogue;
    public int hitCount = 0; // Variable to track the number of hits

    // Function to trigger the dialogue
    public void TriggerDialogue()
    {
        Debug.Log("Dialogue triggered!");
        // Find the DialogueReglages object and call its CommencerDialogue function
        FindObjectOfType<DialogueReglages>().CommencerDialogue(dialogue);
        // Reset hit count for future interactions
        hitCount = 0;
    }
}
