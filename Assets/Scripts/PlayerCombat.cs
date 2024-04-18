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

    // Update is called once per frame
    void Update()
    {
        if (Time.time >= prochaineAttaqueTemps)
        {
            if (Input.GetButtonDown("Attack"))
            {
                Attack();
                prochaineAttaqueTemps = Time.time + tempsAttenteAttaque;
            }
        }
    }

    // Fonction pour attaquer
    void Attack()
    {
        // On active l'animation d'attaque
        animator.SetTrigger("Attack");
        // Detecter les ennemis dans la range d'attaque
        Collider2D[] EnnemieTouche = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
        // Appliquer des degats
        foreach (Collider2D ennemi in EnnemieTouche)
        {
            ennemi.GetComponent<Ennemi>().TakeDamage(DommageAttaque);
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
}
