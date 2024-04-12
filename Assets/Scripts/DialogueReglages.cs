using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueReglages : MonoBehaviour
{

    public TextMeshProUGUI NomTexte;
    public TextMeshProUGUI DialogueTexte;
    public Animator animator;

    private Queue<string> phrases;
    // Start is called before the first frame update
    void Start()
    {
        phrases = new Queue<string>();
    }

    public void CommencerDialogue(Dialogue dialogue)
    {
        animator.SetBool("IsOpen", true);

        NomTexte.text = dialogue.nom;

        phrases.Clear();

        foreach (string phrase in dialogue.phrases)
        {
            phrases.Enqueue(phrase);
        }

        AfficherProchainePhrase();
    }

    public void AfficherProchainePhrase()
    {
        if (phrases.Count == 0)
        {
            FinDialogue();
            return;
        }

        string phrase = phrases.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TouchePhrase(phrase));

        
    }

    IEnumerator TouchePhrase(string phrase)
    {
        DialogueTexte.text = "";
        foreach (char lettre in phrase.ToCharArray())
        {
            DialogueTexte.text += lettre;
            yield return null;
        }
    }

    void FinDialogue()
    {
        animator.SetBool("IsOpen", false);
    }

}
