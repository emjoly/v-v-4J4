using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
// Classe Dialogue qui permet de créer des dialogues
public class Dialogue
{
    // Nom de la personne ou la chose qui parle dans le dialogue 
    public string nom;
    // Phrases de la personne ou la chose qui parle dans le dialogue
    [TextArea(3, 10)]
    // Tableau de phrases
    public string[] phrases;

}
