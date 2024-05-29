using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
 
public class DialogueActivation : MonoBehaviour
{
    public Dialogue dialogue;
    public AudioSource backgroundMusicSource;
    public AudioClip newBackgroundMusic;

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

        Animator animator = playerMovement.GetComponent<Animator>();
        if (animator != null)
        {
            animator.speed = 0f;
        }

        FindObjectOfType<DialogueReglages>().CommencerDialogue(dialogue);
        dialogueTriggered = true;
        backgroundMusicSource.clip = newBackgroundMusic;
        backgroundMusicSource.loop = true;
        backgroundMusicSource.Play();
    }

    public void TakeHit()
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

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Boss collided with player!");

            TakeHit();
        }
    }
}
