using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PicTombe : MonoBehaviour
{
    // Variables pour le rigidbody et le boxcollider
    Rigidbody2D rb;
    BoxCollider2D boxCollider2D;
    // Float pour la distance du raycast
    public float distance;
    // Bool pour savoir si le pic est en train de tomber
    bool entrainTomber = false;

    private void Start()
    {
        // On récupère le rigidbody et le boxcollider
        rb = GetComponent<Rigidbody2D>();
        boxCollider2D = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        // On désactive les collisions avec les triggers
        Physics2D.queriesStartInColliders = false;
        // Si le pic n'est pas en train de tomber
        if(entrainTomber == false){
            // On lance un raycast vers le bas
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, distance);
            Debug.DrawRay(transform.position, Vector2.down * distance, Color.red);
            // Si le raycast touche le joueur et que le joueur est en dessous du pic
            if(hit.transform != null){
                if(hit.collider.tag == "Player"){
                    // On active la gravité du pic
                    rb.gravityScale = 5;
                    entrainTomber = true;
                }
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Si le joueur entre en collision avec le pic, on le détruit
        if(collision.gameObject.tag == "Player"){
            collision.gameObject.GetComponent<PlayerMovement>().TakeDamage(10);
            Destroy(gameObject);
        }
        // Si le pic entre en collision avec un sol, on désactive la gravité et le boxcollider
        else{
            rb.gravityScale = 0;
            boxCollider2D.enabled = false;
        }
    }
}