using System.Collections;
using UnityEngine;

public class EnnemyFollowDamage : MonoBehaviour
{
    public int maxHealth = 100;
    int currentHealth;

    public Animator animator;
    public AudioClip SonBlesse;
    public AudioClip SonExplose;

    private float damageCooldown = 1.5f;
    private float lastDamageTakenTime;

    private bool isDead = false;
    private bool isBlesse = false;

    public EnnemyFollow ennemyFollow;

    // Start is called before the first frame update
    void Start()
    {
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
            StartCoroutine(BlesseBack2False(1.0f));

            if (currentHealth <= 0)
            {
                Die();
                Destroy(gameObject, 1f);
            }

            lastDamageTakenTime = Time.time;
        }
    }
    private IEnumerator BlesseBack2False(float delay)
    {
        yield return new WaitForSeconds(delay);
        isBlesse = false;
    }

    void Die()
    {
        Debug.Log("Ennemi mort");
        animator.SetBool("EstMort", true);
        GetComponent<AudioSource>().PlayOneShot(SonExplose);
        GetComponent<Collider2D>().enabled = false;
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
