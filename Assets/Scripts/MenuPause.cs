using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuPause : MonoBehaviour
{
    // Variables pour le menu pause
    public static bool JeuEnPause = false;
    public GameObject pauseMenuUI;
    public GameObject OptionMenu;
    public GameObject ControlesMenu;
    public GameObject PauseMenu;

    // Update is called once per frame
    void Update()
    {
        // Si on appuie sur la touche Echap et qu'aucun sous-menu n'est actif
        if (Input.GetKeyDown(KeyCode.Escape) && !TouslesSousMenusActifs())
        {
            // Si le jeu est en pause, on le reprend
            if (JeuEnPause)
            {
                Resume();
            }
            else
            // Sinon, on le met en pause
            {
                Pause();
            }
        }
    }
    // Fonction pour vérifier si un sous-menu est actif
    bool TouslesSousMenusActifs()
    {
        // Si un des sous-menus est actif, retourner vrai
        if (OptionMenu.activeSelf || ControlesMenu.activeSelf || PauseMenu.activeSelf)
            return true;
        // Sinon, retourner faux
        return false;
    }
    // Fonction pour reprendre le jeu
    public void Resume()
    {
        // On désactive le menu pause
        if (JeuEnPause)
        {
            // On désactive le menu pause
            pauseMenuUI.SetActive(false);
            // On remet le temps à la normale
            Time.timeScale = 1f;
            // On dit que le jeu n'est plus en pause
            JeuEnPause = false;
        }
    }

    void Pause()
    {
        // Si le jeu n'est pas en pause, on le met en pause
        if (!JeuEnPause)
        {
            // On active le menu pause
            pauseMenuUI.SetActive(true);
            // On met le temps en pause
            Time.timeScale = 0f;
            // On dit que le jeu est en pause
            JeuEnPause = true;
        }
    }
    // Fonction pour charger le menu
    public void LoadMenu()
    {
        // On remet le temps à la normale
        Time.timeScale = 1f;
        Debug.Log("Loading menu...");
    }
    // Fonction pour quitter le jeu
    public void QuitGame()
    {
        Debug.Log("Quitting game...");
        // On quitte l'application
        Application.Quit();
    }
}
