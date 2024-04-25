using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    // Variables pour l'attaque
    public Animator animator;
    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask enemyLayers;
    public int DommageAttaque = 20;
    public float tempsAttenteAttaque;
    float prochaineAttaqueTemps = 0f;

    bool faitFaceADroite = false;
    bool hasAttacked = false; // Add this line
    bool hasDealtDamage = false; // Add this line

    public AudioClip SonAttaque;

    public DialogueReglages dialogueReglages;

    // Update is called once per frame
    void Update()
    {
        if (Time.time >= prochaineAttaqueTemps)
        {
            if (Input.GetButtonDown("Attack")&& !dialogueReglages.isDialogueOpen)
            {                
                animator.SetTrigger("Attack");
                // On active l'animation d'attaque
                prochaineAttaqueTemps = Time.time + tempsAttenteAttaque;
                GetComponent<AudioSource>().PlayOneShot(SonAttaque);
                StartCoroutine(AttackCoroutine()); // Start the attack coroutine
            }
        }
    }

public void PerformAttack()
{
    if (dialogueReglages.isDialogueOpen || hasDealtDamage)
    {
        return;
    }

    // Detect enemies in range
    Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

    // Deal damage if it hasn't been dealt yet
    foreach (Collider2D enemy in hitEnemies)
    {
        DialogueActivation boss = enemy.GetComponent<DialogueActivation>();
        if (boss != null)
        {
            boss.TakeHit();
        }
        else
        {
            enemy.GetComponent<Ennemi>().TakeDamage(DommageAttaque);
        }
    }
    hasDealtDamage = true; // Damage has been dealt
}

    IEnumerator AttackCoroutine()
    {
        hasAttacked = true;
        float attackEndTime = Time.time + tempsAttenteAttaque;
        while (Time.time < attackEndTime)
        {
            // Detect enemies in range
            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
            // Deal damage if it hasn't been dealt yet
            if (!hasDealtDamage)
            {
                foreach (Collider2D enemy in hitEnemies)
                {
                    DialogueActivation boss = enemy.GetComponent<DialogueActivation>();
                    if (boss != null)
                    {
                        boss.TakeHit();
                    }
                    else
                    {
                        enemy.GetComponent<Ennemi>().TakeDamage(DommageAttaque);
                    }
                }
                hasDealtDamage = true; // Damage has been dealt
            }
            yield return null; // Wait for the next frame
        }
        hasAttacked = false;
        hasDealtDamage = false; // Reset this at the end of the attack
    }

    void OnDrawGizmosSelected()
    {
        // Si c'est nul, sortir de la fonction
        if (attackPoint == null)
        {
            return;
        }
        // Dessiner un cercle pour la range d'attaque
        if (attackPoint != null)
        {
            Gizmos.DrawWireSphere(attackPoint.position, attackRange);
        }
    }
    // Function to flip the player
    public void FlipPlayer()
    {
        // Flip the player's direction
        faitFaceADroite = !faitFaceADroite;

        // Flip le attackPoint
        if (!faitFaceADroite)
        {
            attackPoint.localPosition = new Vector3(-Mathf.Abs(attackPoint.localPosition.x), attackPoint.localPosition.y, attackPoint.localPosition.z);
        }
        else
        {
            attackPoint.localPosition = new Vector3(Mathf.Abs(attackPoint.localPosition.x), attackPoint.localPosition.y, attackPoint.localPosition.z);
        }
    }

    public void ResetAttack()
{
    hasDealtDamage = false;
}
}
