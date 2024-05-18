using System.Collections;
using UnityEngine;

public class EnnemyFollowDamage : MonoBehaviour
{
    // Int for the maximum health of the enemy
    public int maxHealth = 100;
    // Int for the current health
    int currentHealth;

    public Animator animator;
    public AudioClip SonBlesse;

    private float damageCooldown = 1.5f; // 2 seconds cooldown
    private float lastDamageTakenTime;

    private bool isDead = false;
    private bool isBlesse = false;

    public EnnemyFollow ennemyFollow;

    // Start is called before the first frame update
    void Start()
    {
        // Initialize the current health to the maximum health
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();
    }

    public void TakeDamage(int damage)
    {
        if (Time.time >= lastDamageTakenTime + damageCooldown && !isDead)
        {
            isBlesse = true;
            currentHealth -= damage;
            animator.SetTrigger("AMal");
            GetComponent<AudioSource>().PlayOneShot(SonBlesse);
            StartCoroutine(BlesseBack2False(1.0f)); // freeze when it is hurt

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

    // Function for the death of the enemy
    void Die()
    {
        Debug.Log("Ennemi mort");
        animator.SetBool("EstMort", true);
        GetComponent<Collider2D>().enabled = false;
        // Disable the enemy
        this.enabled = false;
        ennemyFollow.enabled = false; 
        isDead = true;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerMovement player = collision.GetComponent<PlayerMovement>();
        if (player != null)
        {
            if (Time.time >= lastDamageTakenTime + damageCooldown)
            {
                player.TakeDamage(10);
                lastDamageTakenTime = Time.time;
            }
        }
    }
}
