using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class CreditsController : MonoBehaviour
{
    public Transform creditsParent;
    public float scrollSpeed = 50f;
    public float fadeDuration = 2f;
    public float delayBeforeStart = 3f;

    void Start()
    {
        StartCoroutine(StartCredits());
    }

    IEnumerator StartCredits()
    {
        // Wait for 3 seconds after scene change
        yield return new WaitForSeconds(delayBeforeStart);

        // Gradually move the credits text upwards
        Vector3 initialPosition = creditsParent.position;
        while (creditsParent.position.y < initialPosition.y + GetCreditsHeight())
        {
            creditsParent.position += Vector3.up * scrollSpeed * Time.deltaTime;
            yield return null;
        }

        // Load the menu scene
        SceneManager.LoadScene("Intro");
    }

    float GetCreditsHeight()
    {
        float height = 0f;
        foreach (Transform child in creditsParent)
        {
            height += child.GetComponent<TextMeshProUGUI>().preferredHeight;
        }
        return height;
    }
}
