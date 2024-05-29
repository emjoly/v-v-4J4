using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class BossManager : MonoBehaviour
{
    public Boss hand1;
    public Boss hand2;
    public GameObject vv;

    private Animator vvAnimator; // Declare Animator variable

    void Start()
    {
        vvAnimator = vv.GetComponent<Animator>(); // Get Animator component of vv
    }

    void Update()
    {
        if (hand1.isDead && hand2.isDead)
        {
            vvAnimator.SetBool("EstMort", true); // Set animator parameter
            StartCoroutine(LoadSceneAfterDelay(7));
        }
    }

    IEnumerator LoadSceneAfterDelay(int delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene("Cine2");
    }
}