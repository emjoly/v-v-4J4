using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ennemi : MonoBehaviour
{
    public int maxHealth = 100;
    int currentHealth;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        // Hurt animation 

        if(currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        // Death animation
        // Disable the ennemy
        Debug.Log("Ennemi mort");
        Destroy(gameObject);
    }
}
