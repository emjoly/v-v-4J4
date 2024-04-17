using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatforms : MonoBehaviour
{
    // Float pour le délai avant la chute de la plateforme
    private float Delai = 1f;
    private float DestructionDelai = 2f;

    // Rigidbody2D de la plateforme
    [SerializeField] private Rigidbody2D rb;

    // Fonction pour détecter la collision
    private void OnCollisionEnter2D(Collision2D collision){
        // Si la collision est avec le joueur
        if(collision.gameObject.CompareTag("Player")){
            // On appelle la coroutine Fall
            StartCoroutine(Fall());
        }
    }
    // Coroutine pour faire tomber la plateforme
    private IEnumerator Fall(){
        // On attend le délai avant de faire tomber la plateforme
        yield return new WaitForSeconds(Delai);
        // On change le bodyType de la plateforme en Dynamic
        rb.bodyType = RigidbodyType2D.Dynamic;
        // On détruit la plateforme après un certain délai
        Destroy(gameObject, DestructionDelai);
    }
}
