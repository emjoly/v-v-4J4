using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueReglages : MonoBehaviour
{
    // Variables pour le dialogue
    public TextMeshProUGUI NomTexte;
    public TextMeshProUGUI DialogueTexte;
    // Variable pour l'animation
    public Animator animator;
    // File d'attente de phrases
    private Queue<string> phrases;

    // Start is called before the first frame update
    void Start()
    {
        // Initialisation de la file d'attente de phrases
        phrases = new Queue<string>();
    }
    // Fonction pour commencer le dialogue
    public void CommencerDialogue(Dialogue dialogue)
    {
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
        // On désactive l'animation
        animator.SetBool("IsOpen", false);
    }

}
