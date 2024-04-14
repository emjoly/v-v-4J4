using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PicTombe : MonoBehaviour
{
    Rigidbody2D rb;
    BoxCollider2D boxCollider2D;
    public float distance;
    bool entrainTomber = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        boxCollider2D = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        Physics2D.queriesStartInColliders = false;
        if(entrainTomber == false){
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, distance);
            Debug.DrawRay(transform.position, Vector2.down * distance, Color.red);
            if(hit.transform != null){
                if(hit.collider.tag == "Player"){
                    rb.gravityScale = 5;
                    entrainTomber = true;
                }
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player"){
            Destroy(gameObject);
        }
        else{
            rb.gravityScale = 0;
            boxCollider2D.enabled = false;
        }
    }
}