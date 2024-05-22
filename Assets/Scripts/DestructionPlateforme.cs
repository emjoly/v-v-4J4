using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DestructionPlateforme : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
{
    // Check if the collision is with a platform tagged as "BrisPlateforme"
    if (collision.gameObject.CompareTag("Player") && collision.relativeVelocity.magnitude > 15f)
    {
        // On appelle la fonction BreakPlatform
        BreakPlatform();
    }
}


    // Fonction pour détruire la plateforme
    public void BreakPlatform()
    {
         Debug.Log("BreakPlatform called");
        // On détruit la plateforme
        Destroy(gameObject);
    }
}
