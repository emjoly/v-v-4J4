using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
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

    void Update()
{
    if (!GetComponent<PlayerMovement>().isDead && Time.time >= prochaineAttaqueTemps)
    {
        if (Input.GetButtonDown("Attack") && !dialogueReglages.isDialogueOpen)
        {
            animator.SetBool("IsAttacking", true);
            animator.SetLayerWeight(1, 1);
            prochaineAttaqueTemps = Time.time + tempsAttenteAttaque;
            GetComponent<AudioSource>().PlayOneShot(SonAttaque);
            hasAttacked = true;
        }
        else if (!Input.GetButton("Attack"))
        {
            animator.SetBool("IsAttacking", false);
            animator.SetLayerWeight(1, 0);
            hasAttacked = false;
        }
    }

    if (animator.GetBool("IsAttacking"))
    {
        PerformAttacks();
    }

    hasDealtDamage = false;
}


    public void PerformAttacks()
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
                boss.TakeDamage(DommageAttaque);
            }
            else
            {
                EnnemyFollowDamage EnnemiVolant = enemy.GetComponent<EnnemyFollowDamage>();
                Ennemi ennemiComponent = enemy.GetComponent<Ennemi>();
                if (ennemiComponent != null)
                {
                    ennemiComponent.TakeDamage(DommageAttaque);
                }
                if (EnnemiVolant != null)
                {
                    EnnemiVolant.TakeDamage(DommageAttaque);
                }
            }
        }
        hasDealtDamage = true;
    }

    void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
        {
            return;
        }
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

    public void FlipPlayer()
    {
        faitFaceADroite = !faitFaceADroite;
        attackPoint.localPosition = new Vector3(faitFaceADroite ? Mathf.Abs(attackPoint.localPosition.x) : -Mathf.Abs(attackPoint.localPosition.x), attackPoint.localPosition.y, attackPoint.localPosition.z);
    }

    public void ResetAttack()
    {
        hasDealtDamage = false;
        animator.SetLayerWeight(1, 0);
    }
}
