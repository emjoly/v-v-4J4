using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss2 : MonoBehaviour
{
    public int maxHealth = 300;
    public int currentHealth;
    private float damageCooldown = 1f;
    private float lastDamageTime;
    public Slider healthBar;
    public bool isDead = false;
    private Animator animator;
    public GameObject SpikesAndBEAMS;
    public GameObject choixJoueur;
    private PlayerMovement playerMovement;

    void Start()
    {
        currentHealth = maxHealth;
        healthBar.gameObject.SetActive(true);
        animator = GetComponent<Animator>();
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            playerMovement = player.GetComponent<PlayerMovement>();
        }
    }

    void Update()
    {
        healthBar.value = currentHealth;
    }

    public void TakeDamage(int damage)
    {
        if (Time.time >= lastDamageTime + damageCooldown)
        {
            currentHealth -= damage;

            if (currentHealth <= 0)
            {
                // Changer pour l'animation Boss2pardon
                animator.SetTrigger("Pardon");
                // Désactiver les spikes et les beams
                SpikesAndBEAMS.SetActive(false);
                // Attendre 3 secondes avant d'activer l'image pour choix du joueur
                StartCoroutine(ActivateChoixJoueur());
                // Désactiver les mouvements du joueur
                if (playerMovement != null)
                {
                    playerMovement.enabled = false;
                }
            }

            lastDamageTime = Time.time;
            if (healthBar != null)
            {
                healthBar.value = currentHealth;
            }
        }
    }
        IEnumerator ActivateChoixJoueur()
    {
        yield return new WaitForSeconds(3);
        choixJoueur.SetActive(true);
    }
}
