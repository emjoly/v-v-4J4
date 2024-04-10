using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatforms : MonoBehaviour
{

    private float Delai = 1f;
    private float DestructionDelai = 2f;

    [SerializeField] private Rigidbody2D rb;

    private void OnCollisionEnter2D(Collision2D collision){
        if(collision.gameObject.CompareTag("Player")){
            StartCoroutine(Fall());
        }
    }

    private IEnumerator Fall(){
        yield return new WaitForSeconds(Delai);
        rb.bodyType = RigidbodyType2D.Dynamic;
        Destroy(gameObject, DestructionDelai);
    }
}
