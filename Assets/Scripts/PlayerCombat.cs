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
    public float debisAttaque = 2f;
    public float tempsAttenteAttaque;
    float prochaineAttaqueTemps = 0f;

    bool faitFaceADroite = false;  // 

    public AudioClip SonAttaque;

    // Update is called once per frame
    void Update()
    {
        if (Time.time >= prochaineAttaqueTemps)
        {
            if (Input.GetButtonDown("Attack"))
            {                
                animator.SetTrigger("Attack");
                // On active l'animation d'attaque
                prochaineAttaqueTemps = Time.time + tempsAttenteAttaque;
                GetComponent<AudioSource>().PlayOneShot(SonAttaque);
            }
        }
    }

    // Fonction pour attaquer
    public void PerformAttack()
    {
        AudiosSettings.PlaySound("AttackTest");

        // Detecter les ennemis dans la range d'attaque
        Collider2D[] EnnemieTouche = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
        // Appliquer des degats
        foreach (Collider2D ennemi in EnnemieTouche)
        {
            if (ennemi.CompareTag("BossStart"))
            {
                DialogueActivation boss = ennemi.GetComponent<DialogueActivation>();
                boss.hitCount++;
                if (boss.hitCount >= 4)
                {
                    boss.TriggerDialogue();
                }
            }
            else
            {
                ennemi.GetComponent<Ennemi>().TakeDamage(DommageAttaque);
            }
        }
    }

    // Fonction pour faire apparaitre un cercle dans l'editeur
    void OnDrawGizmosSelected()
    {
        // Si c'est nul, sortir de la fonction
        if (attackPoint == null)
        {
            return;
        }
        // Dessiner un cercle pour la range d'attaque
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
    // Function to flip the player
    public void FlipPlayer()
    {
        // Flip the player's direction
        faitFaceADroite = !faitFaceADroite;

        // Flip the attackPoint
        if (!faitFaceADroite)
        {
            attackPoint.localPosition = new Vector3(-Mathf.Abs(attackPoint.localPosition.x), attackPoint.localPosition.y, attackPoint.localPosition.z);
        }
        else
        {
            attackPoint.localPosition = new Vector3(Mathf.Abs(attackPoint.localPosition.x), attackPoint.localPosition.y, attackPoint.localPosition.z);
        }
    }
}