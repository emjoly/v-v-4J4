using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class Ennemi : MonoBehaviour
{
    // Int pour le maximum de vie de l'ennemi
    public int maxHealth = 100;
    // Int pour la vie actuelle
    int currentHealth;

    public Animator animator;


    private float damageCooldown = 1.5f; // 2 seconds cooldown
    private float lastDamageTime;

    // Start is called before the first frame update
    void Start()
    {
        // On initialise la vie actuelle à la vie maximum
        currentHealth = maxHealth;
    }
    // Fonction pour infliger des dégâts
    public void TakeDamage(int damage)
    {
        AudiosSettings.PlaySound("AttackHitTest");
        // On enlève les dégâts à la vie actuelle
        currentHealth -= damage;

        animator.SetTrigger("Mal");

        // On vérifie si la vie actuelle est inférieure ou égale à 0
        if (currentHealth <= 0)
        {
            // On appelle la fonction Die
            Die();
        }
    }
    // Fonction pour la mort de l'ennemi
    void Die()
    {
        Debug.Log("Ennemi mort");

        animator.SetBool("EstMort", true);
        GetComponent<Collider2D>().enabled = false;
        // On disable l'ennemy
        this.enabled = false;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerMovement player = collision.GetComponent<PlayerMovement>();
        if (player != null)
        {
            if (Time.time >= lastDamageTime + damageCooldown)
            {
                player.TakeDamage(10);
                lastDamageTime = Time.time;
            }
        }
    }
}