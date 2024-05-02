using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoingtAnim : MonoBehaviour
{
    public float speed = 10.0f;
    public Transform player;
    public Boss boss; 
    private bool isMoving = false;
    private bool isFirstTime = true;
    public GameObject vieObject;

    void Start()
    {
        = new Vector3(player.position.x, player.position.y + 10, transform.position.z);

        StartCoroutine(FistRoutine());
    }

    void Update()
    {
       
        if (isMoving && transform.position.y < player.position.y)
        {
            transform.position = new Vector3(player.position.x, player.position.y + 10, transform.position.z);
        }

      
        if (isMoving)
        {
            transform.position += Vector3.down * speed * Time.deltaTime;
        }
    }

IEnumerator FistRoutine()
{
    while (boss.currentHealth > 0) 
    {
      
        if (isFirstTime)
        {
            yield return new WaitForSeconds(2);
            isFirstTime = false;
        }

        isMoving = true;

        yield return new WaitForSeconds(10);


        isMoving = false;
        yield return new WaitForSeconds(5);


        isMoving = true;
    }


    if (boss.currentHealth <= 0)
    {
        this.gameObject.SetActive(false);

        if (vieObject != null)
        {
            vieObject.SetActive(true);
        }
    }
}




    void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.tag == "Ground")
        {
            isMoving = false;
        }
    }
}
