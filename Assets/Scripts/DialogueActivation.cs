using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class DialogueActivation : MonoBehaviour
{
    public Dialogue dialogue;
    public AudioSource backgroundMusicSource; 
    public AudioClip newBackgroundMusic;
    public GameObject BossHealthSlider;

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
    }

    public void TakeHit()
    {
        hitCount++;
        if (hitCount >= 3)
        {
            PolygonCollider2D collider = GetComponent<PolygonCollider2D>();
            if (collider != null)
            {
                collider.enabled = false;
            }

            BossHealthSlider.SetActive(true);
            TriggerDialogue();
            camPerso.enabled = false;
            camBoss.enabled = true;
        }
    }


}

