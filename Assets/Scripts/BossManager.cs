using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
 
public class BossManager : MonoBehaviour
{
    public Boss hand1;
    public Boss hand2;

    void Update()
    {
        if (hand1.isDead && hand2.isDead)
        {
            StartCoroutine(LoadSceneAfterDelay(2));
        }
    }

    IEnumerator LoadSceneAfterDelay(int delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene("Cine2");
    }
}

