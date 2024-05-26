using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DestructionPlateforme : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerMovement playerMovement = collision.gameObject.GetComponent<PlayerMovement>();
            if (playerMovement != null && playerMovement.isSlaming)
            {
                BreakPlatform();
            }
        }
    }

    public void BreakPlatform()
    {
        Debug.Log("BreakPlatform called");
        Destroy(gameObject);
    }
}
