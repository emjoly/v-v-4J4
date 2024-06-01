using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class PardonButton : MonoBehaviour
{
    public void OnClick()
    {
        SceneManager.LoadScene("Pardon");
    }
}

