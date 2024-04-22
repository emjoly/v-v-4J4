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
    public Rigidbody2D rb;
    public SpriteRenderer ennemiSprite;

    private float damageCooldown = 1.5f; // 2 seconds cooldown
    private float lastDamageTime;

    public GameObject pointA;
    public GameObject pointB;
    private Transform currentPosition;
    public float speed;
    
    // Start is called before the first frame update
    void Start()
    {
        // On initialise la vie actuelle à la vie maximum
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        currentPosition = pointB.transform;
    }

    private void Update()
    {
        Vector2 point = currentPosition.position - transform.position;
        if(currentPosition == pointB.transform)
        {
            rb.velocity = new Vector2(speed, 0);
        }
        else
        {
            rb.velocity = new Vector2(-speed, 0);
        }
        if(Vector2.Distance(transform.position, currentPosition.position) < 0.5f && currentPosition == pointB.transform)
        {
            currentPosition = pointA.transform;
            ennemiSprite.flipX = true;
        }
        if (Vector2.Distance(transform.position, currentPosition.position) < 0.5f && currentPosition == pointA.transform)
        {
            currentPosition = pointB.transform;
            ennemiSprite.flipX = false;

        }
    }

    // Fonction pour infliger des dégâts
    public void TakeDamage(int damage)
    {
        AudiosSettings.PlaySound("AttackHitTest");
        // On enlève les dégâts à la vie actuelle
        currentHealth -= damage;

        animator.SetTrigger("AMal");

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