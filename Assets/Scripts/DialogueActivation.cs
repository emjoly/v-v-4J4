using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
 
public class DialogueActivation : MonoBehaviour
{
    public Dialogue dialogue;
    public AudioSource backgroundMusicSource;
    public AudioClip newBackgroundMusic;

    private int hitCount = 0;
    private bool dialogueTriggered = false;

    public CinemachineVirtualCamera camPerso;
    public CinemachineVirtualCamera camBoss;

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
        backgroundMusicSource.clip = newBackgroundMusic;
        backgroundMusicSource.loop = true;
        backgroundMusicSource.Play();
    }

    public void TakeHit()
    {
        Debug.Log("Boss hit count: " + hitCount); // Add this line for debugging
        hitCount++;
        if (hitCount == 1)
        {
            CapsuleCollider2D collider = GetComponent<CapsuleCollider2D>();
            if (collider != null)
            {
                collider.enabled = false;
            }

            SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
            if (spriteRenderer != null)
            {
                Color color = spriteRenderer.color;
                color.a *= 0.25f;
                spriteRenderer.color = color;
            }

            TriggerDialogue();
            camPerso.enabled = false;
            camBoss.enabled = true;
        }
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Boss collided with player!"); // Debug for collision with player

            // Call TakeHit() when the boss collides with the player
            TakeHit();
        }
    }

}

