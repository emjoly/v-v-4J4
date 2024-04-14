using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{

    public Animator animator;
    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask enemyLayers;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Attack")){
            Attack();
        }
    }

    void Attack(){
                // Animation Attack
        animator.SetTrigger("Attack");

        // Detecter les ennemis dans la range d'attaque
        Collider2D[] EnnemieTouche = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
        // Appliquer des degats
        foreach(Collider2D ennemi in EnnemieTouche){
            Debug.Log("Ennemi touché" + ennemi.name);
        }
    }

    void OnDrawGizmosSelected()
    {
        if(attackPoint == null){
            return;
        }
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
