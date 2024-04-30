using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class Intro : MonoBehaviour
{
    // Fonction pour jouer au jeu
    public void playGame (){
        // On charge la scène suivante (numéro d'index)
        SceneManager.LoadScene("IntroCine");
    }
    // Fonction pour quitter le jeu
    public void quitGame (){
        Debug.Log("Quit");
        // On quitte l'application
        Application.Quit();
    }
}

