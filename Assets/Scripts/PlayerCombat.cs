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
    bool hasAttacked = false;
    bool hasDealtDamage = false;

    public AudioClip SonAttaque;

    public DialogueReglages dialogueReglages;

    // Update is called once per frame
    void Update()
    {
        if (!GetComponent<PlayerMovement>().isDead && Time.time >= prochaineAttaqueTemps)
        {
            if (Input.GetButtonDown("Attack") && !dialogueReglages.isDialogueOpen)
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

    Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

    foreach (Collider2D enemy in hitEnemies)
    {
        if (enemy.CompareTag("Boss"))
        {
            Boss boss = enemy.GetComponent<Boss>();
        }
        else
        {
            // Check if enemy is not null and has the Ennemi component
            Ennemi ennemiComponent = enemy.GetComponent<Ennemi>();
            if (ennemiComponent != null)
            {
                ennemiComponent.TakeDamage(DommageAttaque);
            }
            else
            {
                Debug.LogWarning("The enemy does not have the Ennemi component.");
            }
        }
    }
    hasDealtDamage = true;
}



    IEnumerator AttackCoroutine()
    {
        hasAttacked = true;
        float attackEndTime = Time.time + tempsAttenteAttaque;
        while (Time.time < attackEndTime)
        {
            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
            if (!hasDealtDamage)
            {
                foreach (Collider2D enemy in hitEnemies)
                {
                    DialogueActivation boss = enemy.GetComponent<DialogueActivation>();
                    if (boss != null)
                    {
                        boss.TakeHit();
                    }
                    else if (enemy.CompareTag("Boss")|| enemy.CompareTag("BossHand")) // Check if the enemy is the boss
                    {
                        Boss bossComponent = enemy.GetComponent<Boss>();
                        if (bossComponent != null)
                        {
                            bossComponent.TakeDamage(DommageAttaque);
                        }
                    }
                    else
                    {
                        // Check if enemy is not null and has the Ennemi component
                        Ennemi ennemiComponent = enemy.GetComponent<Ennemi>();
                        if (ennemiComponent != null)
                        {
                            ennemiComponent.TakeDamage(DommageAttaque);
                        }
                        else
                        {
                            Debug.LogWarning("The enemy does not have the Ennemi component.");
                        }
                    }
                }
                hasDealtDamage = true;
            }
            yield return null;
        }
        hasAttacked = false;
        hasDealtDamage = false;
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
