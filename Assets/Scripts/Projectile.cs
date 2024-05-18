using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed;
    private Vector2 target;
    Rigidbody2D bulletRB;

    private float damageCooldown = 1.5f; // 2 seconds cooldown
    private float lastDamageTime;

    // Start is called before the first frame update
    void Start()
    {
        bulletRB = GetComponent<Rigidbody2D>();
        target = GameObject.FindGameObjectWithTag("Player").transform.position;
        Vector2 moveDirection = (target - (Vector2)transform.position).normalized * speed;
        bulletRB.velocity = new Vector2(moveDirection.x, moveDirection.y);
        Destroy(this.gameObject, 2f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
        void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerMovement player = collision.GetComponent<PlayerMovement>();
        if (player != null)
        {
            if (Time.time >= lastDamageTime + damageCooldown)
            {
                player.TakeDamage(10);
                lastDamageTime = Time.time;
            }
        }
    }
}


