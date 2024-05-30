using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueReglages2 : MonoBehaviour
{
    // Variables pour le dialogue
    public TextMeshProUGUI NomTexte;
    public TextMeshProUGUI DialogueTexte;
    // Variable pour l'animation
    public Animator animator;
    // File d'attente de phrases
    private Queue<string> phrases;

    public bool isDialogueOpen = false;
    public bool hasDialogueActivated = false;

    public DialogueActivation2 dialogueActivation;
    public AudioSource backgroundMusicSource;
    public AudioClip newBackgroundMusic; 

    public GameObject spikesAndBeamController;
    public GameObject Boss;
    public GameObject Plat1;
    public GameObject Plat2;

    // Delay duration
    private float delayDuration = 2f;

    // Flag to track if the delay is active
    private bool isDelayActive = false;

    // Start is called before the first frame update
    void Start()
    {
        // Initialisation de la file d'attente de phrases
        phrases = new Queue<string>();
    }

    void Update()
    {
        // Press la touche entrer pour aller à la prochaine phrase
        if (isDialogueOpen && Input.GetKeyDown(KeyCode.Return))
        {
            // Display the next sentence
            AfficherProchainePhrase();
        }
    }

    // Fonction pour commencer le dialogue
    public void CommencerDialogue(Dialogue dialogue)
    {
        if (hasDialogueActivated)
        {
            return;
        }
        hasDialogueActivated = true;

        isDialogueOpen = true;
        // On active l'animation
        animator.SetBool("IsOpen", true);
        // On affiche le nom du la personne ou la chose qui parle
        NomTexte.text = dialogue.nom;
        // On vide la file d'attente de phrases
        phrases.Clear();
        // On ajoute les phrases à la file d'attente
        foreach (string phrase in dialogue.phrases)
        {
            phrases.Enqueue(phrase);
        }
        // On affiche la prochaine phrase
        AfficherProchainePhrase();

        // Change la background music
        if (backgroundMusicSource != null && newBackgroundMusic != null)
        {
            backgroundMusicSource.Stop();
            backgroundMusicSource.clip = newBackgroundMusic;
            backgroundMusicSource.Play();
        }
        else
        {
            Debug.Log("Aucune musique associée à ce dialogue.");
        }
    }

    // Fonction pour afficher la prochaine phrase
    public void AfficherProchainePhrase()
    {
        // Si la file d'attente est vide
        if (phrases.Count == 0)
        {
            FinDialogue();
            return;
        }
        // On affiche la prochaine phrase
        string phrase = phrases.Dequeue();
        // On arrete toutes les coroutines
        StopAllCoroutines();
        // On lance la coroutine TouchePhrase
        StartCoroutine(TouchePhrase(phrase));
    }

    // Fonction pour taper les phrases
    IEnumerator TouchePhrase(string phrase)
    {
        // On vide le texte
        DialogueTexte.text = "";
        // On affiche chaque lettre une par une
        foreach (char lettre in phrase.ToCharArray())
        {
            // On ajoute la lettre
            DialogueTexte.text += lettre;
            yield return null;
        }
    }

    // Fonction pour finir le dialogue
    void FinDialogue()
    {
        isDialogueOpen = false;
        // On désactive l'animation
        animator.SetBool("IsOpen", false);
        PlayerMovement playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        playerMovement.enabled = true;
        Boss.SetActive(true);
        Plat1.SetActive(true);
        Plat2.SetActive(true);

        // Start the delay coroutine
        StartCoroutine(ActivateSpikesAndBeamControllerWithDelay());

        GameObject bossHealthSlider = System.Array.Find(Resources.FindObjectsOfTypeAll<GameObject>(), obj => obj.name == "BossHealthSlider");
        if (bossHealthSlider != null)
        {
            bossHealthSlider.SetActive(true);
        }

        Animator playerAnimator = playerMovement.GetComponent<Animator>();
        if (playerAnimator != null)
        {
            playerAnimator.speed = 1f;
        }
    }

    // Coroutine to activate spikesAndBeamController after delayDuration seconds
    IEnumerator ActivateSpikesAndBeamControllerWithDelay()
    {
        // If delay is already active, wait until it's finished
        if (isDelayActive)
        {
            yield break;
        }

        isDelayActive = true;
        yield return new WaitForSeconds(delayDuration);

        // Activate spikesAndBeamController after the delay
        if (spikesAndBeamController != null)
        {
            spikesAndBeamController.SetActive(true);
        }

        isDelayActive = false;
    }
}


