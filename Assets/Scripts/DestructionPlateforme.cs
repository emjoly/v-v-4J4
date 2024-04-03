using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructionPlateforme : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
        private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the collided object is on the breakable layer and has the "BreakablePlatform" tag
        if (collision.gameObject.layer == LayerMask.NameToLayer("BrisPlateforme") && collision.relativeVelocity.magnitude > 10f)
        {
            BreakPlatform();
        }
    }

    public void BreakPlatform()
    {
        // Destroy the platform GameObject
        Destroy(gameObject);
    }
}
