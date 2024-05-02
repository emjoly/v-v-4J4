using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss : MonoBehaviour
{
    public int maxHealth = 100;
    int currentHealth;
    public int damage = 20;
    private float damageCooldown = 1f;
    private float lastDamageTime;
    public Slider healthBar;
    public Animator animator;
    private bool isDead = false;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.value = currentHealth;
    }

    public void TakeDamage(int damage)
    {
        if (Time.time >= lastDamageTime + damageCooldown)
        {
            currentHealth -= damage;
            animator.SetTrigger("AMal");

            if (currentHealth <= 0)
            {
                Die();
            }

            lastDamageTime = Time.time;
            if (healthBar != null)
            {
                healthBar.value = currentHealth;
            }
        }
    }

    void Die()
    {
        Debug.Log("Boss mort");
        animator.SetBool("EstMort", true);
        GetComponent<Collider2D>().enabled = false;
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
                player.TakeDamage(damage);
                lastDamageTime = Time.time;
            }
        }
    }
}


