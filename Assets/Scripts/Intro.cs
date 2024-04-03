using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class Intro : MonoBehaviour
{

    public void playGame (){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void quitGame (){
        Debug.Log("Quit");
        Application.Quit();
    }
}

