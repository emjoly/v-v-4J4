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
    private float lastDamageTakenTime;

    private bool isDead = false;
    private bool isBlesse = false;

    public AudioClip SonBlesse;

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
    // si l'ennemi est mort il ne bouge plus
    if (!isDead)
    {
        Vector2 point = currentPosition.position - transform.position;
        if (currentPosition == pointB.transform)
        {
            rb.velocity = isBlesse ? Vector2.zero : new Vector2(speed, 0);
        }
        else
        {
            rb.velocity = isBlesse ? Vector2.zero : new Vector2(-speed, 0);
        }
        if (Vector2.Distance(transform.position, currentPosition.position) < 0.5f && currentPosition == pointB.transform)
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
}


    public void TakeDamage(int damage)
    {
        if (Time.time >= lastDamageTakenTime + damageCooldown && !isDead)
        {
            //AudiosSettings.PlaySound("AttackHitTest");
            isBlesse = true;
            currentHealth -= damage;
            animator.SetTrigger("AMal");
            GetComponent<AudioSource>().PlayOneShot(SonBlesse);
            StartCoroutine(BlesseBack2False(1.0f)); // freeze quand il est blessé

            if (currentHealth <= 0)
            {
                Die();
            }

            lastDamageTakenTime = Time.time;
        }
    }
    private IEnumerator BlesseBack2False(float delay)
    {
        yield return new WaitForSeconds(delay);
        isBlesse = false;
    }

    // Fonction pour la mort de l'ennemi
    void Die()
    {
        Debug.Log("Ennemi mort");
        animator.SetBool("EstMort", true);
        GetComponent<Collider2D>().enabled = false;
        rb.velocity = Vector2.zero;
        // On disable l'ennemy
        this.enabled = false;
        isDead = true;
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