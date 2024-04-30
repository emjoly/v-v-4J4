using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        // Si la collision est avec une plateforme avec un layermask "BrisPlateforme et si le joueur entre en collision avec une vitesse supérieure à 10
        if (collision.gameObject.layer == LayerMask.NameToLayer("BrisPlateforme") && collision.relativeVelocity.magnitude > 10f)
        {
            // On appelle la fonction BreakPlatform
            BreakPlatform();
        }
    }
    // Fonction pour détruire la plateforme
    public void BreakPlatform()
    {
        // On détruit la plateforme
        Destroy(gameObject);
    }
}
