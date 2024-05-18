using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatforms : MonoBehaviour
{
    private float Delai = 0.5f;
    private float RespawnDelai = 5f;

    [SerializeField] private Rigidbody2D rb;
    private bool playerOnPlatform = false;
    private Vector3 initialPosition;

    private void Start()
    {
        initialPosition = transform.position;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerOnPlatform = true;
            StartCoroutine(Fall());
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerOnPlatform = false;
        }
    }

    private IEnumerator Fall()
    {
        yield return new WaitForSeconds(Delai);

        if (playerOnPlatform)
        {
            rb.bodyType = RigidbodyType2D.Dynamic;
            StartCoroutine(Respawn());
        }
    }

    private IEnumerator Respawn()
    {
        yield return new WaitForSeconds(RespawnDelai);

        transform.position = initialPosition;
        rb.bodyType = RigidbodyType2D.Static;
    }
}

