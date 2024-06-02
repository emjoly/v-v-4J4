using UnityEngine;
using UnityEngine.SceneManagement;

public class PardonButton : MonoBehaviour
{
    public void OnClick()
    {
        SceneManager.LoadScene("Pardon");
    }
}