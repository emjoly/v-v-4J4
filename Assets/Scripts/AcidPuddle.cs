using System.Collections;
using UnityEngine;

public class AcidPuddle : MonoBehaviour
{
    public int nombreDommage = 10;
    public float intervalDommage = 1.0f;
    public AudioClip sonAcid;
    private AudioSource audioSource;
    private Coroutine damageCoroutine;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            damageCoroutine = StartCoroutine(DamagePlayer(collision));
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (damageCoroutine != null)
            {
                StopCoroutine(damageCoroutine);
            }
        }
    }

    private IEnumerator DamagePlayer(Collider2D player)
    {
        PlayerMovement playerMovement = player.GetComponent<PlayerMovement>();

        if (playerMovement != null)
        {
            while (true)
            {
                playerMovement.TakeDamage(nombreDommage);
                if (sonAcid != null && audioSource != null)
                {
                    audioSource.PlayOneShot(sonAcid);
                }
                yield return new WaitForSeconds(intervalDommage);
            }
        }
    }
}
