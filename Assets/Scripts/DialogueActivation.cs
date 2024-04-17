using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueActivation : MonoBehaviour
{
    public Dialogue dialogue;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Collision detected!");
        // regarder si le collider est le joueur
        if (collision.collider.CompareTag("Player"))
        {
            // On appel la fonction TriggerDialogue
            TriggerDialogue();
        }
    }
    // Fonction pour déclencher le dialogue
    public void TriggerDialogue()
    {
        Debug.Log("Dialogue triggered!");
        // On trouve l'objet DialogueReglages(le script) et on appel sa fonction CommencerDialogue
        FindObjectOfType<DialogueReglages>().CommencerDialogue(dialogue);
    }
}
