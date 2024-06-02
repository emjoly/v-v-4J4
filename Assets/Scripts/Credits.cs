using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class CreditsController : MonoBehaviour
{
    public Transform creditsParent;
    public float scrollSpeed = 50f;
    public float delayBeforeStart = 3f;
    public Image fadeImage; // Assign this in the Inspector
    public float fadeDuration = 2f;

    void Start()
    {
        StartCoroutine(FadeToBlack());
    }

    IEnumerator FadeToBlack()
    {
        // Fade to black
        float elapsedTime = 0f;
        Color color = fadeImage.color;
        while (elapsedTime < fadeDuration)
        {
            color.a = Mathf.Lerp(0f, 1f, elapsedTime / fadeDuration);
            fadeImage.color = color;
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        color.a = 1f;
        fadeImage.color = color;

        // Wait for 3 seconds after scene change
        yield return new WaitForSeconds(delayBeforeStart);

        // Start credits scrolling
        StartCoroutine(StartCredits());
    }

    IEnumerator StartCredits()
    {
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
