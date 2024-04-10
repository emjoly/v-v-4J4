using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuPause : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI;
    public GameObject OptionMenu;
    public GameObject ControlesMenu;
    public GameObject PauseMenu;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !AnySubMenusActive())
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    bool AnySubMenusActive()
    {
        // Check if any of the sub-menus are active
        if (OptionMenu.activeSelf || ControlesMenu.activeSelf || PauseMenu.activeSelf)
            return true;
        return false;
    }

    public void Resume()
    {
        if (GameIsPaused)
        {
            pauseMenuUI.SetActive(false);
            Time.timeScale = 1f;
            GameIsPaused = false;
        }
    }

    void Pause()
    {
        if (!GameIsPaused)
        {
            pauseMenuUI.SetActive(true);
            Time.timeScale = 0f;
            GameIsPaused = true;
        }
    }

    public void LoadMenu()
    {
        Time.timeScale = 1f;
        Debug.Log("Loading menu...");
    }

    public void QuitGame()
    {
        Debug.Log("Quitting game...");
        Application.Quit();
    }
}
