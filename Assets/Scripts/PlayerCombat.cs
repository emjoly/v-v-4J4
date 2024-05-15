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

    public bool hasAttacked = false;
    bool hasDealtDamage = false;
    bool faitFaceADroite = false;

    public AudioClip SonAttaque;

    public DialogueReglages dialogueReglages;
    

    // Update is called once per frame
void Update()
{
    if (!GetComponent<PlayerMovement>().isDead && Time.time >= prochaineAttaqueTemps)
    {
        if (Input.GetButtonDown("Attack") && !dialogueReglages.isDialogueOpen)
        {
            animator.SetBool("IsAttacking", true); // Start the attack animation
            animator.SetLayerWeight(1, 2);
            prochaineAttaqueTemps = Time.time + tempsAttenteAttaque;
            GetComponent<AudioSource>().PlayOneShot(SonAttaque);
            StartCoroutine(AttackCoroutine());
        }
        else
        {
            animator.SetBool("IsAttacking", false);
            animator.SetLayerWeight(1, 0);  // End the attack animation
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
                Ennemi ennemiComponent = enemy.GetComponent<Ennemi>();
                if (ennemiComponent != null)
                {
                    ennemiComponent.TakeDamage(DommageAttaque);
                }
                else
                {
                    Debug.LogWarning("Meow");
                }
            }
        }
        hasDealtDamage = true;
    }

    IEnumerator AttackCoroutine()
    {
        hasAttacked = true;
        float attackEndTime = Time.time + tempsAttenteAttaque; // NEED TO CHANGE THIS 
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
                    else if (enemy.CompareTag("Boss") || enemy.CompareTag("BossHand"))
                    {
                        Boss bossComponent = enemy.GetComponent<Boss>();
                        if (bossComponent != null)
                        {
                            bossComponent.TakeDamage(DommageAttaque);
                        }
                    }
                    else
                    {
                        Ennemi ennemiComponent = enemy.GetComponent<Ennemi>();
                        if (ennemiComponent != null)
                        {
                            ennemiComponent.TakeDamage(DommageAttaque);
                        }
                        else
                        {
                            Debug.LogWarning("Meow");
                        }
                    }
                }
                hasDealtDamage = true;
            }
            yield return null;
        }
        hasAttacked = false;
        hasDealtDamage = false;
        animator.SetLayerWeight(1, 0); 
    }

    void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
        {
            return;
        }
        if (attackPoint != null)
        {
            Gizmos.DrawWireSphere(attackPoint.position, attackRange);
        }
    }
    public void FlipPlayer()
    {
        faitFaceADroite = !faitFaceADroite;

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
